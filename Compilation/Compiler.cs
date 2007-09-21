
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
#endregion

namespace Reflexil.Compilation
{
    class Compiler : MarshalByRefObject
    {

        #region " Fields "
        private CompilerErrorCollection m_errors;
        private string m_assemblyLocation;
        #endregion

        #region " Properties "
        public CompilerErrorCollection Errors
        {
            get
            {
                return m_errors;
            }
        }

        public string AssemblyLocation
        {
            get
            {
                return m_assemblyLocation;
            }
        }
        #endregion

        #region " Methods "
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Compile(string code, string[] references, ESupportedLanguage language)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider(language.ToString());
            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = false;
            parameters.IncludeDebugInformation = false;
            parameters.ReferencedAssemblies.AddRange(references);

            if (language == ESupportedLanguage.CSharp)
            {
                parameters.CompilerOptions = "/unsafe";
            }

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            m_assemblyLocation = null;
            m_errors = results.Errors;

            if (!results.Errors.HasErrors)
            {
                m_assemblyLocation = results.CompiledAssembly.Location;
            }
        }

        public Compiler()
        {
            AppDomainHelper.CheckAppDomain();
        }
        #endregion

    }
}
