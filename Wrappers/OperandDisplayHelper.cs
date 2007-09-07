
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Text;
using Reflexil.Utils;
using Reflexil.Properties;
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
                string result = string.Format("({0}) {1} {2}", Changebase(mdef.Body.Instructions.IndexOf(operand).ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase), operand.OpCode, OperandDisplayHelper.ToString(mdef, operand.Operand));
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
            return string.Format("-> ({0}) {1} ({2})", Changebase(operand.Index.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase), operand.Name, operand.VariableType);
		}
		
		public static string ToString(ParameterDefinition operand)
		{
            return string.Format("-> ({0}) {1} ({2})", Changebase(operand.Sequence.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase), operand.Name, operand.ParameterType);
		}

        public static string Changebase(string input, ENumericBase inputbase, ENumericBase outputbase)
        {
            try
            {
                string result = string.Empty;
                if (input != null && input != String.Empty)
                {
                    input = input.Replace(" ", String.Empty);
                    bool isnegative = input.StartsWith("-");
                    input = input.Replace("-", String.Empty);
                    long value = Convert.ToInt64(input, (int)inputbase);
                    result = ((isnegative) ? "-" : String.Empty) + Convert.ToString(value, (int)outputbase);
                }
                return result;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
		
		public static string ToString(MethodDefinition mdef, object operand)
		{
			if (operand == null)
			{
				return string.Empty;
			}
			else
			{
				if (operand is Instruction)
				{
					return ToString(mdef, ((Instruction) operand), true);
				}
				else if (operand is Instruction[])
				{
                    return ToString(mdef, ((Instruction[])operand));
				}
				else if (operand is VariableDefinition)
				{
					return ToString((VariableDefinition) operand);
				}
				else if (operand is ParameterDefinition)
				{
					return ToString((ParameterDefinition) operand);
				}
                else if (   (operand is Int16 || operand is Int32 || operand is Int64 || operand is SByte)
                         || (operand is UInt16 || operand is UInt32 || operand is UInt64 || operand is Byte) )
                {
                    return Changebase(operand.ToString(), ENumericBase.Dec, Settings.Default.OperandDisplayBase);
                }
			}
			return operand.ToString();
		}
		#endregion
		
	}
	
}

