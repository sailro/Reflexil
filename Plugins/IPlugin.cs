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

using System.Collections.Generic;
using System.Drawing;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Reflexil.Plugins
{
	public interface IPlugin
	{
		IPackage Package { get; }
		string HostApplication { get; }

		Bitmap GetAllBrowserImages();
		Bitmap GetAllBarImages();
		List<OpCode> GetAllOpCodes();
		string GetOpcodeDesc(OpCode opcode);
		bool IsAssemblyContextLoaded(string location);
		IAssemblyContext GetAssemblyContext(string location);
		IAssemblyContext GetAssemblyContext(object item);
		IAssemblyContext ReloadAssemblyContext(string location);

		AssemblyDefinition LoadAssembly(string location, bool readsymbols);

		bool IsAssemblyNameReferenceHandled(object item);
		bool IsAssemblyDefinitionHandled(object item);
		bool IsTypeDefinitionHandled(object item);
		bool IsModuleDefinitionHandled(object item);
		bool IsMethodDefinitionHandled(object item);
		bool IsPropertyDefinitionHandled(object item);
		bool IsFieldDefinitionHandled(object item);
		bool IsEventDefinitionHandled(object item);
		bool IsEmbeddedResourceHandled(object item);
		bool IsAssemblyLinkedResourceHandled(object item);
		bool IsLinkedResourceHandled(object item);

		AssemblyNameReference GetAssemblyNameReference(object item);
		AssemblyDefinition GetAssemblyDefinition(object item);
		TypeDefinition GetTypeDefinition(object item);
		PropertyDefinition GetPropertyDefinition(object item);
		FieldDefinition GetFieldDefinition(object item);
		EventDefinition GetEventDefinition(object item);
		EmbeddedResource GetEmbeddedResource(object item);
		AssemblyLinkedResource GetAssemblyLinkedResource(object item);
		LinkedResource GetLinkedResource(object item);
		MethodDefinition GetMethodDefinition(object item);
		ModuleDefinition GetModuleDefinition(object item);
	}
}