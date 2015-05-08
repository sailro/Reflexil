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
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using Mono.Cecil.Rocks;
using Reflexil.Properties;

#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Cecil object model helper.
	/// </summary>
	public static class CecilHelper
	{
		#region Methods

		#region Cecil/Cecil searchs

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
			return
				tdef.Fields.FirstOrDefault(fdef => (fdef.Name == fref.Name) && (fdef.FieldType.FullName == fref.FieldType.FullName));
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
			return ((anref1.Name == anref2.Name) &&
			        (String.Compare(anref1.Version.ToString(2), anref2.Version.ToString(2), StringComparison.Ordinal) == 0) &&
			        (anref1.Culture == anref2.Culture));
		}

		/// <summary>
		/// Determines if two methods matches
		/// </summary>
		/// <param name="mref1">a method</param>
		/// <param name="mref2">a method to compare</param>
		/// <returns>true if matches</returns>
		private static bool MethodMatches(MethodReference mref1, MethodReference mref2)
		{
			if ((mref1.Name != mref2.Name) || (mref1.Parameters.Count != mref2.Parameters.Count) ||
			    (mref1.ReturnType.FullName != mref2.ReturnType.FullName))
				return false;

			for (var i = 0; i <= mref1.Parameters.Count - 1; i++)
			{
				if (mref1.Parameters[i].ParameterType.FullName != mref2.Parameters[i].ParameterType.FullName)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Find a similar method in the given type definition 
		/// </summary>
		/// <param name="tdef">Type definition</param>
		/// <param name="mref">Method reference</param>
		/// <returns>Method definition (or null if not found)</returns>
		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, MethodReference mref)
		{
			return tdef.Methods.FirstOrDefault(mdef => MethodMatches(mdef, mref));
		}

		#endregion

		#region Method body

		internal static Instruction GetInstruction(MethodBody oldBody, MethodBody newBody, Instruction i)
		{
			int pos = oldBody.Instructions.IndexOf(i);
			if (pos > -1 && pos < newBody.Instructions.Count)
				return newBody.Instructions[pos];

			return new Instruction(int.MaxValue, OpCodes.Nop);
		}

		private static TypeReference FixTypeImport(ModuleDefinition context, MethodDefinition source, MethodDefinition target,
			TypeReference type)
		{
			if (type.FullName == source.DeclaringType.FullName)
				return target.DeclaringType;

			return context.Import(type);
		}

		private static FieldReference FixFieldImport(ModuleDefinition context, MethodDefinition source,
			MethodDefinition target, FieldReference field)
		{
			if (field.DeclaringType.FullName == source.DeclaringType.FullName)
				return FindMatchingField(target.DeclaringType, field);

			return context.Import(field);
		}

		private static MethodReference FixMethodImport(ModuleDefinition context, MethodDefinition source,
			MethodDefinition target, MethodReference method)
		{
			if (method.DeclaringType.FullName == source.DeclaringType.FullName)
				return FindMatchingMethod(target.DeclaringType, method);

			return context.Import(method);
		}

		private static MethodBody CloneMethodBody(MethodBody body, MethodDefinition source, MethodDefinition target)
		{
			var context = target.DeclaringType.Module;
			var nb = new MethodBody(target)
			{
				MaxStackSize = body.MaxStackSize,
				InitLocals = body.InitLocals,
				CodeSize = body.CodeSize
			};

			var worker = nb.GetILProcessor();

			foreach (var var in body.Variables)
				nb.Variables.Add(new VariableDefinition(
					var.Name, FixTypeImport(context, source, target, var.VariableType)));

			foreach (var instr in body.Instructions)
			{
				var ni = new Instruction(instr.OpCode, OpCodes.Nop);

				switch (instr.OpCode.OperandType)
				{
					case OperandType.InlineArg:
					case OperandType.ShortInlineArg:
						if (instr.Operand == body.ThisParameter)
							ni.Operand = nb.ThisParameter;
						else
						{
							var param = body.Method.Parameters.IndexOf((ParameterDefinition) instr.Operand);
							ni.Operand = target.Parameters[param];
						}
						break;
					case OperandType.InlineVar:
					case OperandType.ShortInlineVar:
						var var = body.Variables.IndexOf((VariableDefinition) instr.Operand);
						ni.Operand = nb.Variables[var];
						break;
					case OperandType.InlineField:
						ni.Operand = FixFieldImport(context, source, target, (FieldReference) instr.Operand);
						break;
					case OperandType.InlineMethod:
						ni.Operand = FixMethodImport(context, source, target, (MethodReference) instr.Operand);
						break;
					case OperandType.InlineType:
						ni.Operand = FixTypeImport(context, source, target, (TypeReference) instr.Operand);
						break;
					case OperandType.InlineTok:
						if ((instr.Operand) is TypeReference)
							ni.Operand = FixTypeImport(context, source, target, (TypeReference) instr.Operand);
						else if ((instr.Operand) is FieldReference)
							ni.Operand = FixFieldImport(context, source, target, (FieldReference) instr.Operand);
						else if ((instr.Operand) is MethodReference)
							ni.Operand = FixMethodImport(context, source, target, (MethodReference) instr.Operand);
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

			for (var i = 0; i < body.Instructions.Count; i++)
			{
				var instr = nb.Instructions[i];
				var oldi = body.Instructions[i];

				switch (instr.OpCode.OperandType)
				{
					case OperandType.InlineSwitch:
					{
						var olds = (Instruction[]) oldi.Operand;
						var targets = new Instruction[olds.Length];

						for (var j = 0; j < targets.Length; j++)
							targets[j] = GetInstruction(body, nb, olds[j]);

						instr.Operand = targets;
					}
						break;
					case OperandType.InlineBrTarget:
					case OperandType.ShortInlineBrTarget:
						instr.Operand = GetInstruction(body, nb, (Instruction) oldi.Operand);
						break;
				}
			}

			foreach (var eh in body.ExceptionHandlers)
			{
				var neh = new ExceptionHandler(eh.HandlerType)
				{
					TryStart = GetInstruction(body, nb, eh.TryStart),
					TryEnd = GetInstruction(body, nb, eh.TryEnd),
					HandlerStart = GetInstruction(body, nb, eh.HandlerStart),
					HandlerEnd = GetInstruction(body, nb, eh.HandlerEnd)
				};

				switch (eh.HandlerType)
				{
					case ExceptionHandlerType.Catch:
						neh.CatchType = FixTypeImport(context, source, target, eh.CatchType);
						break;
					case ExceptionHandlerType.Filter:
						neh.FilterStart = GetInstruction(body, nb, eh.FilterStart);
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
			var newBody = CloneMethodBody(source.Body, source, target);
			target.Body = newBody;


			if (Settings.Default.OptimizeAndFixIL)
			{
				// this will also call ComputeOffsets
				target.Body.SimplifyMacros();
				target.Body.OptimizeMacros();
			}
			else
			{
				target.Body.ComputeOffsets();
			}
		}

		public static ParameterDefinition CloneParameterDefinition(ParameterDefinition param, MethodDefinition owner)
		{
			var context = owner.Module;
			var np = new ParameterDefinition(
				param.Name,
				param.Attributes,
				context.Import(param.ParameterType));

			if (param.HasConstant)
				np.Constant = param.Constant;

			if (param.MarshalInfo != null)
				np.MarshalInfo = new MarshalInfo(param.MarshalInfo.NativeType);

			foreach (var ca in param.CustomAttributes)
				np.CustomAttributes.Add(CustomAttribute.Clone(ca, context));

			return np;
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
			asmdef.MainModule.Attributes &= ~ModuleAttributes.StrongNameSigned;
		}

		public static ModuleDefinition GetModuleFromCustomAttributeProvider(ICustomAttributeProvider provider)
		{
			if (provider is AssemblyDefinition)
				return (provider as AssemblyDefinition).MainModule;

			if (provider is GenericParameter)
				return (provider as GenericParameter).Module;

			if (provider is IMemberDefinition)
				return (provider as IMemberDefinition).DeclaringType.Module;

			if (provider is MethodReturnType)
			{
				var mdef = (provider as MethodReturnType).Method as MethodDefinition;
				if (mdef != null)
					return mdef.Module;
			}

			if (provider is ModuleDefinition)
				return provider as ModuleDefinition;

			if (provider is ParameterDefinition)
			{
				var mdef = (provider as ParameterDefinition).Method as MethodDefinition;
				if (mdef != null)
					return mdef.Module;
			}

			throw new ArgumentException();
		}

		public static IEnumerable<AssemblyNameReference> SearchReferences(ModuleDefinition module, string searchToken,
			Version searchVersion)
		{
			return module.AssemblyReferences.Where(aname => ByteHelper.ByteToString(aname.PublicKeyToken) == searchToken && aname.Version == searchVersion);
		}

		public static void PatchAssemblyNames(ModuleDefinition module, string searchToken, Version searchVersion, string replaceToken, Version replaceVersion)
		{
			var references = SearchReferences(module, searchToken, searchVersion).ToList();
			PatchAssemblyNames(references, replaceToken, replaceVersion);
		}

		public static void PatchAssemblyNames(IEnumerable<AssemblyNameReference> references, string replaceToken, Version replaceVersion)
		{
			foreach (var aname in references)
			{
				aname.Version = replaceVersion;
				aname.PublicKeyToken = ByteHelper.StringToByte(replaceToken);
			}
		}

		#endregion
	}
}