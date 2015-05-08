/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Properties;
using System.Text;
using Reflexil.Utils;

#endregion

namespace Reflexil.Plugins
{
	public abstract class BasePlugin : IPlugin, IComparer<OpCode>
	{
		private readonly List<OpCode> _allopcodes;
		private readonly Dictionary<string, string> _opcodesdesc;
		private readonly Bitmap _browserimages;
		private readonly Bitmap _barimages;
		protected readonly Dictionary<string, IAssemblyContext> Assemblycache;

		public abstract string HostApplication { get; }

		public static Bitmap ReflexilImage
		{
			get { return Resources.reflexil; }
		}

		public static bool ShowSymbols
		{
			get { return Settings.Default.ShowSymbols; }
		}

		public static bool AutoDetectObfuscators
		{
			get { return Settings.Default.AutoDetectObfuscators; }
		}

		public IPackage Package { get; private set; }

		protected BasePlugin(IPackage package)
		{
			Package = package;
			_allopcodes = new List<OpCode>();

			foreach (var finfo in typeof (OpCodes).GetFields())
				_allopcodes.Add((OpCode) (finfo.GetValue(null)));

			_allopcodes.Sort(this);
			_opcodesdesc = new Dictionary<string, string>();
			Assemblycache = new Dictionary<string, IAssemblyContext>();

			_browserimages = Resources.browser;
			_barimages = Resources.bar;

			ReloadOpcodesDesc(new MemoryStream(Encoding.ASCII.GetBytes(Resources.opcodes)));
		}

		public virtual string GetOpcodeDesc(OpCode opcode)
		{
			return _opcodesdesc.ContainsKey(opcode.Name) ? _opcodesdesc[opcode.Name] : string.Empty;
		}

		public virtual List<OpCode> GetAllOpCodes()
		{
			return _allopcodes;
		}

		public int Compare(OpCode x, OpCode y)
		{
			return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
		}

		private void ReloadOpcodesDesc(Stream stream)
		{
			const string opcode = "opcode";
			const string desc = "desc";

			var reader = new StreamReader(stream);
			var rex = new Regex(String.Format("^(?<{0}>.*)=(?<{1}>.*)$", opcode, desc));

			_opcodesdesc.Clear();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				if (line == null)
					continue;

				var items = rex.Split(line);
				_opcodesdesc.Add(items[rex.GroupNumberFromName(opcode)], items[rex.GroupNumberFromName(desc)]);
			}
		}

		public Bitmap GetAllBrowserImages()
		{
			return _browserimages;
		}

		public Bitmap GetAllBarImages()
		{
			return _barimages;
		}

		public bool IsAssemblyContextLoaded(string location)
		{
			location = Environment.ExpandEnvironmentVariables(location);
			return Assemblycache.ContainsKey(location);
		}

		public void RemoveAssemblyContext(string location)
		{
			location = Environment.ExpandEnvironmentVariables(location);
			if (Assemblycache.ContainsKey(location))
				Assemblycache.Remove(location);
		}

		public IAssemblyContext ReloadAssemblyContext(string location)
		{
			RemoveAssemblyContext(location);
			return GetAssemblyContext(location);
		}

		public abstract bool IsAssemblyNameReferenceHandled(object item);
		public abstract bool IsAssemblyDefinitionHandled(object item);
		public abstract bool IsTypeDefinitionHandled(object item);
		public abstract bool IsPropertyDefinitionHandled(object item);
		public abstract bool IsFieldDefinitionHandled(object item);
		public abstract bool IsModuleDefinitionHandled(object item);
		public abstract bool IsMethodDefinitionHandled(object item);
		public abstract bool IsEventDefinitionHandled(object item);
		public abstract bool IsEmbeddedResourceHandled(object item);
		public abstract bool IsLinkedResourceHandled(object item);
		public abstract bool IsAssemblyLinkedResourceHandled(object item);

		public abstract LinkedResource GetLinkedResource(object item);
		public abstract MethodDefinition GetMethodDefinition(object item);
		public abstract PropertyDefinition GetPropertyDefinition(object item);
		public abstract FieldDefinition GetFieldDefinition(object item);
		public abstract EventDefinition GetEventDefinition(object item);
		public abstract EmbeddedResource GetEmbeddedResource(object item);
		public abstract AssemblyLinkedResource GetAssemblyLinkedResource(object item);
		public abstract IAssemblyContext GetAssemblyContext(string location);

		public virtual AssemblyDefinition LoadAssembly(string location, bool readsymbols)
		{
			var parameters = new ReaderParameters { ReadSymbols = readsymbols, ReadingMode = ReadingMode.Deferred };
			var resolver = new ReflexilAssemblyResolver();
			try
			{
				return resolver.ReadAssembly(location, parameters);
			}
			catch (Exception)
			{
				// perhaps pdb file is not found, just ignore this time
				if (!readsymbols)
					throw;

				parameters.ReadSymbols = false;
				return resolver.ReadAssembly(location, parameters);
			}
		}

		public IAssemblyContext GetAssemblyContext<T>(string location) where T : IAssemblyContext, new()
		{
			location = Environment.ExpandEnvironmentVariables(location);

			if (Assemblycache.ContainsKey(location))
				return Assemblycache[location];

			try
			{
				// Check for obfuscators
				if (AutoDetectObfuscators)
				{
					AssemblyHelper.SearchObfuscator(location, true);
				}
				AssemblyDefinition asmdef = LoadAssembly(location, ShowSymbols);
				IAssemblyContext context = new T();
				context.AssemblyDefinition = asmdef;
				Assemblycache.Add(location, context);
			}
			catch (Exception)
			{
				return null;
			}
			return Assemblycache[location];
		}

		public abstract AssemblyNameReference GetAssemblyNameReference(object item);
		public abstract AssemblyDefinition GetAssemblyDefinition(object item);
		public abstract TypeDefinition GetTypeDefinition(object item);
		public abstract ModuleDefinition GetModuleDefinition(object item);
		public abstract IAssemblyContext GetAssemblyContext(object item);

	}
}