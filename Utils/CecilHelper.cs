
#region " Imports "
using System;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;
using Reflector.CodeModel;
#endregion

namespace Reflexil.Utils
{
	
	public sealed partial class CecilHelper
	{
		
		#region " Methods "
		private CecilHelper()
		{
        }

        #region " Reflector/Cecil searchs "
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

        private static bool MethodMatches(MethodDefinition mdef, IMethodDeclaration itype)
        {
            if (mdef.Name.StartsWith(itype.Name) && mdef.Parameters.Count == itype.Parameters.Count && TypeMatches(mdef.ReturnType.ReturnType, itype.ReturnType.Type))
            {
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

        private static MethodDefinition FindMatchingMethod(TypeDefinition typedef, IMethodDeclaration type)
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

        private static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDeclaration itype)
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

        public static MethodDefinition ReflectorMethodToCecilMethod(IMethodDeclaration mdec)
        {
            ITypeDeclaration classdec = (ITypeDeclaration)mdec.DeclaringType;
            IModule moddec = GetModule(classdec);

            AssemblyDefinition adef = DataManager.GetInstance().GetAssemblyDefinition(moddec.Location);
            TypeDefinition typedef = FindMatchingType(adef, classdec);

            if (typedef != null)
            {
                return FindMatchingMethod(typedef, mdec);
            }

            return null;
        }
        #endregion

        #region " Cecil/Cecil searchs "
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


