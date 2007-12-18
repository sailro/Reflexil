/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Properties;
using Reflexil.Utils;
#endregion

namespace Reflexil.Wrappers
{
    /// <summary>
    /// Helper for displaying various Cecil objects
    /// </summary>
	public static class OperandDisplayHelper
	{
		
		#region " Methods "
        /// <summary>
        /// Returns a String that represents an instruction
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <param name="operand">Instruction</param>
        /// <param name="showLink">Prefix the string with a link</param>
        /// <returns>A String like [->] opcode operand</returns>
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
		
        /// <summary>
        /// Returns a String that represents several instructions
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <param name="operand">Instructions</param>
        /// <returns>A String like [->] opcode1 operand1, opcode2 operand2, ... </returns>
        public static string ToString(MethodDefinition mdef, Instruction[] operand)
		{
			StringBuilder result = new StringBuilder("-> ");
			for (int i = 0; i <= operand.Length - 1; i++)
			{
                if (i > 0)
                {
                    result.Append(", ");
                }
                result.Append(ToString(mdef, operand[i], false));
			}
			return result.ToString();
		}
		
        /// <summary>
        /// Returns a String that represents a variable definition
        /// </summary>
        /// <param name="operand">Variable definition</param>
        /// <returns>A String like -> (index) name (variable type)</returns>
		public static string ToString(VariableDefinition operand)
		{
            return string.Format("-> ({0}) {1} ({2})", Changebase(operand.Index.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase), operand.Name, operand.VariableType);
		}
		
        /// <summary>
        /// Returns a String that represents a parameter definition
        /// </summary>
        /// <param name="operand">Parameter definition</param>
        /// <returns>A String like -> (index) name (parameter type)</returns>
        public static string ToString(ParameterDefinition operand)
		{
            return string.Format("-> ({0}) {1} ({2})", Changebase(operand.Sequence.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase), operand.Name, operand.ParameterType);
		}

        /// <summary>
        /// Change numerical base. Handles negative numbers.
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <param name="inputbase">Input base (must match with input)</param>
        /// <param name="outputbase">Output base</param>
        /// <returns>Converted input as string</returns>
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
		
        /// <summary>
        /// Returns a String that represents the object
        /// </summary>
        /// <param name="mdef">Method definition</param>
        /// <param name="operand">Object</param>
        /// <returns>See OperandDisplayHelper specialized ToString methods</returns>
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

