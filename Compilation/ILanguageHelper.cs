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
using System.Collections.Generic;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    /// <summary>
    /// Helper interface for code generation
    /// </summary>
    public interface ILanguageHelper
    {

        #region " Methods "
        /// <summary>
        /// Generate method signature 
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <returns>generated source code</returns>
        string GetMethodSignature(MethodDefinition mdef);

        /// <summary>
        /// Generate method
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <returns>generated source code</returns>
        string GetMethod(MethodDefinition mdef);

        /// <summary>
        /// Generate field
        /// </summary>
        /// <param name="fdef">Field definition</param>
        /// <returns>generated source code</returns>
        string GetField(FieldDefinition fdef);

        /// <summary>
        /// Generate type signature
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <returns>generated source code</returns>
        string GetTypeSignature(TypeDefinition tdef);

        /// <summary>
        /// Generate source code from method declaring type. All others
        /// methods are generated as stubs.
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <param name="references">Assembly references</param>
        /// <returns>generated source code</returns>
        string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references);
        #endregion

    }
}