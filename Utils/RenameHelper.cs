/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Helper for renaming existing items
    /// </summary>
	public static class RenameHelper
    {

        #region " Methods "
        /// <summary>
        /// Rename a type definition, nested or not
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="name">new name</param>
        public static void RenameTypeDefinition(TypeDefinition tdef, string name)
        {
            string ns = string.Empty;
            if (name.Contains("."))
            {
                int offset = name.LastIndexOf(".");
                ns = name.Substring(0, offset);
                name = name.Substring(offset + 1);
            }

            if (!tdef.IsNested)
            {
                tdef.Namespace = ns;
            }
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
            {
                RenameTypeDefinition(imdef as TypeDefinition, name);
            }
            else
            {
                imdef.Name = name;
            }
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
        /// Rename an object
        /// </summary>
        /// <param name="obj">Type/Method/Property/Field/Event definition/Assembly Reference</param>
        public static void Rename(Object obj, string name)
        {
            if (obj is IMemberDefinition)
            {
                RenameMemberDefinition(obj as IMemberDefinition, name);
            }
            else if (obj is AssemblyNameReference)
            {
                RenameAssemblyNameReference(obj as AssemblyNameReference, name);
            }
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
                TypeDefinition tdef = obj as TypeDefinition;
                if (tdef.IsNested)
                {
                    return tdef.Name;
                }
                else
                {
                    return tdef.FullName;
                }
            }
            else if (obj is IMemberDefinition)
            {
                return (obj as IMemberDefinition).Name;
            }
            else if (obj is AssemblyNameReference)
            {
                return (obj as AssemblyNameReference).Name;
            }
            return string.Empty;
        }
        #endregion

    }
}
