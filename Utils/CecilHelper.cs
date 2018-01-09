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

using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using Mono.Cecil.Rocks;
using Reflexil.Properties;

namespace Reflexil.Utils
{
	public static class CecilHelper
	{
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

		public static FieldDefinition FindMatchingField(TypeDefinition tdef, FieldReference fref)
		{
			return tdef.Fields.FirstOrDefault(fdef => (fdef.Name == fref.Name) && (fdef.FieldType.FullName == fref.FieldType.FullName));
		}

		public static bool ReferenceMatches(AssemblyNameReference anref1, AssemblyNameReference anref2)
		{
			// Skip Key
			return (anref1.Name == anref2.Name) &&
			       (string.Compare(anref1.Version.ToString(2), anref2.Version.ToString(2), StringComparison.Ordinal) == 0) &&
			       (anref1.Culture == anref2.Culture);
		}

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

		public static MethodDefinition FindMatchingMethod(TypeDefinition tdef, MethodReference mref)
		{
			return tdef.Methods.FirstOrDefault(mdef => MethodMatches(mdef, mref));
		}

		internal static Instruction GetInstruction(MethodBody oldBody, MethodBody newBody, Instruction i)
		{
			var pos = oldBody.Instructions.IndexOf(i);
			if (pos > -1 && pos < newBody.Instructions.Count)
				return newBody.Instructions[pos];

			return new Instruction(int.MaxValue, OpCodes.Nop);
		}

		private static TypeReference FixTypeImport(ModuleDefinition context, TypeReference type)
		{
			if (!(type is TypeDefinition))
				return CecilImporter.Import(context, type);

			var result = FindMatchingType(context, type.FullName);
			if (result == null)
				throw new ArgumentException(string.Format("No match for type {0} in source assembly.", type.FullName));

			return result;
		}

		private static FieldReference FixFieldImport(ModuleDefinition context, FieldReference field)
		{
			if (!(field is FieldDefinition))
				return CecilImporter.Import(context, field);

			var type = FixTypeImport(context, field.DeclaringType) as TypeDefinition;
			var result = FindMatchingField(type, field);
			if (result == null)
				throw new ArgumentException(string.Format("No match for field {0} in source assembly.", field.FullName));

			return result;
		}

		private static MethodReference FixMethodImport(ModuleDefinition context, MethodReference method)
		{
			if (!(method is MethodDefinition))
				return CecilImporter.Import(context, method);

			var type = FixTypeImport(context, method.DeclaringType) as TypeDefinition;
			var result = FindMatchingMethod(type, method);
			if (result == null)
				throw new ArgumentException(string.Format("No match for method {0} in source assembly.", method.FullName));

			return result;
		}

		private static MethodBody CloneMethodBody(MethodBody body, MethodDefinition target)
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
				nb.Variables.Add(new VariableDefinition(FixTypeImport(context, var.VariableType)));

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
						ni.Operand = FixFieldImport(context, (FieldReference) instr.Operand);
						break;
					case OperandType.InlineMethod:
						ni.Operand = FixMethodImport(context, (MethodReference) instr.Operand);
						break;
					case OperandType.InlineType:
						ni.Operand = FixTypeImport(context, (TypeReference) instr.Operand);
						break;
					case OperandType.InlineTok:
						var tref = instr.Operand as TypeReference;
						if (tref != null)
							ni.Operand = FixTypeImport(context, tref);
						else if (instr.Operand is FieldReference)
							ni.Operand = FixFieldImport(context, (FieldReference) instr.Operand);
						else if (instr.Operand is MethodReference)
							ni.Operand = FixMethodImport(context, (MethodReference) instr.Operand);
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
						neh.CatchType = FixTypeImport(context, eh.CatchType);
						break;
					case ExceptionHandlerType.Filter:
						neh.FilterStart = GetInstruction(body, nb, eh.FilterStart);
						break;
				}

				nb.ExceptionHandlers.Add(neh);
			}

			return nb;
		}

		public static void CloneMethodBody(MethodDefinition source, MethodDefinition target)
		{
			var newBody = CloneMethodBody(source.Body, target);
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
				CecilImporter.Import(context, param.ParameterType));

			if (param.HasConstant)
				np.Constant = param.Constant;

			if (param.MarshalInfo != null)
				np.MarshalInfo = new MarshalInfo(param.MarshalInfo.NativeType);

			foreach (var ca in param.CustomAttributes)
				np.CustomAttributes.Add(CustomAttribute.Clone(ca, context));

			return np;
		}

		public static void RemoveStrongNameReference(AssemblyNameReference andef)
		{
			andef.PublicKeyToken = new byte[0];
		}

		public static void RemoveStrongName(AssemblyDefinition asmdef)
		{
			asmdef.Name.PublicKey = new byte[0];
			asmdef.Name.PublicKeyToken = new byte[0];
			asmdef.Name.Attributes = AssemblyAttributes.SideBySideCompatible;
			asmdef.MainModule.Attributes &= ~ModuleAttributes.StrongNameSigned;
		}

		public static ModuleDefinition GetModuleFromCustomAttributeProvider(ICustomAttributeProvider provider)
		{
			var adef = provider as AssemblyDefinition;
			if (adef != null)
				return adef.MainModule;

			var gparam = provider as GenericParameter;
			if (gparam != null)
				return gparam.Module;

			var tdef = provider as TypeDefinition;
			if (tdef != null)
				return tdef.Module;

			var imdef = provider as IMemberDefinition;
			if (imdef != null)
				return imdef.DeclaringType.Module;

			var mrt = provider as MethodReturnType;
			if (mrt != null)
			{
				var mdef = mrt.Method as MethodDefinition;
				if (mdef != null)
					return mdef.Module;
			}

			var definition = provider as ModuleDefinition;
			if (definition != null)
				return definition;

			var pdef = provider as ParameterDefinition;
			if (pdef != null)
			{
				var mdef = pdef.Method as MethodDefinition;
				if (mdef != null)
					return mdef.Module;
			}

			throw new ArgumentException();
		}

		public static IEnumerable<AssemblyNameReference> SearchReferences(ModuleDefinition module, string searchToken, Version searchVersion)
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
	}
}