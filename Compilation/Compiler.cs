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
using System.CodeDom.Compiler;
#endregion

namespace Reflexil.Compilation
{
    /// <summary>
    /// .NET source code compiler
    /// </summary>
    public class Compiler : MarshalByRefObject
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
        /// <summary>
        /// Lifetime initialization
        /// </summary>
        /// <returns>null for unlimited lifetime</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// Compile source code
        /// </summary>
        /// <param name="code">full source code to compile</param>
        /// <param name="references">assembly references</param>
        /// <param name="language">target language</param>
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

        /// <summary>
        /// Constructor.
        /// Checks that AppDomain isolation is correctly used
        /// </summary>
        public Compiler()
        {
            AppDomainHelper.CheckAppDomain();
        }
        #endregion

    }
}
