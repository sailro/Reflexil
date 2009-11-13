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
            m_parseinformation = new ParseInformation();

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
            m_projectcontentregistry.ActivatePersistence(Path.Combine(Path.GetTempPath(), REFLEXIL_PERSISTENCE));

            m_projectcontent = new DefaultProjectContent();
            m_projectcontent.Language = LanguageProperties;
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
                    if ((!Disposing) && (m_control != null))
                    {
                        code = m_control.Text;
                    }
                }));
                TextReader textReader = new StringReader(code);
                ICompilationUnit newCompilationUnit;

                using (IParser p = ParserFactory.CreateParser(RefactorySupportedLanguage, textReader))
                {
                    p.Parse();
                    newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
                }
                // Remove information from lastCompilationUnit and add information from newCompilationUnit.
                ProjectContent.UpdateCompilationUnit(LastCompilationUnit, newCompilationUnit, DummyFileName);
                m_lastcompilationunit = newCompilationUnit;
                ParseInformation.SetCompilationUnit(newCompilationUnit);
            }
            catch (Exception)
            {
            }
        }

        ICompilationUnit ConvertCompilationUnit(CompilationUnit cu)
        {
            NRefactoryASTConvertVisitor converter;
            converter = new NRefactoryASTConvertVisitor(ProjectContent);
            cu.AcceptVisitor(converter, null);
            return converter.Cu;
        }
#endregion
        
	}

}
