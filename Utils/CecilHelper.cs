
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
		
		private static bool TypeMatches(TypeReference typeref, IType type)
		{
			if ((type) is ITypeReference)
			{
				ITypeReference ityperef = (ITypeReference) type;
				if (typeref.Namespace == ityperef.Namespace && typeref.Name.StartsWith(ityperef.Name))
				{
					if (typeref.DeclaringType != null&& (ityperef.Owner) is ITypeReference)
					{
						return TypeMatches(typeref.DeclaringType, ((ITypeReference) ityperef.Owner));
					}
					else
					{
						return true;
					}
				}
				return false;
			}
			else if ((type) is IGenericParameter)
			{
				IGenericParameter igenprm = (IGenericParameter) type;
				return typeref.Name == igenprm.Name;
			}
			else if ((type) is IGenericArgument)
			{
				IGenericArgument igenarg = (IGenericArgument) type;
				return TypeMatches(typeref, igenarg.Owner.GenericArguments[igenarg.Position]);
			}
			else if ((type) is IArrayType)
			{
				IArrayType iarrtyp = (IArrayType) type;
				return TypeMatches(typeref, iarrtyp.ElementType);
			}
			else if ((type) is IReferenceType)
			{
				IReferenceType iref = (IReferenceType) type;
				return TypeMatches(typeref, iref.ElementType);
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
			ITypeDeclaration classdec = (ITypeDeclaration) mdec.DeclaringType;
			IModule moddec = GetModule(classdec);
			
			AssemblyDefinition adef = DataManager.GetInstance().GetAssemblyDefinition(moddec.Location);
			TypeDefinition typedef = FindMatchingType(adef, classdec);
			
			if (typedef != null)
			{
				return FindMatchingMethod(typedef, mdec);
			}
			
			return null;
		}

        public static void ImportMethodBody(MethodDefinition source, MethodDefinition target)
        {
            // All i want is already in Mono.Cecil, but not accessible. Reflection is my friend

            Type helpertype = Type.GetType("Mono.Cecil.ReflectionHelper, Mono.Cecil");
            ConstructorInfo helperctor = helpertype.GetConstructor(new Type[] { typeof(ModuleDefinition) });
            object helper = helperctor.Invoke(new object[] { target.DeclaringType.Module });

            Type contexttype = Type.GetType("Mono.Cecil.ImportContext, Mono.Cecil");
            ConstructorInfo contextctor = contexttype.GetConstructor(new Type[] { helpertype });
            object context = contextctor.Invoke(new object[] { helper });

            Type mbodytype = typeof(Mono.Cecil.Cil.MethodBody);
            MethodInfo clonemethod = mbodytype.GetMethod("Clone",BindingFlags.Static | BindingFlags.NonPublic,null,new Type[] { mbodytype, typeof(MethodDefinition), contexttype },null);
            Mono.Cecil.Cil.MethodBody newBody = clonemethod.Invoke(null, new object[] { source.Body, target, context }) as Mono.Cecil.Cil.MethodBody;

            target.Body = newBody;
        }

        public static string GetTypeSignature(TypeReference source)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(source.Name);
            if (source.GenericParameters.Count > 0)
            {
                builder.Length -= source.GenericParameters.Count.ToString().Length + 1;
                builder.Append("<");
                for (int i = 0; i < source.GenericParameters.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(source.GenericParameters[i].Name);
                }
                builder.Append(">");
            }

            return builder.ToString();
        }
        
        public static string GetMethodSignature(MethodDefinition source)
        {
            StringBuilder builder = new StringBuilder();

            if (!source.IsConstructor)
            {
                builder.Append(source.ReturnType.ReturnType.ToString());
                builder.Append(" ");
            }
            builder.Append(source.Name);
            builder.Append("(");

            for (int i = 0; i < source.Parameters.Count; i++)
            {
                if (i > 0) 
                {
                    builder.Append(", ");
                }
                builder.Append(source.Parameters[i].ParameterType.ToString());
                builder.Append(" ");
                builder.Append(source.Parameters[i].Name);
            }
            builder.Append(")");
            builder = builder.Replace("System.Void", "void");
            builder = builder.Replace(MethodDefinition.Ctor, source.DeclaringType.Name);

            return builder.ToString();
        }
		#endregion
		
	}
	
}


