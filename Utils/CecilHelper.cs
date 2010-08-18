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
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Runtime.InteropServices;
using Reflexil.Plugins;
using System.Collections.Generic;
using System.Collections;
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
        public static TypeDefinition FindMatchingType(ICollection<TypeDefinition> types, string fulltypename)
        {
            foreach (var ttype in types)
            {
                if (fulltypename == ttype.FullName)
                    return ttype;

                var ittype = FindMatchingType(ttype.NestedTypes, fulltypename);
                if (ittype != null)
                    return ittype;
            }
            return null;
        }

        public static TypeDefinition FindMatchingType(ModuleDefinition mdef, string fulltypename)
        {
            return FindMatchingType(mdef.Types, fulltypename);
        }

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
            if ((mref1.Name == mref2.Name) && (mref1.Parameters.Count == mref2.Parameters.Count) && (mref1.ReturnType.FullName == mref2.ReturnType.FullName))
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

            return null;
        }
        #endregion

        #region " Method body "
        public static ParameterDefinition CloneParameterDefinition(ParameterDefinition param)
        {
            if (param.Method is IGenericParameterProvider)
            {
                return CloneParameterDefinition(param, new ImportContext(NullReferenceImporter.Instance, param.Method as IGenericParameterProvider));
            }
            return CloneParameterDefinition(param, new ImportContext(NullReferenceImporter.Instance));
        }
        
        public static ParameterDefinition CloneParameterDefinition(ParameterDefinition param, ImportContext context)
        {
            ParameterDefinition np = new ParameterDefinition(
                param.Name,
                param.Attributes,
                context.Import(param.ParameterType));

            if (param.HasConstant)
                np.Constant = param.Constant;

            if (param.MarshalInfo != null)
                np.MarshalInfo = new MarshalInfo(param.MarshalInfo.NativeType);

            foreach (CustomAttribute ca in param.CustomAttributes)
                np.CustomAttributes.Add(CustomAttribute.Clone(ca, context));

            return np;
        }

        internal static Instruction GetInstruction(Mono.Cecil.Cil.MethodBody oldBody, Mono.Cecil.Cil.MethodBody newBody, Instruction i)
        {
            int pos = oldBody.Instructions.IndexOf(i);
            if (pos > -1 && pos < newBody.Instructions.Count)
                return newBody.Instructions[pos];

            return new Instruction(int.MaxValue, OpCodes.Nop); 
        }

        public static Mono.Cecil.Cil.MethodBody CloneMethodBody(Mono.Cecil.Cil.MethodBody body, MethodDefinition parent, ImportContext context)
        {
            Mono.Cecil.Cil.MethodBody nb = new Mono.Cecil.Cil.MethodBody(parent);
            nb.MaxStackSize = body.MaxStackSize;
            nb.InitLocals = body.InitLocals;
            nb.CodeSize = body.CodeSize;

            ILProcessor worker = nb.GetILProcessor();

            foreach (VariableDefinition var in body.Variables)
                nb.Variables.Add(new VariableDefinition(
                    var.Name, context.Import(var.VariableType)));

            foreach (Instruction instr in body.Instructions)
            {
                Instruction ni = new Instruction(instr.OpCode, OpCodes.Nop);

                switch (instr.OpCode.OperandType)
                {
                    case OperandType.InlineArg:
                    case OperandType.ShortInlineArg:
                        if (instr.Operand == body.ThisParameter)
                            ni.Operand = nb.ThisParameter;
                        else
                        {
                            int param = body.Method.Parameters.IndexOf((ParameterDefinition)instr.Operand);
                            ni.Operand = parent.Parameters[param];
                        }
                        break;
                    case OperandType.InlineVar:
                    case OperandType.ShortInlineVar:
                        int var = body.Variables.IndexOf((VariableDefinition)instr.Operand);
                        ni.Operand = nb.Variables[var];
                        break;
                    case OperandType.InlineField:
                        ni.Operand = context.Import((FieldReference)instr.Operand);
                        break;
                    case OperandType.InlineMethod:
                        ni.Operand = context.Import((MethodReference)instr.Operand);
                        break;
                    case OperandType.InlineType:
                        ni.Operand = context.Import((TypeReference)instr.Operand);
                        break;
                    case OperandType.InlineTok:
                        if (instr.Operand is TypeReference)
                            ni.Operand = context.Import((TypeReference)instr.Operand);
                        else if (instr.Operand is FieldReference)
                            ni.Operand = context.Import((FieldReference)instr.Operand);
                        else if (instr.Operand is MethodReference)
                            ni.Operand = context.Import((MethodReference)instr.Operand);
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                    case OperandType.InlineSwitch:
                        break;
                    default:
                        ni.Operand = instr.Operand;
                        break;
                }

                worker.Append(ni);
            }

            for (int i = 0; i < body.Instructions.Count; i++)
            {
                Instruction instr = nb.Instructions[i];
                Instruction oldi = body.Instructions[i];

                if (instr.OpCode.OperandType == OperandType.InlineSwitch)
                {
                    Instruction[] olds = (Instruction[])oldi.Operand;
                    Instruction[] targets = new Instruction[olds.Length];

                    for (int j = 0; j < targets.Length; j++)
                        targets[j] = GetInstruction(body, nb, olds[j]);

                    instr.Operand = targets;
                }
                else if (instr.OpCode.OperandType == OperandType.ShortInlineBrTarget || instr.OpCode.OperandType == OperandType.InlineBrTarget)
                    instr.Operand = GetInstruction(body, nb, (Instruction)oldi.Operand);
            }

            foreach (ExceptionHandler eh in body.ExceptionHandlers)
            {
                ExceptionHandler neh = new ExceptionHandler(eh.HandlerType);
                neh.TryStart = GetInstruction(body, nb, eh.TryStart);
                neh.TryEnd = GetInstruction(body, nb, eh.TryEnd);
                neh.HandlerStart = GetInstruction(body, nb, eh.HandlerStart);
                neh.HandlerEnd = GetInstruction(body, nb, eh.HandlerEnd);

                switch (eh.HandlerType)
                {
                    case ExceptionHandlerType.Catch:
                        neh.CatchType = context.Import(eh.CatchType);
                        break;
                    case ExceptionHandlerType.Filter:
                        neh.FilterStart = GetInstruction(body, nb, eh.FilterStart);
                        neh.FilterEnd = GetInstruction(body, nb, eh.FilterEnd);
                        break;
                }

                nb.ExceptionHandlers.Add(neh);
            }

            return nb;
        }

        /// <summary>
        /// Clone a source method body to a target method definition.
        /// Field/Method/Type references are corrected
        /// </summary>
        /// <param name="source">Source method definition</param>
        /// <param name="target">Target method definition</param>
        public static void CloneMethodBody(MethodDefinition source, MethodDefinition target)
        {
            ImportContext context = new ImportContext(new DefaultImporter(target.DeclaringType.Module));
            Mono.Cecil.Cil.MethodBody newBody = CloneMethodBody(source.Body, target, context);

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

            UpdateInstructionsOffsets(target.Body);
        }

        public static void UpdateInstructionsOffsets(Mono.Cecil.Cil.MethodBody body)
        {
            long start = 0;
            long position = 0;

            foreach (Instruction instr in body.Instructions)
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
                    case OperandType.ShortInlineArg:
                        position += Marshal.SizeOf(typeof(byte));
                        break;
                    case OperandType.InlineSig:
                    case OperandType.InlineI:
                        position += Marshal.SizeOf(typeof(int));
                        break;
                    case OperandType.InlineVar:
                    case OperandType.InlineArg:
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
            asmdef.Name.Attributes = AssemblyAttributes.SideBySideCompatible;
        }
        #endregion

    }
}


