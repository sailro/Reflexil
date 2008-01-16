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
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflector.CodeModel;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Cecil object model helper.
    /// </summary>
	public static partial class CecilHelper
	{
		
		#region " Methods "

        #region " Reflector/Cecil searchs "
        /// <summary>
        /// Determines whether two types are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="typeref">Cecil type reference</param>
        /// <param name="type">Reflector type reference</param>
        /// <returns>true if equivalent</returns>
        private static bool TypeMatches(TypeReference typeref, IType type)
        {
            if (type is ITypeReference)
            {
                ITypeReference ityperef = (ITypeReference)type;
                if (typeref.Namespace == ityperef.Namespace && typeref.Name.StartsWith(ityperef.Name))
                {
                    if (typeref.DeclaringType != null && (ityperef.Owner) is ITypeReference)
                    {
                        return TypeMatches(typeref.DeclaringType, ((ITypeReference)ityperef.Owner));
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (type is IGenericParameter)
            {
                IGenericParameter igenprm = (IGenericParameter)type;
                return typeref.Name.StartsWith(igenprm.Name);
            }
            else if (type is IGenericArgument)
            {
                IGenericArgument igenarg = (IGenericArgument)type;
                return TypeMatches(typeref, igenarg.Owner.GenericArguments[igenarg.Position]);
            }
            else if ((type is IArrayType) && (typeref is ArrayType))
            {
                IArrayType iarrtyp = (IArrayType)type;
                return TypeMatches(typeref, iarrtyp.ElementType);
            }
            else if ((type is IReferenceType) && (typeref is ReferenceType))
            {
                IReferenceType iref = (IReferenceType)type;
                return TypeMatches(typeref, iref.ElementType);
            }
            else if ((type is IPointerType) && (typeref is PointerType))
            {
                IPointerType ipt = (IPointerType)type;
                return TypeMatches(typeref, ipt.ElementType);
            }
            return false;
        }

        /// <summary>
        /// Determines whether two methods are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="mdef">Cecil method definition</param>
        /// <param name="itype">Reflector method declaration</param>
        /// <returns>true if equivalent</returns>
        private static bool MethodMatches(MethodDefinition mdef, IMethodDeclaration itype)
        {
            if (mdef.Name.StartsWith(itype.Name) && mdef.Parameters.Count == itype.Parameters.Count && TypeMatches(mdef.ReturnType.ReturnType, itype.ReturnType.Type))
            {
                // Compatible with code alteration feature !!!
                // Called only the first time then in cache, so even if code is altered, this will work
                if ((itype.Body != null) && (mdef.Body != null))
                {
                    if ((itype.Body as IMethodBody).Instructions.Count != mdef.Body.Instructions.Count)
                    {
                        return false;
                    }
                }
                
                // Same than above for parameter alteration
                for (int i = 0; i <= mdef.Parameters.Count - 1; i++)
                {
                    if (!TypeMatches(mdef.Parameters[i].ParameterType, itype.Parameters[i].ParameterType))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find a matching method in the Cecil object model for a given Reflector method 
        /// </summary>
        /// <param name="typedef">Cecil type definition</param>
        /// <param name="type">Reflector method declaration</param>
        /// <returns>Cecil method definition (null if not found)</returns>
        public static MethodDefinition FindMatchingMethod(TypeDefinition typedef, IMethodDeclaration type)
        {
            foreach (MethodDefinition retMethod in typedef.Methods)
            {
                if (MethodMatches(retMethod, type))
                {
                    return retMethod;
                }
            }

            foreach (MethodDefinition retMethod in typedef.Constructors)
            {
                if (MethodMatches(retMethod, type))
                {
                    return retMethod;
                }
            }
            return null;
        }

        /// <summary>
        /// Find a matching type in the Cecil object model for a given Reflector type 
        /// </summary>
        /// <param name="adef">Cecil assembly definition</param>
        /// <param name="itype">Reflector type declaration</param>
        /// <returns>Cecil type definition (null if not found)</returns>
        public static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDeclaration itype)
        {
            string fullname = itype.Name;

            if (itype.Namespace != string.Empty)
            {
                fullname = itype.Namespace + "." + fullname;
            }
            if (itype.GenericArguments.Count > 0)
            {
                fullname += String.Format("`{0}", itype.GenericArguments.Count);
            }

            if (adef != null)
            {
                if (adef.MainModule.Types.Contains(fullname))
                {
                    return adef.MainModule.Types[fullname];
                }

                foreach (TypeDefinition retType in adef.MainModule.Types)
                {
                    if (TypeMatches(retType, itype))
                    {
                        return retType;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get Reflector module from a given Reflector type
        /// </summary>
        /// <param name="itype">Reflector type</param>
        /// <returns>Reflector module (null if not found)</returns>
        private static IModule GetModule(ITypeReference itype)
        {
            if ((itype.Owner) is IModule)
            {
                return ((IModule)itype.Owner);
            }
            else if ((itype.Owner) is ITypeReference)
            {
                return GetModule((ITypeReference)itype.Owner);
            }
            return null;
        }

        /// <summary>
        /// Retrieve the matching method in the Cecil object model
        /// </summary>
        /// <param name="mdec">Reflector method declaration</param>
        /// <returns>Cecil method definition (null if not found)</returns>
        public static MethodDefinition ReflectorMethodToCecilMethod(IMethodDeclaration mdec)
        {
            ITypeDeclaration classdec = (ITypeDeclaration)mdec.DeclaringType;
            IModule moddec = GetModule(classdec);

            AssemblyContext context = DataManager.GetInstance().GetAssemblyContext(moddec.Location);
            if (context != null)
            {
                return context.GetMethodDefinition(mdec);
            }

            return null;
        }

        /// <summary>
        /// Retrieve the matching type in the Cecil object model
        /// </summary>
        /// <param name="tdec">Reflector type declaration</param>
        /// <returns>Cecil type definition (null if not found)</returns>
        public static TypeDefinition ReflectorTypeToCecilType(ITypeDeclaration tdec)
        {
            IModule moddec = GetModule(tdec);

            AssemblyContext context = DataManager.GetInstance().GetAssemblyContext(moddec.Location);
            if (context != null)
            {
                return FindMatchingType(context.AssemblyDefinition, tdec);
            }

            return null;
        }

        /// <summary>
        /// Remove the Strong Name of the given assembly
        /// </summary>
        /// <param name="asmdef">Strong Name assembly</param>
        public static void RemoveStrongName(AssemblyDefinition asmdef) {
            asmdef.Name.PublicKey = new byte[0];
            asmdef.Name.PublicKeyToken = new byte[0];
            asmdef.Name.Flags = AssemblyFlags.SideBySideCompatible;
        }

        /// <summary>
        /// Remove the Strong Name Reference of the given assembly name
        /// </summary>
        /// <param name="andef">Strong Name assembly</param>
        public static void RemoveStrongNameReference(AssemblyNameReference andef)
        {
            andef.PublicKeyToken = new byte[0];
        }
        #endregion

        #region " Cecil/Cecil searchs "
        /// <summary>
        /// Find a similar field in the given type definition 
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="fref">Field reference</param>
        /// <returns>Field definition (or null if not found)</returns>
        public static FieldDefinition FindMatchingField(TypeDefinition tdef, FieldReference fref)
        {
            foreach (FieldDefinition fdef in tdef.Fields)
            {
                if ((fdef.Name == fref.Name) && (fdef.FieldType == fref.FieldType))
                {
                    return fdef;
                }
            }
            return null;
        }

        /// <summary>
        /// Determines if two assembly name references matches
        /// </summary>
        /// <param name="anref1">an assembly name reference</param>
        /// <param name="anref2">an assembly name reference to compare</param>
        /// <returns>true if matches</returns>
        public static bool ReferenceMatches(AssemblyNameReference anref1, AssemblyNameReference anref2)
        {
            // Skip Key
            return ((anref1.Name == anref2.Name) && (anref1.Version.ToString(2).CompareTo(anref2.Version.ToString(2)) == 0) && (anref1.Culture == anref2.Culture));
        }

        /// <summary>
        /// Determines if two methods matches
        /// </summary>
        /// <param name="mref1">a method</param>
        /// <param name="mref2">a method to compare</param>
        /// <returns>true if matches</returns>
        private static bool MethodMatches(MethodReference mref1, MethodReference mref2)
        {
            if ((mref1.Name == mref2.Name) && (mref1.Parameters.Count == mref2.Parameters.Count) && (mref1.ReturnType.ReturnType.FullName == mref2.ReturnType.ReturnType.FullName))
            {
                for (int i = 0; i <= mref1.Parameters.Count - 1; i++)
                {
                    if (mref1.Parameters[i].ParameterType.FullName != mref2.Parameters[i].ParameterType.FullName)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find a similar method in the given type definition 
        /// </summary>
        /// <param name="tdef">Type definition</param>
        /// <param name="mref">Method reference</param>
        /// <returns>Method definition (or null if not found)</returns>
        public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, MethodReference mref)
        {
            foreach (MethodDefinition mdef in tdef.Methods)
            {
                if (MethodMatches(mdef, mref))
                {
                    return mdef;
                }
            }

            foreach (MethodDefinition mdef in tdef.Constructors)
            {
                if (MethodMatches(mdef, mref))
                {
                    return mdef;
                }
            }
            return null;
        }
        #endregion

        #region " Method body import "
        /// <summary>
        /// Clone a source method body to a target method definition.
        /// Field/Method/Type references are corrected
        /// </summary>
        /// <param name="source">Source method definition</param>
        /// <param name="target">Target method definition</param>
        public static void ImportMethodBody(MethodDefinition source, MethodDefinition target)
        {
            // All i want is already in Mono.Cecil, but not accessible. Reflection is my friend
            object context = new ImportContext(new DefaultImporter(target.DeclaringType.Module));
            Type contexttype = context.GetType();

            Type mbodytype = typeof(Mono.Cecil.Cil.MethodBody);
            MethodInfo clonemethod = mbodytype.GetMethod("Clone", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { mbodytype, typeof(MethodDefinition), contexttype }, null);
            Mono.Cecil.Cil.MethodBody newBody = clonemethod.Invoke(null, new object[] { source.Body, target, context }) as Mono.Cecil.Cil.MethodBody;

            target.Body = newBody;

            // Then correct fields and methods references
            foreach (Instruction ins in newBody.Instructions)
            {
                if (ins.Operand is TypeReference)
                {
                    TypeReference tref = ins.Operand as TypeReference;
                    if (tref.FullName == source.DeclaringType.FullName)
                    {
                        ins.Operand = target.DeclaringType;
                    }

                } else if (ins.Operand is FieldReference)
                {
                    FieldReference fref = ins.Operand as FieldReference;
                    if (fref.DeclaringType.FullName == source.DeclaringType.FullName)
                    {
                        ins.Operand = FindMatchingField(target.DeclaringType as TypeDefinition, fref);
                    }
                } else if (ins.Operand is MethodReference)
                {
                    MethodReference mref = ins.Operand as MethodReference;
                    if (mref.DeclaringType.FullName == source.DeclaringType.FullName)
                    {
                        ins.Operand = FindMatchingMethod(target.DeclaringType as TypeDefinition, mref);
                    }
                }
            }
        }
        #endregion

		#endregion
		
	}
}


