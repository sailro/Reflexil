/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using ICSharpCode.TextEditor;
using ICSharpCode.SharpDevelop.Dom;
using Reflexil.Properties;
using Reflexil.Intellisense;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;
using Reflexil.Compilation;
using SupportedLanguage = Reflexil.Compilation.SupportedLanguage;

#endregion

namespace Reflexil.Forms
{
	public partial class IntellisenseForm : Form
	{
		#region Constants

		public const string ReflexilPersistence = "Reflexil.Persistence";
		public const string ReflexilPersistenceCheck = "Reflexil.chk";

		#endregion

		#region Fields

		private Thread _parserThread;
		private TextEditorControl _control;

		#endregion

		#region Properties

		public ICompilationUnit LastCompilationUnit { get; private set; }

		public ParseInformation ParseInformation { get; private set; }

		public DefaultProjectContent ProjectContent { get; private set; }

		public ProjectContentRegistry ProjectContentRegistry { get; private set; }

		public static string DummyFileName
		{
			get { return "source." + ((SupportedLanguage == SupportedLanguage.CSharp) ? "cs" : "vb"); }
		}

		public static SupportedLanguage SupportedLanguage
		{
			get { return Settings.Default.Language; }
		}

		public static LanguageProperties LanguageProperties
		{
			get
			{
				switch (SupportedLanguage)
				{
					case SupportedLanguage.CSharp:
						return LanguageProperties.CSharp;
					case SupportedLanguage.VisualBasic:
						return LanguageProperties.VBNet;
					default:
						throw new NotImplementedException();
				}
			}
		}

		public static ICSharpCode.NRefactory.SupportedLanguage RefactorySupportedLanguage
		{
			get
			{
				switch (SupportedLanguage)
				{
					case SupportedLanguage.CSharp:
						return ICSharpCode.NRefactory.SupportedLanguage.CSharp;
					case SupportedLanguage.VisualBasic:
						return ICSharpCode.NRefactory.SupportedLanguage.VBNet;
					default:
						throw new NotImplementedException();
				}
			}
		}

		#endregion

		#region Methods

		public IntellisenseForm()
		{
			InitializeComponent();
		}

		public virtual String[] GetReferences(bool keepextension, CompilerProfile profile)
		{
			// We cannot use abstract modifier because of the designer, let's derived class handle this method
			throw new NotImplementedException();
		}

		public void SetupIntellisense(TextEditorControl control)
		{
			_control = control;

			control.SetHighlighting((SupportedLanguage == SupportedLanguage.CSharp) ? "C#" : "VBNET");
			control.ShowEOLMarkers = false;
			control.ShowInvalidLines = false;

			HostCallbackImplementation.Register(this);
			CodeCompletionKeyHandler.Attach(this, control);
			ToolTipProvider.Attach(this, control);

			ProjectContentRegistry = new ProjectContentRegistry(); // Default .NET 2.0 registry

			// Persistence lets SharpDevelop.Dom create a cache file on disk so that
			// future starts are faster.
			// It also caches XML documentation files in an on-disk hash table, thus
			// reducing memory usage.
			try
			{
				if (Settings.Default.CacheFiles)
				{
					var persistencePath = Path.Combine(Path.GetTempPath(), ReflexilPersistence);
					var persistenceCheck = Path.Combine(persistencePath, ReflexilPersistenceCheck);

					Directory.CreateDirectory(persistencePath); // Check write/access to directory
					File.WriteAllText(persistenceCheck, @"Using cache!"); // Check write file rights
					File.ReadAllText(persistenceCheck); // Check read file rights

					ProjectContentRegistry.ActivatePersistence(persistencePath);
				}
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception)
			{
				// don't use cache file
			}

			ProjectContent = new DefaultProjectContent {Language = LanguageProperties};
			ParseInformation = new ParseInformation(new DefaultCompilationUnit(ProjectContent));
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (DesignMode)
				return;

			_parserThread = new Thread(ParserThread) {IsBackground = true};
			_parserThread.Start();
		}

		private void ParserThread()
		{
			ProjectContent.AddReferencedContent(ProjectContentRegistry.Mscorlib);

			// do one initial parser step to enable code-completion while other references are loading
			ParseStep();

			foreach (var assemblyName in GetReferences(false, null))
			{
				var referenceProjectContent = ProjectContentRegistry.GetProjectContentForReference(assemblyName, assemblyName);
				ProjectContent.AddReferencedContent(referenceProjectContent);
				if (referenceProjectContent is ReflectionProjectContent)
					(referenceProjectContent as ReflectionProjectContent).InitializeReferences();
			}

			if (SupportedLanguage == SupportedLanguage.VisualBasic)
			{
				ProjectContent.DefaultImports = new DefaultUsing(ProjectContent);
				ProjectContent.DefaultImports.Usings.Add("System");
				ProjectContent.DefaultImports.Usings.Add("System.Text");
				ProjectContent.DefaultImports.Usings.Add("Microsoft.VisualBasic");
			}

			// Parse the current file every 2 seconds
			while (!IsDisposed)
			{
				ParseStep();

				Thread.Sleep(2000);
			}
		}

		private void ParseStep()
		{
			try
			{
				string code = null;
				Invoke(new MethodInvoker(delegate { code = _control.Text; }));
				TextReader textReader = new StringReader(code);
				ICompilationUnit newCompilationUnit;
				var supportedLanguage = SupportedLanguage == SupportedLanguage.CSharp
					? ICSharpCode.NRefactory.SupportedLanguage.CSharp
					: ICSharpCode.NRefactory.SupportedLanguage.VBNet;
				using (var p = ParserFactory.CreateParser(supportedLanguage, textReader))
				{
					// we only need to parse types and method definitions, no method bodies
					// so speed up the parser and make it more resistent to syntax
					// errors in methods
					p.ParseMethodBodies = false;

					p.Parse();
					newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
				}

				// Remove information from lastCompilationUnit and add information from newCompilationUnit.
				ProjectContent.UpdateCompilationUnit(LastCompilationUnit, newCompilationUnit, DummyFileName);
				LastCompilationUnit = newCompilationUnit;
				ParseInformation = new ParseInformation(newCompilationUnit);
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception)
			{
			}
		}

		private ICompilationUnit ConvertCompilationUnit(CompilationUnit cu)
		{
			var supportedLanguage = SupportedLanguage == SupportedLanguage.CSharp
				? ICSharpCode.NRefactory.SupportedLanguage.CSharp
				: ICSharpCode.NRefactory.SupportedLanguage.VBNet;
			var converter = new NRefactoryASTConvertVisitor(ProjectContent, supportedLanguage);
			cu.AcceptVisitor(converter, null);
			return converter.Cu;
		}

		#endregion
	}
}