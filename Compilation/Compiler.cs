/* Reflexil Copyright (c) 2007-2014 Sebastien LEBRETON

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
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;
using Microsoft.VisualBasic;
#endregion

namespace Reflexil.Compilation
{
    /// <summary>
    /// .NET source code compiler
    /// </summary>
    public class Compiler : MarshalByRefObject
    {
        #region Consts
        private const string CompilerVersion = "CompilerVersion";
        public const string CompilerV20 = "v2.0";
        public const string CompilerV35 = "v3.5";
        public const string CompilerV40 = "v4.0";
        #endregion

        #region Properties

	    public CompilerErrorCollection Errors { get; private set; }
	    public string AssemblyLocation { get; private set; }

	    #endregion

        #region Methods
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
        /// <param name="compilerVersion">compiler version</param>
        public void Compile(string code, string[] references, ESupportedLanguage language, String compilerVersion)
        {
            var properties = new Dictionary<string, string> {{CompilerVersion, compilerVersion}};
            CodeDomProvider provider;

            switch (language)
            {
                case ESupportedLanguage.CSharp:
                    provider = new CSharpCodeProvider(properties);
                    break;
                case ESupportedLanguage.VisualBasic:
                    provider = new VBCodeProvider(properties);
                    break;
                default:
                    throw new ArgumentException();
            }

            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = false,
                IncludeDebugInformation = false
            };

            parameters.ReferencedAssemblies.AddRange(references);

            if (language == ESupportedLanguage.CSharp)
            {
                parameters.CompilerOptions = "/unsafe";
            }

            var results = provider.CompileAssemblyFromSource(parameters, code);
            AssemblyLocation = null;
            Errors = results.Errors;

            if (!results.Errors.HasErrors)
                AssemblyLocation = results.CompiledAssembly.Location;
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
