/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using System.Runtime.InteropServices;
using Reflexil.Plugins;
#endregion

namespace Reflexil.Utils
{
    /// <summary>
    /// Cecil object model helper.
    /// </summary>
	public static partial class CecilHelper
    {

        #region " Methods "

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
                if ((fdef.Name == fref.Name) && (fdef.FieldType.FullName == fref.FieldType.FullName))
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

        #region " Method body "
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

        public static void UpdateInstructionsOffsets(InstructionCollection instructions)
        {
            Mono.Cecil.Cil.MethodBody body = instructions.Container;
            long start = 0;
            long position = 0;

            foreach (Instruction instr in instructions)
            {

                instr.Offset = (int)(position - start);

                position += instr.OpCode.Size;

                switch (instr.OpCode.OperandType)
                {
                    case OperandType.InlineNone:
                        break;
                    case OperandType.InlineSwitch:
                        Instruction[] targets = (Instruction[])instr.Operand;
                        position += Marshal.SizeOf(typeof(uint))*targets.Length;
                        break;
                    case OperandType.ShortInlineBrTarget:
                        position += Marshal.SizeOf(typeof(byte));
                        break;
                    case OperandType.InlineBrTarget:
                        position += Marshal.SizeOf(typeof(int));
                        break;
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineVar:
                    case OperandType.ShortInlineParam:
                        position += Marshal.SizeOf(typeof(byte));
                        break;
                    case OperandType.InlineSig:
                    case OperandType.InlineI:
                        position += Marshal.SizeOf(typeof(int));
                        break;
                    case OperandType.InlineVar:
                    case OperandType.InlineParam:
                        position += Marshal.SizeOf(typeof(short));
                        break;
                    case OperandType.InlineI8:
                        position += Marshal.SizeOf(typeof(long));
                        break;
                    case OperandType.ShortInlineR:
                        position += Marshal.SizeOf(typeof(float));
                        break;
                    case OperandType.InlineR:
                        position += Marshal.SizeOf(typeof(double));
                        break;
                    case OperandType.InlineString:
                    case OperandType.InlineField:
                    case OperandType.InlineMethod:
                    case OperandType.InlineType:
                    case OperandType.InlineTok:
                        position += Marshal.SizeOf(typeof(int)); 
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Remove the Strong Name Reference of the given assembly name
        /// </summary>
        /// <param name="andef">Strong Name assembly</param>
        public static void RemoveStrongNameReference(AssemblyNameReference andef)
        {
            andef.PublicKeyToken = new byte[0];
        }

        /// <summary>
        /// Remove the Strong Name of the given assembly
        /// </summary>
        /// <param name="asmdef">Strong Name assembly</param>
        public static void RemoveStrongName(AssemblyDefinition asmdef)
        {
            asmdef.Name.PublicKey = new byte[0];
            asmdef.Name.PublicKeyToken = new byte[0];
            asmdef.Name.Flags = AssemblyFlags.SideBySideCompatible;
        }

        /// <summary>
        /// Retrieve the Void struct
        /// </summary>
        /// <param name="adef">AssemblyDefinition</param>
        /// <returns></returns>
        public static TypeReference GetVoidType(AssemblyDefinition adef)
        {
            foreach (AssemblyNameReference anref in adef.MainModule.AssemblyReferences)
            {
                if (anref.Name == "mscorlib")
                {
                    DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
                    AssemblyDefinition mscorlib = resolver.Resolve(anref);
                    return mscorlib.MainModule.Types["System.Void"];
                }
            }
            throw new ArgumentException("Unable to find mscorlib");
        }

        /// <summary>
        /// Inject an item definition into an owner
        /// </summary>
        /// <param name="owner">Owner item</param>
        /// <param name="targettype">Target type definition</param>
        /// <param name="name">name for the newly created item</param>
        /// <returns>Object definition</returns>
        public static object Inject(object owner, Type targettype, string name)
        {
            if (owner != null && targettype != null && name != null)
            {
                TypeReference @void;

                if (owner is AssemblyDefinition)
                {
                    AssemblyDefinition adef = owner as AssemblyDefinition;
                    @void = GetVoidType(adef);

                    if (targettype.Equals(typeof(AssemblyNameReference)))
                    {
                        AssemblyNameReference anref = new AssemblyNameReference(name, string.Empty, new Version());
                        adef.MainModule.AssemblyReferences.Add(anref);
                        return anref;
                    }
                    else if (targettype.Equals(typeof(TypeDefinition)))
                    {
                        TypeDefinition tdef = new TypeDefinition(name, string.Empty, (Mono.Cecil.TypeAttributes)0, null);
                        adef.MainModule.Types.Add(tdef);
                        return tdef;
                    }
                }
                else if (owner is TypeDefinition)
                {
                    TypeDefinition tdef = owner as TypeDefinition;
                    @void = GetVoidType(tdef.Module.Assembly);

                    if (targettype.Equals(typeof(TypeDefinition)))
                    {
                        // TODO Handle base type in MethodDefinition
                        TypeDefinition itdef = new TypeDefinition(name, tdef.Namespace, (Mono.Cecil.TypeAttributes)0, null);
                        tdef.NestedTypes.Add(itdef);
                        return itdef;
                    }
                    else if (targettype.Equals(typeof(MethodDefinition)))
                    {
                        MethodDefinition mdef = new MethodDefinition(name, (Mono.Cecil.MethodAttributes)0, @void);
                        tdef.Methods.Add(mdef);
                        return mdef;
                    }
                    else if (targettype.Equals(typeof(PropertyDefinition)))
                    {
                        PropertyDefinition pdef = new PropertyDefinition(name, @void, (Mono.Cecil.PropertyAttributes)0);
                        pdef.GetMethod = PropertyDefinition.CreateGetMethod(pdef);
                        pdef.SetMethod = PropertyDefinition.CreateSetMethod(pdef);
                        tdef.Properties.Add(pdef);
                        return pdef;
                    }
                    else if (targettype.Equals(typeof(FieldDefinition)))
                    {
                        FieldDefinition fdef = new FieldDefinition(name, @void, (Mono.Cecil.FieldAttributes)0);
                        tdef.Fields.Add(fdef);
                        return fdef;
                    }
                    else if (targettype.Equals(typeof(EventDefinition)))
                    {
                        EventDefinition edef = new EventDefinition(name, @void, (Mono.Cecil.EventAttributes)0);
                        edef.AddMethod = EventDefinition.CreateAddMethod(edef);
                        edef.RemoveMethod = EventDefinition.CreateRemoveMethod(edef);
                        tdef.Events.Add(edef);
                        return edef;
                    }
                }

                throw new NotImplementedException();
            }
            else
            {
                throw new ArgumentException();
            }
        }


        #endregion

    }
}


