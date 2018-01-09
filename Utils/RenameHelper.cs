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
using Mono.Cecil;

namespace Reflexil.Utils
{
	public static class RenameHelper
	{
		public static void RenameTypeDefinition(TypeDefinition tdef, string name)
		{
			var ns = string.Empty;
			if (name.Contains("."))
			{
				var offset = name.LastIndexOf(".", StringComparison.Ordinal);
				ns = name.Substring(0, offset);
				name = name.Substring(offset + 1);
			}

			if (!tdef.IsNested)
				tdef.Namespace = ns;

			tdef.Name = name;
		}

		public static void RenameMemberDefinition(IMemberDefinition imdef, string name)
		{
			var tdef = imdef as TypeDefinition;
			if (tdef != null)
				RenameTypeDefinition(tdef, name);
			else
				imdef.Name = name;
		}

		private static void RenameResource(Resource resource, string name)
		{
			resource.Name = name;
		}

		public static void RenameAssemblyNameReference(AssemblyNameReference anref, string name)
		{
			anref.Name = name;
		}

		public static void RenameModuleDefinition(ModuleDefinition mdef, string name)
		{
			mdef.Name = name;
		}

		public static void Rename(object obj, string name)
		{
			// ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
			if (obj is IMemberDefinition)
				RenameMemberDefinition((IMemberDefinition) obj, name);
			else if (obj is AssemblyNameReference)
				RenameAssemblyNameReference((AssemblyNameReference) obj, name);
			else if (obj is Resource)
				RenameResource((Resource) obj, name);
			else if (obj is AssemblyDefinition)
				RenameAssemblyNameReference(((AssemblyDefinition) obj).Name, name);
			else if (obj is ModuleDefinition)
				RenameModuleDefinition((ModuleDefinition) obj, name);
		}

		public static string GetName(object obj)
		{
			// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
			if (obj is TypeDefinition)
			{
				var tdef = (TypeDefinition) obj;
				return tdef.IsNested ? tdef.Name : tdef.FullName;
			}

			if (obj is IMemberDefinition)
				return ((IMemberDefinition) obj).Name;

			if (obj is AssemblyNameReference)
				return ((AssemblyNameReference) obj).Name;

			if (obj is Resource)
				return ((Resource) obj).Name;

			if (obj is AssemblyDefinition)
				return ((AssemblyDefinition) obj).Name.Name;

			if (obj is ModuleDefinition)
				return ((ModuleDefinition) obj).Name;
			// ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

			return string.Empty;
		}
	}
}