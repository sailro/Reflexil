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

using System.Linq;
using Mono.Cecil;
using Reflector.CodeModel;
using System.Collections.Generic;

namespace Reflexil.Plugins.Reflector
{
	internal static class ReflectorHelper
	{
		private static bool IsSameName(string cecilobject, string reflectorobject)
		{
			if (cecilobject != null && reflectorobject != null)
				return cecilobject.StartsWith(reflectorobject);

			return false;
		}

		private static bool TypeMatches(TypeDefinition tdef, ITypeDeclaration itdef)
		{
			return TypeMatches(tdef as TypeReference, itdef) && TypeMatches(tdef.BaseType, itdef.BaseType);
		}

		private static bool TypeMatches(TypeReference typeref, IType type)
		{
			// ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
			if (type is ITypeDeclaration && typeref is TypeDefinition)
			{
				var itypedec = (ITypeDeclaration) type;
				var tdef = (TypeDefinition) typeref;

				if (itypedec.Fields.Count != tdef.Fields.Count)
					return false;

				if (itypedec.Properties.Count != tdef.Properties.Count)
					return false;

				if (itypedec.Methods.Count != tdef.Methods.Count)
					return false;

				if (itypedec.Events.Count != tdef.Events.Count)
					return false;

				if (itypedec.NestedTypes.Count != tdef.NestedTypes.Count)
					return false;
			}

			if (type is ITypeReference)
			{
				var ityperef = (ITypeReference) type;
				if (typeref.Namespace != ityperef.Namespace || !IsSameName(typeref.Name, ityperef.Name))
					return false;

				if (typeref.DeclaringType != null && (ityperef.Owner) is ITypeReference)
					return TypeMatches(typeref.DeclaringType, (ITypeReference) ityperef.Owner);

				return true;
			}

			if (type is IGenericParameter)
			{
				var igenprm = (IGenericParameter) type;
				return typeref.Name.StartsWith(igenprm.Name);
			}

			if (type is IGenericArgument)
			{
				var igenarg = (IGenericArgument) type;
				return TypeMatches(typeref, igenarg.Owner.GenericArguments[igenarg.Position]);
			}

			if (type is IArrayType && typeref is ArrayType)
			{
				var iarrtyp = (IArrayType) type;
				return TypeMatches(((ArrayType) typeref).ElementType, iarrtyp.ElementType);
			}

			if (type is IReferenceType && typeref is ByReferenceType)
			{
				var iref = (IReferenceType) type;
				return TypeMatches(((ByReferenceType) typeref).ElementType, iref.ElementType);
			}

			if (!(type is IPointerType) || !(typeref is PointerType))
				return false;

			var ipt = (IPointerType) type;
			return TypeMatches(((PointerType) typeref).ElementType, ipt.ElementType);
			// ReSharper restore CanBeReplacedWithTryCastAndCheckForNull
		}

		private static bool MethodMatches(MethodDefinition mdef, IMethodDeclaration imdec)
		{
			if (mdef == null || imdec == null)
				return false;

			if (!IsSameName(mdef.Name, imdec.Name) || mdef.Parameters.Count != imdec.Parameters.Count ||
			    !TypeMatches(mdef.ReturnType, imdec.ReturnType.Type))
				return false;

			// Compatible with code alteration feature !!!
			// Called only the first time then in cache, so even if code is altered, this will work
			var methodBody = imdec.Body as IMethodBody;
			if (methodBody != null && (mdef.Body != null))
			{
				if (methodBody.Instructions.Count != mdef.Body.Instructions.Count)
					return false;
			}
			else if ((imdec.Body != null) ^ (mdef.Body != null))
			{
				// abstract vs default method 
				return false;
			}

			// Same than above for parameter alteration
			for (var i = 0; i <= mdef.Parameters.Count - 1; i++)
			{
				if (!TypeMatches(mdef.Parameters[i].ParameterType, imdec.Parameters[i].ParameterType))
					return false;
			}
			return true;
		}

