/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using System.Linq;
using Mono.Cecil;
using Reflector.CodeModel;
using System.Collections.Generic;
#endregion

namespace Reflexil.Plugins.Reflector
{
    /// <summary>
    /// Reflector / Cecil object model helper.
    /// </summary>
    class ReflectorHelper
    {

        #region " Reflector/Cecil searchs "

        #region " Private Matchers "
        /// <summary>
        /// Improve this!
        /// </summary>
        /// <param name="cecilobject">Cecil object name</param>
        /// <param name="reflectorobject">Reflector object name</param>
        /// <returns>true if similar</returns>
        private static bool IsSameName(string cecilobject, string reflectorobject)
        {
            if (cecilobject != null && reflectorobject != null)
            {
                return cecilobject.StartsWith(reflectorobject);
            }
            return false;
        }

        /// <summary>
        /// Determines whether two types definition are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="tdef">Cecil type definition</param>
        /// <param name="itdef">Reflector type declaration</param>
        /// <returns>true if equivalent</returns>
        private static bool TypeMatches(TypeDefinition tdef, ITypeDeclaration itdef)
        {
            if (TypeMatches(tdef as TypeReference, itdef as ITypeDeclaration))
            {
                if (TypeMatches(tdef.BaseType, itdef.BaseType)) {
					// TODO - more checks are needed
                    return true;
                }
            }
            return false;
        }

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
                var ityperef = (ITypeReference)type;
                if (typeref.Namespace == ityperef.Namespace && IsSameName(typeref.Name, ityperef.Name))
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
                return TypeMatches(((ArrayType)typeref).ElementType, iarrtyp.ElementType);
            }
            else if ((type is IReferenceType) && (typeref is ByReferenceType))
            {
                IReferenceType iref = (IReferenceType)type;
                return TypeMatches(((ByReferenceType)typeref).ElementType, iref.ElementType);
            }
            else if ((type is IPointerType) && (typeref is PointerType))
            {
                IPointerType ipt = (IPointerType)type;
                return TypeMatches(((PointerType)typeref).ElementType, ipt.ElementType);
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
            if (mdef != null && itype != null)
            {
                if (IsSameName(mdef.Name, itype.Name) && mdef.Parameters.Count == itype.Parameters.Count && TypeMatches(mdef.ReturnType, itype.ReturnType.Type))
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
                    else if ( (itype.Body != null) ^ (mdef.Body != null) )
                    {
                        // abstract vs default method 
                        return false;
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
            }
            return false;
        }

        /// <summary>
        /// Determines whether two properties are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="pdef">Cecil property definition</param>
        /// <param name="pdec">Reflector property declaration</param>
        /// <returns>true if equivalent</returns>
        private static bool PropertyMatches(PropertyDefinition pdef, IPropertyDeclaration pdec)
        {
            // Compatible with alteration feature !!!
            // Called only the first time then in cache, so even if code is altered, this will work
            // No need to check the declaring type, if we are here, they are in sync
            if (pdef != null && pdec != null)
            {
                if (IsSameName(pdef.Name, pdec.Name) && pdef.Parameters.Count == pdec.Parameters.Count && TypeMatches(pdef.PropertyType, pdec.PropertyType))
                {
                    if (pdef.GetMethod != null)
                    {
                        if (!MethodMatches(pdef.GetMethod, pdec.GetMethod as IMethodDeclaration))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (pdec.GetMethod != null)
                        {
                            return false;
                        }
                    }

                    if (pdef.SetMethod != null)
                    {
                        if (!MethodMatches(pdef.SetMethod, pdec.SetMethod as IMethodDeclaration))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (pdec.SetMethod != null)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether two fields are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="pdef">Cecil field definition</param>
        /// <param name="pdec">Reflector field declaration</param>
        /// <returns>true if equivalent</returns>
        private static bool FieldMatches(FieldDefinition fdef, IFieldDeclaration fdec)
        {
            // Compatible with alteration feature !!!
            // Called only the first time then in cache, so even if code is altered, this will work
            // No need to check the declaring type, if we are here, they are in sync
            return    (fdef != null) 
                   && (fdec != null)
                   && (fdef.Name.Equals(fdec.Name));
        }

        /// <summary>
        /// Determines whether two events are equivalent (Cecil/Reflector)
        /// </summary>
        /// <param name="edef">Cecil event definition</param>
        /// <param name="edec">Reflector event declaration</param>
        /// <returns>true if equivalent</returns>
        private static bool EventMatches(EventDefinition edef, IEventDeclaration edec)
        {
            // Compatible with alteration feature !!!
            // Called only the first time then in cache, so even if code is altered, this will work
            // No need to check the declaring type, if we are here, they are in sync
            if (edef != null && edec != null)
            {
                if (IsSameName(edef.Name, edec.Name) && TypeMatches(edef.EventType, edec.EventType))
                {
                    return MethodMatches(edef.AddMethod, edec.AddMethod as IMethodDeclaration)
                        && MethodMatches(edef.RemoveMethod, edec.RemoveMethod as IMethodDeclaration);
                }
            }
            return false;
        }
        #endregion

        #region " Internal Finders "
        /// <summary>
        /// Find a matching method in the Cecil object model for a given Reflector method 
        /// </summary>
        /// <param name="typedef">Cecil type definition</param>
        /// <param name="type">Reflector method declaration</param>
        /// <returns>Cecil method definition (null if not found)</returns>
        internal static MethodDefinition FindMatchingMethod(TypeDefinition typedef, IMethodDeclaration type)
        {
            foreach (MethodDefinition retMethod in typedef.Methods)
            {
                if (MethodMatches(retMethod, type))
                {
                    return retMethod;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a matching property in the Cecil object model for a given Reflector property 
        /// </summary>
        /// <param name="typedef">Cecil type definition</param>
        /// <param name="pdec">Reflector property declaration</param>
        /// <returns>Cecil property definition (null if not found)</returns>
        internal static PropertyDefinition FindMatchingProperty(TypeDefinition typedef, IPropertyDeclaration pdec)
        {
            foreach (PropertyDefinition pdef in typedef.Properties)
            {
                if (PropertyMatches(pdef, pdec))
                {
                    return pdef;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a matching field in the Cecil object model for a given Reflector field 
        /// </summary>
        /// <param name="typedef">Cecil type definition</param>
        /// <param name="fdec">Reflector field declaration</param>
        /// <returns>Cecil field definition (null if not found)</returns>
        public static FieldDefinition FindMatchingField(TypeDefinition typedef, IFieldDeclaration fdec)
        {
            foreach (FieldDefinition fdef in typedef.Fields)
            {
                if (FieldMatches(fdef, fdec))
                {
                    return fdef;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a matching event in the Cecil object model for a given Reflector event 
        /// </summary>
        /// <param name="typedef">Cecil type definition</param>
        /// <param name="edec">Reflector event declaration</param>
        /// <returns>Cecil event definition (null if not found)</returns>
        public static EventDefinition FindMatchingEvent(TypeDefinition typedef, IEventDeclaration edec)
        {
            foreach (EventDefinition edef in typedef.Events)
            {
                if (EventMatches(edef, edec))
                {
                    return edef;
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
        internal static TypeDefinition FindMatchingType(AssemblyDefinition adef, ITypeDeclaration itype)
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
                TypeDefinition result = adef.MainModule.GetType(fullname);
                if (result != null)
                    return result;

                return FindMatchingType(adef.MainModule.Types, itype);
            }

            return null;
        }

        internal static TypeDefinition FindMatchingType(IEnumerable<TypeDefinition> collection, ITypeDeclaration itype)
        {
            foreach (TypeDefinition retType in collection)
            {
                if (TypeMatches(retType, itype))
                {
                    return retType;
                }

                TypeDefinition result = FindMatchingType(retType.NestedTypes, itype);
                if (result != null)
                    return result;

            }
            return null;
        }

        internal static EmbeddedResource FindMatchingResource(AssemblyDefinition adef, IEmbeddedResource eres)
        {
            return (from resource in adef.MainModule.Resources
                    where resource is EmbeddedResource && resource.Name.Equals(eres.Name)
                    select resource as EmbeddedResource).FirstOrDefault();
        }

        internal static AssemblyLinkedResource FindMatchingResource(AssemblyDefinition adef, IResource alres)
        {
            return (from resource in adef.MainModule.Resources
                    where resource is AssemblyLinkedResource && resource.Name.Equals(alres.Name)
                    select resource as AssemblyLinkedResource).FirstOrDefault();
        }

        internal static LinkedResource FindMatchingResource(AssemblyDefinition adef, IFileResource lres)
        {
            return (from resource in adef.MainModule.Resources
                    where resource is LinkedResource && resource.Name.Equals(lres.Name)
                    select resource as LinkedResource).FirstOrDefault();
        }
        #endregion

        /// <summary>
        /// Get Reflector module from a given Reflector type
        /// </summary>
        /// <param name="itype">Reflector type</param>
        /// <returns>Reflector module (null if not found)</returns>
        public static IModule GetModule(ITypeReference itype)
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
        /// Retrieve the matching assembly definition reference in the Cecil object model
        /// </summary>
        /// <param name="aloc">Reflector assembly location</param>
        /// <returns>Cecil assembly assembly definition (null if not found)</returns>
        public static AssemblyDefinition ReflectorAssemblyLocationToCecilAssemblyDefinition(IAssemblyLocation aloc)
        {
            IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(aloc.Location);
            if (context != null)
            {
                return context.AssemblyDefinition;
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

            IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(moddec.Location);
            if (context != null)
            {
                return FindMatchingType(context.AssemblyDefinition, tdec);
            }

            return null;
        }

        private static TCecil ReflectorToCecilMember<TCecil, TReflector>(TReflector item, Func<ReflectorAssemblyContext, object, TCecil> finder) where TReflector : IMemberDeclaration 
        {
            ITypeDeclaration classdec = (ITypeDeclaration)item.DeclaringType;
            IModule moddec = GetModule(classdec);

            ReflectorAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(moddec.Location) as ReflectorAssemblyContext;
            if (context != null)
            {
                return finder(context, item);
            }

            return default(TCecil);
        }

        /// <summary>
        /// Retrieve the matching assembly name reference in the Cecil object model
        /// </summary>
        /// <param name="aref">Reflector assembly reference</param>
        /// <returns>Cecil assembly name reference (null if not found)</returns>
        public static AssemblyNameReference ReflectorAssemblyReferenceToCecilAssemblyNameReference(IAssemblyReference aref)
        {
            IModule moddec = aref.Context;

            ReflectorAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(moddec.Location) as ReflectorAssemblyContext;
            if (context != null)
            {
                return context.GetAssemblyNameReference(aref);
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
            return ReflectorToCecilMember(mdec, (context, item) => context.GetMethodDefinition(item));
        }

        /// <summary>
        /// Retrieve the matching property in the Cecil object model
        /// </summary>
        /// <param name="pdec">Reflector property declaration</param>
        /// <returns>Cecil property definition (null if not found)</returns>
        public static PropertyDefinition ReflectorPropertyToCecilProperty(IPropertyDeclaration pdec)
        {
            return ReflectorToCecilMember(pdec, (context, item) => context.GetPropertyDefinition(item));
        }

        /// <summary>
        /// Retrieve the matching field in the Cecil object model
        /// </summary>
        /// <param name="fdec">Reflector field declaration</param>
        /// <returns>Cecil property definition (null if not found)</returns>
        public static FieldDefinition ReflectorFieldToCecilField(IFieldDeclaration fdec)
        {
            return ReflectorToCecilMember(fdec, (context, item) => context.GetFieldDefinition(item));
        }

        /// <summary>
        /// Retrieve the matching field in the Cecil object model
        /// </summary>
        /// <param name="fdec">Reflector field declaration</param>
        /// <returns>Cecil property definition (null if not found)</returns>
        public static EventDefinition ReflectorEventToCecilEvent(IEventDeclaration edec)
        {
            return ReflectorToCecilMember(edec, (context, item) => context.GetEventDefinition(item));
        }

        public static Resource ReflectorResourceToCecilResource(IResource res)
        {
            var moddec = res.Module;
            var context = PluginFactory.GetInstance().GetAssemblyContext(moddec.Location) as ReflectorAssemblyContext;

            if (context != null)
            {
                if (res is IEmbeddedResource)
                    return context.GetEmbeddedResource(res);

                if (res is IFileResource)
                    return context.GetLinkedResource(res);

                return context.GetAssemblyLinkedResource(res);
            }

            return null;
        }
        #endregion

        #region " Reflector/Reflector searchs "
        public static ITypeDeclaration FindMatchingType(ITypeDeclaration tdec, ITypeReference tref)
        {
            ITypeDeclaration result = null;

            if (tdec.ToString() == tref.ToString())
            {
                return tdec;
            }
            else
            {
                foreach (ITypeDeclaration idec in tdec.NestedTypes)
                {
                    result = FindMatchingType(idec, tref);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public static ITypeDeclaration FindMatchingType(IAssembly asm, ITypeReference tref)
        {
            ITypeDeclaration result = null;

            foreach (IModule mod in asm.Modules)
            {
                foreach (ITypeDeclaration tdec in mod.Types)
                {
                    result = FindMatchingType(tdec, tref);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        #endregion

    }
}
