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
using Mono.Cecil;

#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Helper for renaming existing items
	/// </summary>
	public static class RenameHelper
	{
		#region Methods

		/// <summary>
		/// Rename a type definition, nested or not
		/// </summary>
		/// <param name="tdef">Type definition</param>
		/// <param name="name">new name</param>
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

		/// <summary>
		/// Rename a member definition
		/// </summary>
		/// <param name="imdef">Member definition</param>
		/// <param name="name">new name</param>
		public static void RenameMemberDefinition(IMemberDefinition imdef, string name)
		{
			if (imdef is TypeDefinition)
				RenameTypeDefinition(imdef as TypeDefinition, name);
			else
				imdef.Name = name;
		}

		/// <summary>
		/// Rename a resource
		/// </summary>
		/// <param name="resource">Resource definition</param>
		/// <param name="name">new name</param>
		private static void RenameResource(Resource resource, string name)
		{
			resource.Name = name;
		}

		/// <summary>
		/// Rename an assembly name reference
		/// </summary>
		/// <param name="anref">Assembly name reference</param>
		/// <param name="name">new name</param>
		public static void RenameAssemblyNameReference(AssemblyNameReference anref, string name)
		{
			anref.Name = name;
		}

		/// <summary>
		/// Rename a module definition
		/// </summary>
		/// <param name="mdef">Module definition</param>
		/// <param name="name">new name</param>
		public static void RenameModuleDefinition(ModuleDefinition mdef, string name)
		{
			mdef.Name = name;
		}

		/// <summary>
		/// Rename an object
		/// </summary>
		/// <param name="obj">Type/Method/Property/Field/Event definition/Assembly Reference</param>
		/// <param name="name">new name</param>
		public static void Rename(Object obj, string name)
		{
			if (obj is IMemberDefinition)
				RenameMemberDefinition(obj as IMemberDefinition, name);
			else if (obj is AssemblyNameReference)
				RenameAssemblyNameReference(obj as AssemblyNameReference, name);
			else if (obj is Resource)
				RenameResource(obj as Resource, name);
			else if (obj is AssemblyDefinition)
				RenameAssemblyNameReference((obj as AssemblyDefinition).Name, name);
			else if (obj is ModuleDefinition)
				RenameModuleDefinition(obj as ModuleDefinition, name);
		}

		/// <summary>
		/// Retrieve a cecil-object name
		/// </summary>
		/// <param name="obj">Cecil object</param>
		/// <returns>the name</returns>
		public static string GetName(Object obj)
		{
			if (obj is TypeDefinition)
			{
				var tdef = obj as TypeDefinition;
				return tdef.IsNested ? tdef.Name : tdef.FullName;
			}

			if (obj is IMemberDefinition)
				return (obj as IMemberDefinition).Name;

			if (obj is AssemblyNameReference)
				return (obj as AssemblyNameReference).Name;

			if (obj is Resource)
				return (obj as Resource).Name;

			if (obj is AssemblyDefinition)
				return (obj as AssemblyDefinition).Name.Name;

			if (obj is ModuleDefinition)
				return (obj as ModuleDefinition).Name;

			return string.Empty;
		}

		#endregion
	}
}