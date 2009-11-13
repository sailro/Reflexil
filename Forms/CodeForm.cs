/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Mono.Cecil;
using ICSharpCode.TextEditor.Gui;
using Reflexil.Compilation;
using Reflexil.Properties;
using Reflexil.Intellisense;
using Reflexil.Utils;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;
using System.Drawing;
#endregion

namespace Reflexil.Forms
{
	public partial class CodeForm 
    {

        #region " Fields "
        private AppDomain m_appdomain;
        private Compiler m_compiler;
        private MethodDefinition m_mdef;
        private MethodDefinition m_mdefsource;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        private List<AssemblyNameReference> CompileReferences
        {
            get
            {
                List<AssemblyNameReference> result = new List<AssemblyNameReference>();
                foreach (AssemblyNameReference asmref in m_mdefsource.DeclaringType.Module.AssemblyReferences)
                {
                    result.Add(asmref);
                }
                result.Add(m_mdefsource.DeclaringType.Module.Assembly.Name);
                return result;
            }
        }
        #endregion

        #region " Events "
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
            int srccol;
            int srcrow;

            if (e.RowIndex > -1)
            {
                srcrow = (int)ErrorGridView.Rows[e.RowIndex].Cells[ErrorLineColumn.Name].Value;
                srccol = (int)ErrorGridView.Rows[e.RowIndex].Cells[ErrorColumnColumn.Name].Value;

                if (TextEditor.ActiveTextAreaControl.Document.TotalNumberOfLines >= srcrow)
                {
                    TextEditor.ActiveTextAreaControl.JumpTo(srcrow - 1);
                    TextEditor.ActiveTextAreaControl.Caret.Line = srcrow - 1;
                    TextEditor.ActiveTextAreaControl.Caret.Column = srccol - 1;
                }
                TextEditor.Focus();
            }
        }
        #endregion

        #region " Methods "
        public CodeForm() {
            InitializeComponent();
        }

        public CodeForm(MethodDefinition source)
        {
            InitializeComponent();
            m_mdefsource = source;

            ILanguageHelper helper = LanguageHelperFactory.GetLanguageHelper(Settings.Default.Language);
            TextEditor.Text = helper.GenerateSourceCode(source, CompileReferences);

            // Hook AssemblyResolve Event, usefull if reflexil is not located in the Reflector path
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            m_appdomain = AppDomainHelper.CreateAppDomain();
            m_compiler = AppDomainHelper.CreateCompilerInstanceAndUnwrap(m_appdomain);

            SetupIntellisense(TextEditor);

            TextEditor.Document.FoldingManager.FoldingStrategy = new RegionFoldingStrategy();
            TextEditor.Document.FoldingManager.UpdateFoldings(DummyFileName, null);
            TextEditor.Refresh();
        }

        private bool MarkerSelector(TextMarker textmarker)
        {
            return true;
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.GetExecutingAssembly().FullName == args.Name ? Assembly.GetExecutingAssembly() : null;
        }

        public override String[] GetReferences(bool keepextension)
        {
            List<string> references = new List<string>();
            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();

            // We can now use Cecil FileInformation.DirectoryName
            Directory.SetCurrentDirectory(m_mdefsource.DeclaringType.Module.Image.FileInformation.DirectoryName);

            foreach (AssemblyNameReference asmref in CompileReferences)
            {
                string reference;

                if (asmref.Name == "mscorlib" || asmref.Name.StartsWith("System"))
                {
                    reference = asmref.Name + ((keepextension) ? ".dll": string.Empty);
                }
                else
                {
                    try
                    {
                        AssemblyDefinition asmdef = resolver.Resolve(asmref);
                        FileInfo finfo = asmdef.MainModule.Image.FileInformation;
                        reference = finfo.FullName;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                if (!references.Contains(reference))
                {
                    references.Add(reference);
                }
            }

            return references.ToArray();
        }

        private bool Compile()
        {
            TextEditor.Document.MarkerStrategy.RemoveAll(MarkerSelector);

            m_compiler.Compile(TextEditor.Text, GetReferences(true), Settings.Default.Language);
            if (!m_compiler.Errors.HasErrors)
            {
                m_mdef = FindMatchingMethod();
                ButOk.Enabled = m_mdef != null;
                VerticalSplitContainer.Panel2Collapsed = true;
            }
            else
            {
                m_mdef = null;
                ButOk.Enabled = false;
                CompilerErrorBindingSource.DataSource = m_compiler.Errors;
                VerticalSplitContainer.Panel2Collapsed = false;

                //Add error markers to the TextEditor
                foreach (CompilerError error in m_compiler.Errors)
                {
                    if (error.Line > 0)
                    {
                        int offset = TextEditor.Document.PositionToOffset(new TextLocation(error.Column, error.Line - 1));
                        int length = TextEditor.Document.LineSegmentCollection[error.Line - 1].Length - error.Column + 1;
                        if (length <= 0)
                        {
                            length = 1;
                        }
                        else
                        {
                            offset--;
                        }
                        Color color = (error.IsWarning) ? Color.Orange : Color.Red;
                        TextMarker marker = new TextMarker(offset, length, TextMarkerType.WaveLine, color);
                        marker.ToolTip = error.ErrorText;
                        TextEditor.Document.MarkerStrategy.AddMarker(marker);
                    }
                }
            }

            TextEditor.Refresh();

            MethodHandler.HandleItem(m_mdef);
            return m_compiler.Errors.HasErrors;
        }

        private MethodDefinition FindMatchingMethod()
        {
            MethodDefinition result = null;

            AssemblyDefinition asmdef = AssemblyFactory.GetAssembly(m_compiler.AssemblyLocation);
            TypeDefinition tdef = asmdef.MainModule.Types[m_mdefsource.DeclaringType.FullName];
            if (tdef != null)
            {
                result = CecilHelper.FindMatchingMethod(tdef, (MethodReference)m_mdefsource);
            }

            return result;
        }

        private void CleanCompilationEnvironment()
        {
            m_compiler = null;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            AppDomain.Unload(m_appdomain);
            m_appdomain = null;
        }
        #endregion

	}
}