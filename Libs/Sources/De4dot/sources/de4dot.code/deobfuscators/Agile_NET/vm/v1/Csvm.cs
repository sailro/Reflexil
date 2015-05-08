﻿/*
    Copyright (C) 2011-2014 de4dot@gmail.com

    This file is part of de4dot.

    de4dot is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    de4dot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with de4dot.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using dnlib.DotNet;
using de4dot.blocks;

namespace de4dot.code.deobfuscators.Agile_NET.vm.v1 {
	class Csvm {
		IDeobfuscatorContext deobfuscatorContext;
		ModuleDefMD module;
		EmbeddedResource resource;
		AssemblyRef vmAssemblyRef;

		public bool Detected {
			get { return resource != null && vmAssemblyRef != null; }
		}

		public EmbeddedResource Resource {
			get { return Detected ? resource : null; }
		}

		public Csvm(IDeobfuscatorContext deobfuscatorContext, ModuleDefMD module) {
			this.deobfuscatorContext = deobfuscatorContext;
			this.module = module;
		}

		public Csvm(IDeobfuscatorContext deobfuscatorContext, ModuleDefMD module, Csvm oldOne) {
			this.deobfuscatorContext = deobfuscatorContext;
			this.module = module;
			if (oldOne.resource != null)
				this.resource = (EmbeddedResource)module.Resources[oldOne.module.Resources.IndexOf(oldOne.resource)];
			if (oldOne.vmAssemblyRef != null)
				this.vmAssemblyRef = module.ResolveAssemblyRef(oldOne.vmAssemblyRef.Rid);
		}

		public void Find() {
			resource = FindCsvmResource();
			vmAssemblyRef = FindVmAssemblyRef();
		}

		AssemblyRef FindVmAssemblyRef() {
			foreach (var memberRef in module.GetMemberRefs()) {
				var sig = memberRef.MethodSig;
				if (sig == null)
					continue;
				if (sig.RetType.GetElementType() != ElementType.Object)
					continue;
				if (sig.Params.Count != 2)
					continue;
				if (memberRef.Name != "RunMethod")
					continue;
				if (memberRef.FullName == "System.Object VMRuntime.Libraries.CSVMRuntime::RunMethod(System.String,System.Object[])")
					return memberRef.DeclaringType.Scope as AssemblyRef;
			}
			return null;
		}

		EmbeddedResource FindCsvmResource() {
			return DotNetUtils.GetResource(module, "_CSVM") as EmbeddedResource;
		}

		public bool Restore() {
			if (!Detected)
				return true;

			int oldIndent = Logger.Instance.IndentLevel;
			try {
				Restore2();
				return true;
			}
			catch {
				return false;
			}
			finally {
				Logger.Instance.IndentLevel = oldIndent;
			}
		}

		void Restore2() {
			Logger.v("Restoring CSVM methods");
			Logger.Instance.Indent();

			var opcodeDetector = GetVmOpCodeHandlerDetector();
			var csvmMethods = new CsvmDataReader(resource.Data).Read();
			var converter = new CsvmToCilMethodConverter(deobfuscatorContext, module, opcodeDetector);
			var methodPrinter = new MethodPrinter();
			foreach (var csvmMethod in csvmMethods) {
				var cilMethod = module.ResolveToken(csvmMethod.Token) as MethodDef;
				if (cilMethod == null)
					throw new ApplicationException(string.Format("Could not find method {0:X8}", csvmMethod.Token));
				converter.Convert(cilMethod, csvmMethod);
				Logger.v("Restored method {0:X8}", cilMethod.MDToken.ToInt32());
				PrintMethod(methodPrinter, cilMethod);
			}
			Logger.Instance.DeIndent();
			Logger.n("Restored {0} CSVM methods", csvmMethods.Count);
		}

		static void PrintMethod(MethodPrinter methodPrinter, MethodDef method) {
			const LoggerEvent dumpLogLevel = LoggerEvent.Verbose;
			if (Logger.Instance.IgnoresEvent(dumpLogLevel))
				return;

			Logger.Instance.Indent();

			Logger.v("Locals:");
			Logger.Instance.Indent();
			for (int i = 0; i < method.Body.Variables.Count; i++)
				Logger.v("#{0}: {1}", i, method.Body.Variables[i].Type);
			Logger.Instance.DeIndent();

			Logger.v("Code:");
			Logger.Instance.Indent();
			methodPrinter.Print(dumpLogLevel, method.Body.Instructions, method.Body.ExceptionHandlers);
			Logger.Instance.DeIndent();

			Logger.Instance.DeIndent();
		}

		VmOpCodeHandlerDetector GetVmOpCodeHandlerDetector() {
			var vmFilename = vmAssemblyRef.Name + ".dll";
			var vmModulePath = Path.Combine(Path.GetDirectoryName(module.Location), vmFilename);
			Logger.v("CSVM filename: {0}", vmFilename);

			var dataKey = "cs cached VmOpCodeHandlerDetector v1";
			var dict = (Dictionary<string, VmOpCodeHandlerDetector>)deobfuscatorContext.GetData(dataKey);
			if (dict == null)
				deobfuscatorContext.SetData(dataKey, dict = new Dictionary<string, VmOpCodeHandlerDetector>(StringComparer.OrdinalIgnoreCase));
			VmOpCodeHandlerDetector detector;
			if (dict.TryGetValue(vmModulePath, out detector))
				return detector;
			dict[vmModulePath] = detector = new VmOpCodeHandlerDetector(ModuleDefMD.Load(vmModulePath));

			detector.FindHandlers();
			Logger.v("CSVM opcodes:");
			Logger.Instance.Indent();
			for (int i = 0; i < detector.Handlers.Count; i++)
				Logger.v("{0:X4}: {1}", i, detector.Handlers[i].Name);
			Logger.Instance.DeIndent();

			return detector;
		}
	}
}
