
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Text;
#endregion

namespace Reflexil.Wrappers
{
	public partial class OperandDisplayHelper
	{
		
		#region " Methods "
		public static string ToString(MethodDefinition mdef, Instruction operand, bool showLink)
		{
			if (mdef != null)
			{
				string result = string.Format("({0}) {1} {2}", mdef.Body.Instructions.IndexOf(operand), operand.OpCode, OperandDisplayHelper.ToString(mdef, operand.Operand));
				if (showLink)
				{
					result = "-> " + result;
				}
				return result;
			}
			return string.Empty;
		}
		
		public static string ToString(MethodDefinition mdef, Instruction[] operand)
		{
			StringBuilder result = new StringBuilder("-> ");
			for (int i = 0; i <= operand.Length - 1; i++)
			{
				result.Append(ToString(mdef, operand[i], false));
				if (operand.Length > 1 && i < operand.Length - 1)
				{
					result.Append(", ");
				}
			}
			return result.ToString();
		}
		
		public static string ToString(VariableDefinition operand)
		{
            return string.Format("-> ({0}) {1} ({2})", operand.Index, operand.Name, operand.VariableType);
		}
		
		public static string ToString(ParameterDefinition operand)
		{
            return string.Format("-> ({0}) {1} ({2})", operand.Sequence, operand.Name, operand.ParameterType);
		}
		
		public static string ToString(MethodDefinition mdef, object operand)
		{
			if (operand == null)
			{
				return string.Empty;
			}
			else
			{
				if ((operand) is Instruction)
				{
					return ToString(mdef, ((Instruction) operand), true);
				}
				else if ((operand) is Instruction[])
				{
                    return ToString(mdef, ((Instruction[])operand));
				}
				else if ((operand) is VariableDefinition)
				{
					return ToString((VariableDefinition) operand);
				}
				else if ((operand) is ParameterDefinition)
				{
					return ToString((ParameterDefinition) operand);
				}
			}
			return operand.ToString();
		}
		#endregion
		
	}
	
}

