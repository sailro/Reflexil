
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Mono.Cecil;
using Fireball.CodeEditor.SyntaxFiles;
using Reflexil.Compilation;
using Reflexil.Utils;
using Reflexil.Properties;
using Reflector.CodeModel;
using System.IO;
#endregion

namespace Reflexil.Forms
{
	public partial class CodeForm: Form
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

                CodeEditor.ActiveViewControl.GotoLine(srcrow - 1);
                CodeEditor.ActiveViewControl.Focus();
            }
        }
        #endregion

        #region " Methods "
        public CodeForm(MethodDefinition source)
        {
            InitializeComponent();
            CodeEditorSyntaxLoader.SetSyntax(CodeEditor, (Settings.Default.Language == ESupportedLanguage.CSharp) ? SyntaxLanguage.CSharp : SyntaxLanguage.VBNET);
            m_mdefsource = source;

            ILanguageHelper helper = LanguageHelperFactory.GetLanguageHelper(Settings.Default.Language);
            SyntaxDocument.Text = helper.GenerateSourceCode(source, CompileReferences);

            // Hook AssemblyResolve Event, usefull if reflexil is not located in the Reflector path
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            m_appdomain = AppDomainHelper.CreateAppDomain();
            m_compiler = AppDomainHelper.CreateCompilerInstanceAndUnwrap(m_appdomain);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.GetExecutingAssembly().FullName == args.Name ? Assembly.GetExecutingAssembly() : null;
        }

        private bool Compile()
        {
            List<string> references = new List<string>();
            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();

            // We can't use Cecil FileInformation.DirectoryName, set to 'null' after being saved to disk
            // Directory.SetCurrentDirectory(m_mdefsource.DeclaringType.Module.Image.FileInformation.DirectoryName);

            foreach (IAssembly refasm in DataManager.GetInstance().GetReflectorAssemblies())
            {
                if (DataManager.GetInstance().IsAssemblyDefintionLoaded(refasm.Location))
                {
                    if (DataManager.GetInstance().GetAssemblyDefinition(refasm.Location) == m_mdefsource.DeclaringType.Module.Assembly)
                    {
                        string location = System.Environment.ExpandEnvironmentVariables(refasm.Location);
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(location));
                    }
                }
            }

            foreach (AssemblyNameReference asmref in CompileReferences)
            {
                string reference;

                if (asmref.Name == "mscorlib" || asmref.Name.StartsWith("System"))
                {
                    reference = asmref.Name + ".dll";
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

            m_compiler.Compile(SyntaxDocument.Text, references.ToArray(), Settings.Default.Language);
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
            }

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
                result = CecilHelper.FindMatchingMethod(tdef, m_mdefsource);
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