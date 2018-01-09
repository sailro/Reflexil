/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Reflexil.Compilation
{
	public class Compiler : MarshalByRefObject
	{
		private const string CompilerVersion = "CompilerVersion";
		public static readonly CompilerProfile DotNet2Profile = new CompilerProfile {Caption = ".NET 2.0", CompilerVersion = "v2.0"};
		public static readonly CompilerProfile DotNet35Profile = new CompilerProfile {Caption = ".NET 3.5", CompilerVersion = "v3.5"};
		public static readonly CompilerProfile DotNet4Profile = new CompilerProfile {Caption = ".NET 4.0", CompilerVersion = "v4.0"};

		public static readonly CompilerProfile UnitySilverLightProfile = new CompilerProfile
		{
			Caption = "Unity/SilverLight",
			CompilerVersion = "v3.5",
			NoStdLib = true
		};

		public static readonly CompilerProfile SilverLight5Profile = new CompilerProfile {Caption = "SilverLight 5", CompilerVersion = "v4.0", NoStdLib = true};

		public const string MicrosoftPublicKeyToken = "b77a5c561934e089";
		public const string SilverLightPublicKeyToken = "7cec85d7bea7798e";

		public static readonly Version MicrosoftClr2Version = new Version(2, 0, 0, 0);
		public static readonly Version MicrosoftClr4Version = new Version(4, 0, 0, 0);
		public static readonly Version UnitySilverLightVersion = new Version(2, 0, 5, 0);
		public static readonly Version SilverLight5Version = new Version(5, 0, 5, 0);

		public CompilerErrorCollection Errors { get; private set; }
		public string AssemblyLocation { get; private set; }

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public void Compile(string code, string[] references, SupportedLanguage language, CompilerProfile profile)
		{
			var properties = new Dictionary<string, string> {{CompilerVersion, profile.CompilerVersion}};
			CodeDomProvider provider;

			switch (language)
			{
				case SupportedLanguage.CSharp:
					provider = new CSharpCodeProvider(properties);
					break;
				case SupportedLanguage.VisualBasic:
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

			var compilerOptions = new List<string>();
			if (language == SupportedLanguage.CSharp)
				compilerOptions.Add("/unsafe");

			if (profile.NoStdLib)
				compilerOptions.Add("/nostdlib");

			if (compilerOptions.Count > 0)
				parameters.CompilerOptions = string.Join(" ", compilerOptions.ToArray());

			var results = provider.CompileAssemblyFromSource(parameters, code);
			AssemblyLocation = null;
			Errors = results.Errors;

			if (!results.Errors.HasErrors)
				AssemblyLocation = results.CompiledAssembly.Location;
		}

		public Compiler()
		{
			AppDomainHelper.CheckAppDomain();
		}
	}
}