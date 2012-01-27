/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ICSharpCode.TextEditor;
using ICSharpCode.SharpDevelop.Dom;
using Reflexil.Properties;
using Reflexil.Compilation;
using Reflexil.Intellisense;
using Reflexil.Utils;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;
#endregion 

namespace Reflexil.Forms
{
	public partial class IntellisenseForm: Form
    {

        #region " Constants "
        public const string REFLEXIL_PERSISTENCE = "Reflexil.Persistence";
        public const string REFLEXIL_PERSISTENCE_CHECK = "Reflexil.chk";
        #endregion

        #region " Fields "
        private ProjectContentRegistry m_projectcontentregistry;
        private DefaultProjectContent m_projectcontent;
        private ParseInformation m_parseinformation;
        private ICompilationUnit m_lastcompilationunit;
        private Thread m_parserthread;
        private TextEditorControl m_control;
        #endregion

        #region " Properties "
        public ICompilationUnit LastCompilationUnit
        {
            get
            {
                return m_lastcompilationunit;
            }
        }

        public ParseInformation ParseInformation
        {
            get
            {
                return m_parseinformation;
            }
        }

        public DefaultProjectContent ProjectContent
        {
            get
            {
                return m_projectcontent;
            }
        }

        public ProjectContentRegistry ProjectContentRegistry
        {
            get
            {
                return m_projectcontentregistry;
            }
        }

        public static string DummyFileName
        {
            get
            {
                return "source." + ((SupportedLanguage == ESupportedLanguage.CSharp) ? "cs" : "vb");
            }
        }

        public static ESupportedLanguage SupportedLanguage
        {
            get
            {
                return Settings.Default.Language;
            }
        }

        public static LanguageProperties LanguageProperties
        {
            get
            {
                switch (SupportedLanguage)
                {
                    case ESupportedLanguage.CSharp:
                        return LanguageProperties.CSharp;
                    case ESupportedLanguage.VisualBasic:
                        return LanguageProperties.VBNet;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public static SupportedLanguage RefactorySupportedLanguage
        {
            get
            {
                switch (SupportedLanguage)
                {
                    case ESupportedLanguage.CSharp:
                        return ICSharpCode.NRefactory.SupportedLanguage.CSharp;
                    case ESupportedLanguage.VisualBasic:
                        return ICSharpCode.NRefactory.SupportedLanguage.VBNet;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        #endregion

        #region " Methods "
        public IntellisenseForm()
        {
            InitializeComponent();
        }

        public virtual String[] GetReferences(bool keepextension)
        {
            throw new NotImplementedException();
        }

        public void SetupIntellisense(TextEditorControl control)
        {
            m_control = control;

            control.SetHighlighting((SupportedLanguage == ESupportedLanguage.CSharp) ? "C#" : "VBNET");
            control.ShowEOLMarkers = false;
            control.ShowInvalidLines = false;

            HostCallbackImplementation.Register(this);
            CodeCompletionKeyHandler.Attach(this, control);
            ToolTipProvider.Attach(this, control);

            m_projectcontentregistry = new ProjectContentRegistry(); // Default .NET 2.0 registry

            // Persistence lets SharpDevelop.Dom create a cache file on disk so that
            // future starts are faster.
            // It also caches XML documentation files in an on-disk hash table, thus
            // reducing memory usage.
            try
            {
                if (Settings.Default.CacheFiles)
                {
                    String persistencePath = Path.Combine(Path.GetTempPath(), REFLEXIL_PERSISTENCE);
                    String persistenceCheck = Path.Combine(persistencePath, REFLEXIL_PERSISTENCE_CHECK);

                    Directory.CreateDirectory(persistencePath); // Check write/access to directory
                    File.WriteAllText(persistenceCheck, "Using cache!"); // Check write file rights
                    File.ReadAllText(persistenceCheck); // Check read file rights

                    m_projectcontentregistry.ActivatePersistence(persistencePath);
                }
            }
            catch (Exception)
            {
                // don't use cache file
            }

            m_projectcontent = new DefaultProjectContent();
            m_projectcontent.Language = LanguageProperties;

            m_parseinformation = new ParseInformation(new DefaultCompilationUnit(ProjectContent));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                m_parserthread = new Thread(ParserThread);
                m_parserthread.IsBackground = true;
                m_parserthread.Start();
            }
        }

        void ParserThread()
        {
            //BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading mscorlib..."; }));
            m_projectcontent.AddReferencedContent(ProjectContentRegistry.Mscorlib);

            // do one initial parser step to enable code-completion while other
            // references are loading
            ParseStep();

            foreach (string assemblyName in GetReferences(false))
            {
                string assemblyNameCopy = assemblyName; // copy for anonymous method
                //BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading " + assemblyNameCopy + "..."; }));
                IProjectContent referenceProjectContent = ProjectContentRegistry.GetProjectContentForReference(assemblyName, assemblyName);
                ProjectContent.AddReferencedContent(referenceProjectContent);
                if (referenceProjectContent is ReflectionProjectContent)
                {
                    (referenceProjectContent as ReflectionProjectContent).InitializeReferences();
                }
            }
            if (SupportedLanguage == ESupportedLanguage.VisualBasic)
            {
                ProjectContent.DefaultImports = new DefaultUsing(ProjectContent);
                ProjectContent.DefaultImports.Usings.Add("System");
                ProjectContent.DefaultImports.Usings.Add("System.Text");
                ProjectContent.DefaultImports.Usings.Add("Microsoft.VisualBasic");
            }
            //BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Ready"; }));

            // Parse the current file every 2 seconds
            while (!IsDisposed)
            {
                ParseStep();

                Thread.Sleep(2000);
            }
        }

        void ParseStep()
        {
            try
            {
                string code = null;
                Invoke(new MethodInvoker(delegate
                {
                    code = m_control.Text;
                }));
                TextReader textReader = new StringReader(code);
                ICompilationUnit newCompilationUnit;
                SupportedLanguage supportedLanguage = SupportedLanguage == ESupportedLanguage.CSharp ? ICSharpCode.NRefactory.SupportedLanguage.CSharp : ICSharpCode.NRefactory.SupportedLanguage.VBNet;
                using (IParser p = ParserFactory.CreateParser(supportedLanguage, textReader))
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
                m_lastcompilationunit = newCompilationUnit;
                m_parseinformation = new ParseInformation(newCompilationUnit);
            }
            catch (Exception)
            {
            }
        }

        ICompilationUnit ConvertCompilationUnit(CompilationUnit cu)
        {
            NRefactoryASTConvertVisitor converter;
            SupportedLanguage supportedLanguage = SupportedLanguage == ESupportedLanguage.CSharp ? ICSharpCode.NRefactory.SupportedLanguage.CSharp : ICSharpCode.NRefactory.SupportedLanguage.VBNet;
            converter = new NRefactoryASTConvertVisitor(ProjectContent, supportedLanguage);
            cu.AcceptVisitor(converter, null);
            return converter.Cu;
        }
        #endregion
        
	}

}