		private static bool PropertyMatches(PropertyDefinition pdef, IPropertyDeclaration pdec)
		{
			// Compatible with alteration feature !!!
			// Called only the first time then in cache, so even if code is altered, this will work
			// No need to check the declaring type, if we are here, they are in sync
			if (pdef == null || pdec == null)
				return false;

			if (!IsSameName(pdef.Name, pdec.Name) || pdef.Parameters.Count != pdec.Parameters.Count ||
			    !TypeMatches(pdef.PropertyType, pdec.PropertyType))
				return false;

			if (pdef.GetMethod != null)
			{
				if (!MethodMatches(pdef.GetMethod, pdec.GetMethod as IMethodDeclaration))
					return false;
			}
			else
			{
				if (pdec.GetMethod != null)
					return false;
			}

			if (pdef.SetMethod != null)
			{
				if (!MethodMatches(pdef.SetMethod, pdec.SetMethod as IMethodDeclaration))
					return false;
			}
			else
			{
				if (pdec.SetMethod != null)
					return false;
			}

			return true;
		}

		private static bool FieldMatches(FieldDefinition fdef, IFieldDeclaration fdec)
		{
			// Compatible with alteration feature !!!
			// Called only the first time then in cache, so even if code is altered, this will work
			// No need to check the declaring type, if we are here, they are in sync
			return (fdef != null)
			       && (fdec != null)
			       && fdef.Name.Equals(fdec.Name);
		}

		private static bool EventMatches(EventDefinition edef, IEventDeclaration edec)
		{
			// Compatible with alteration feature !!!
			// Called only the first time then in cache, so even if code is altered, this will work
			// No need to check the declaring type, if we are here, they are in sync
			if (edef == null || edec == null)
				return false;

			if (IsSameName(edef.Name, edec.Name) && TypeMatches(edef.EventType, edec.EventType))
			{
				return MethodMatches(edef.AddMethod, edec.AddMethod as IMethodDeclaration)
				       && MethodMatches(edef.RemoveMethod, edec.RemoveMethod as IMethodDeclaration);
			}
			return false;
		}

		internal static MethodDefinition FindMatchingMethod(TypeDefinition typedef, IMethodDeclaration type)
		{
			return typedef.Methods
				.Where(mdef => MethodMatches(mdef, type))
				.OrderBy(mdef => mdef.Name)
				.FirstOrDefault();
		}

		internal static PropertyDefinition FindMatchingProperty(TypeDefinition typedef, IPropertyDeclaration pdec)
		{
			return typedef
				.Properties
				.Where(pdef => PropertyMatches(pdef, pdec))
				.OrderBy(pdef => pdef.Name)
				.FirstOrDefault();
		}

		public static FieldDefinition FindMatchingField(TypeDefinition typedef, IFieldDeclaration fdec)
		{
			return typedef.Fields.FirstOrDefault(fdef => FieldMatches(fdef, fdec));
		}

		public static EventDefinition FindMatchingEvent(TypeDefinition typedef, IEventDeclaration edec)
		{
			return typedef
				.Events
				.Where(edef => EventMatches(edef, edec))
				.OrderBy(edef => edef.Name)
				.FirstOrDefault();
		}

		internal static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDeclaration itype)
		{
			var fullname = itype.Name;

			if (itype.Namespace != string.Empty)
				fullname = itype.Namespace + "." + fullname;

			if (itype.GenericArguments.Count > 0)
				fullname += string.Format("`{0}", itype.GenericArguments.Count);

			if (adef == null)
				return null;

			var result = adef.MainModule.GetType(fullname);
			return result ?? FindMatchingType(adef.MainModule.Types, itype);
		}

		private static TypeDefinition FindMatchingType(IEnumerable<TypeDefinition> collection, ITypeDeclaration itype)
		{
			foreach (var retType in collection)
			{
				if (TypeMatches(retType, itype))
					return retType;

				var result = FindMatchingType(retType.NestedTypes, itype);
				if (result != null)
					return result;
			}
			return null;
		}

		internal static Resource FindMatchingResource(AssemblyDefinition adef, IResource alres)
		{
			return adef.MainModule.Resources.FirstOrDefault(item => item.Name == alres.Name);
		}

		public static AssemblyNameReference FindMatchingAssemblyReference(AssemblyDefinition adef, IAssemblyReference anref)
		{
			return adef.MainModule.AssemblyReferences.FirstOrDefault(item => item.ToString() == anref.ToString());
		}
	}
}