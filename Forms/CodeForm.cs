/* Reflexil Copyright (c) 2007-2021 Sebastien Lebreton

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Mono.Cecil;
using Reflexil.Compilation;
using Reflexil.Properties;
using Reflexil.Intellisense;
using Reflexil.Utils;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;
using System.Drawing;
using Reflexil.Plugins;

namespace Reflexil.Forms
{
	public partial class CodeForm
	{
		private AppDomain _appdomain;
		private Compiler _compiler;
		private readonly MethodDefinition _mdefsource;

		public MethodDefinition MethodDefinition { get; private set; }

		private List<AssemblyNameReference> CompileReferences
		{
			get
			{
				var result = _mdefsource.DeclaringType.Module.AssemblyReferences.ToList();
				result.Add(_mdefsource.DeclaringType.Module.Assembly.Name);
				return result;
			}
		}

		private bool IsUnityOrSilverLightAssembly
		{
			get
			{
				return
					CompileReferences.Any(
						an =>
							an.Name == "mscorlib" && an.Version == Compiler.UnitySilverLightVersion &&
							ByteHelper.ByteToString(an.PublicKeyToken) == Compiler.SilverLightPublicKeyToken);
			}
		}

		private bool IsSilverLight5Assembly
		{
			get
			{
				return
					CompileReferences.Any(
						an =>
							an.Name == "mscorlib" && an.Version == Compiler.SilverLight5Version && ByteHelper.ByteToString(an.PublicKeyToken) == Compiler.SilverLightPublicKeyToken);
			}
		}

		private bool IsReferencingSystemCore
		{
			get { return CompileReferences.Any(an => an.Name == "System.Core" && an.Version.ToString(2) == "3.5"); }
		}

		private void TextEditor_TextChanged(object sender, EventArgs e)
		{
			if (TextEditor.Document.FoldingManager.FoldingStrategy != null)
			{
				TextEditor.Document.FoldingManager.UpdateFoldings(null, null);
			}
		}

		private void ButPreview_Click(object sender, EventArgs e)
		{
			Compile();
		}

		private void CodeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			CleanCompilationEnvironment();
		}

		private void ErrorGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex <= -1)
				return;

			var srcrow = (int)ErrorGridView.Rows[e.RowIndex].Cells[ErrorLineColumn.Name].Value;
			var srccol = (int)ErrorGridView.Rows[e.RowIndex].Cells[ErrorColumnColumn.Name].Value;

			if (TextEditor.ActiveTextAreaControl.Document.TotalNumberOfLines > srcrow && srcrow > 0)
			{
				TextEditor.ActiveTextAreaControl.JumpTo(srcrow - 1);
				TextEditor.ActiveTextAreaControl.Caret.Line = srcrow - 1;
				TextEditor.ActiveTextAreaControl.Caret.Column = srccol - 1;
			}

			TextEditor.Focus();
		}

		public CodeForm()
		{
			InitializeComponent();
		}

		public CodeForm(MethodDefinition source)
		{
			InitializeComponent();
			_mdefsource = source;

			var helper = LanguageHelperFactory.GetLanguageHelper(Settings.Default.Language);
			TextEditor.Text = helper.GenerateSourceCode(source, CompileReferences);

			// Guess best compiler profile
			SelProfile.Items.Add(Compiler.DotNet2Profile);
			SelProfile.Items.Add(Compiler.DotNet35Profile);
			SelProfile.Items.Add(Compiler.DotNet4Profile);
			SelProfile.Items.Add(Compiler.UnitySilverLightProfile);
			SelProfile.Items.Add(Compiler.SilverLight5Profile);
			SelProfile.SelectedItem = GuessCompilerProfile(source);

			// Hook AssemblyResolve Event, usefull if reflexil is not located in the host program path
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			_appdomain = AppDomainHelper.CreateAppDomain();
			_compiler = AppDomainHelper.CreateCompilerInstanceAndUnwrap(_appdomain);

			SetupIntellisense(TextEditor);

			TextEditor.Document.FoldingManager.FoldingStrategy = new RegionFoldingStrategy();
			TextEditor.Document.FoldingManager.UpdateFoldings(DummyFileName, null);
			TextEditor.Refresh();
		}

		private CompilerProfile GuessCompilerProfile(MethodDefinition source)
		{
			var runtime = source.Module.Runtime;

			if (runtime == TargetRuntime.Net_4_0)
				return IsSilverLight5Assembly ? Compiler.SilverLight5Profile : Compiler.DotNet4Profile;

			if (runtime != TargetRuntime.Net_2_0)
				return Compiler.DotNet2Profile;

			if (IsUnityOrSilverLightAssembly)
				return Compiler.UnitySilverLightProfile;

			if (IsReferencingSystemCore)
				return Compiler.DotNet35Profile;

			return Compiler.DotNet2Profile;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return Assembly.GetExecutingAssembly().FullName == args.Name ? Assembly.GetExecutingAssembly() : null;
		}

		public sealed override string[] GetReferences(bool keepextension, CompilerProfile profile)
		{
			var references = new List<string>();

			var parameters = new ReaderParameters {ReadSymbols = false, ReadingMode = ReadingMode.Deferred, InMemory = true,};
			var resolver = new ReflexilAssemblyResolver(parameters);

			var filename = _mdefsource.DeclaringType.Module.Image.FileName;
			var currentPath = Path.GetDirectoryName(filename);

			if (currentPath != null)
			{
				Directory.SetCurrentDirectory(currentPath);
				resolver.AddSearchDirectory(currentPath);
				// Properly register assembly, so we can find it even if the name is changed
				resolver.RegisterAssembly(_mdefsource.DeclaringType.Module.Assembly);
			}

			foreach (var asmref in CompileReferences)
			{
				string reference;

				if ((profile == null || !profile.NoStdLib) && (asmref.Name == "mscorlib" || asmref.Name.StartsWith("System")))
				{
					reference = asmref.Name + (keepextension ? ".dll" : string.Empty);
				}
				else
				{
					try
					{
						var asmdef = resolver.Resolve(asmref);
						reference = asmdef.MainModule.Image.FileName;
					}
					catch (Exception)
					{
						continue;
					}
				}

				if (!references.Contains(reference))
					references.Add(reference);
			}

			return references.ToArray();
		}

		private void Compile()
		{
			TextEditor.Document.MarkerStrategy.RemoveAll(marker => true);
			var profile = (CompilerProfile)SelProfile.SelectedItem;

			_compiler.Compile(TextEditor.Text, GetReferences(true, profile), Settings.Default.Language, profile);

			if (!_compiler.Errors.HasErrors)
			{
				MethodDefinition = FindMatchingMethod();

				if (profile == Compiler.UnitySilverLightProfile && MethodDefinition != null)
					CecilHelper.PatchAssemblyNames(MethodDefinition.Module, Compiler.MicrosoftPublicKeyToken, Compiler.MicrosoftClr2Version, Compiler.SilverLightPublicKeyToken,
						Compiler.UnitySilverLightVersion);

				if (profile == Compiler.SilverLight5Profile && MethodDefinition != null)
				{
					CecilHelper.PatchAssemblyNames(MethodDefinition.Module, Compiler.MicrosoftPublicKeyToken, Compiler.MicrosoftClr2Version, Compiler.SilverLightPublicKeyToken,
						Compiler.SilverLight5Version);
					CecilHelper.PatchAssemblyNames(MethodDefinition.Module, Compiler.MicrosoftPublicKeyToken, Compiler.MicrosoftClr4Version, Compiler.SilverLightPublicKeyToken,
						Compiler.SilverLight5Version);
				}

				ButOk.Enabled = MethodDefinition != null;
				VerticalSplitContainer.Panel2Collapsed = true;
			}
			else
			{
				MethodDefinition = null;
				ButOk.Enabled = false;
				CompilerErrorBindingSource.DataSource = _compiler.Errors;
				VerticalSplitContainer.Panel2Collapsed = false;

				//Add error markers to the TextEditor
				foreach (CompilerError error in _compiler.Errors)
				{
					if (error.Line <= 0)
						continue;

					var offset = TextEditor.Document.PositionToOffset(new TextLocation(error.Column, error.Line - 1));
					var length = TextEditor.Document.LineSegmentCollection[error.Line - 1].Length - error.Column + 1;

					if (length <= 0)
						length = 1;
					else
						offset--;

					var color = error.IsWarning ? Color.Orange : Color.Red;
					var marker = new TextMarker(offset, length, TextMarkerType.WaveLine, color) {ToolTip = error.ErrorText};
					TextEditor.Document.MarkerStrategy.AddMarker(marker);
				}
			}

			TextEditor.Refresh();

			MethodHandler.HandleItem(MethodDefinition);
		}

		private MethodDefinition FindMatchingMethod()
		{
			MethodDefinition result = null;

			var asmdef = PluginFactory.GetInstance().LoadAssembly(_compiler.AssemblyLocation, false);

			// Fix for inner types, remove namespace and owner.
			var typename = (_mdefsource.DeclaringType.IsNested)
				? _mdefsource.DeclaringType.Name
				: _mdefsource.DeclaringType.FullName;

			// Generic hierarchy will push all generic parameters to the final type, so fix type name
			var tag = typename.LastIndexOf(BaseLanguageHelper.GenericTypeTag, StringComparison.Ordinal);
			if (tag >= 0)
				typename = string.Concat(typename.Substring(0, tag + 1), _mdefsource.DeclaringType.GenericParameters.Count);

			var tdef = CecilHelper.FindMatchingType(asmdef.MainModule, typename);
			if (tdef != null)
				result = CecilHelper.FindMatchingMethod(tdef, _mdefsource);

			return result;
		}

		private void CleanCompilationEnvironment()
		{
			_compiler = null;
			AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
			AppDomain.Unload(_appdomain);
			_appdomain = null;
		}
	}
}
