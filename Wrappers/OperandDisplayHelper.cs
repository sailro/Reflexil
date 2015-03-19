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
using System.Globalization;
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
		#region Constants

		public const char ItemSeparator = ',';

		#endregion

		#region Methods

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
				// Prevent infinite loop, thanks to brien
				var target = (operand.Operand == operand) ? "<self>" : ToString(mdef, operand.Operand);

				var result = string.Format("({0}) {1} {2}",
					Changebase(mdef.Body.Instructions.IndexOf(operand).ToString(CultureInfo.InvariantCulture),
						ENumericBase.Dec,
						Settings.Default.RowIndexDisplayBase
						),
					operand.OpCode,
					target);
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
			var result = new StringBuilder("-> ");
			for (var i = 0; i <= operand.Length - 1; i++)
			{
				if (i > 0)
					result.Append(", ");

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
			return string.Format("-> ({0}) {1} ({2})",
				Changebase(operand.Index.ToString(CultureInfo.InvariantCulture), ENumericBase.Dec,
					Settings.Default.RowIndexDisplayBase), operand.Name, operand.VariableType);
		}

		/// <summary>
		/// Returns a String that represents a parameter definition
		/// </summary>
		/// <param name="operand">Parameter definition</param>
		/// <returns>A String like -> (index) name (parameter type)</returns>
		public static string ToString(ParameterDefinition operand)
		{
			return string.Format("-> ({0}) {1} ({2})",
				Changebase(operand.Index.ToString(CultureInfo.InvariantCulture), ENumericBase.Dec,
					Settings.Default.RowIndexDisplayBase), operand.Name, operand.ParameterType);
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
				if (!String.IsNullOrEmpty(input) && input.Contains(ItemSeparator.ToString(CultureInfo.InvariantCulture)))
				{
					var values = input.Split(ItemSeparator);
					var cbvalues = new string[values.Length];
					for (var i = 0; i < values.Length; i++)
						cbvalues[i] = Changebase(values[i], inputbase, outputbase);
					return String.Join(ItemSeparator.ToString(CultureInfo.InvariantCulture), cbvalues);
				}

				return InternalChangebase(input, inputbase, outputbase);
			}
			catch (Exception)
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Change numerical base. Handles negative numbers.
		/// </summary>
		/// <param name="input">String to convert</param>
		/// <param name="inputbase">Input base (must match with input)</param>
		/// <param name="outputbase">Output base</param>
		/// <returns>Converted input as string</returns>
		private static string InternalChangebase(string input, ENumericBase inputbase, ENumericBase outputbase)
		{
			try
			{
				var result = string.Empty;
				if (!string.IsNullOrEmpty(input))
				{
					input = input.Replace(" ", String.Empty);
					var isnegative = input.StartsWith("-");
					input = input.Replace("-", String.Empty);
					var value = Convert.ToInt64(input, (int) inputbase);
					result = ((isnegative) ? "-" : String.Empty) + Convert.ToString(value, (int) outputbase);
				}
				return result;
			}
			catch (Exception)
			{
				return String.Empty;
			}
		}

		public static string ToString(CustomAttributeArgument argument)
		{
			return argument.Value.ToString();
		}

		public static string ToString(CustomAttributeArgument[] arguments)
		{
			var result = new StringBuilder();
			if (arguments != null)
			{
				for (var i = 0; i < arguments.Length; i++)
				{
					if (i > 0)
					{
						result.Append(ItemSeparator);
						result.Append(" ");
					}

					result.Append(arguments[i].Value);
				}
			}
			return result.ToString();
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
				return string.Empty;

			var instruction = operand as Instruction;
			if (instruction != null)
				return ToString(mdef, instruction, true);

			var instructions = operand as Instruction[];
			if (instructions != null)
				return ToString(mdef, instructions);

			var vdef = operand as VariableDefinition;
			if (vdef != null)
				return ToString(vdef);

			var pdef = operand as ParameterDefinition;
			if (pdef != null)
				return ToString(pdef);

			if ((operand is Int16 || operand is Int32 || operand is Int64 || operand is SByte)
			    || (operand is UInt16 || operand is UInt32 || operand is UInt64 || operand is Byte))
			{
				return Changebase(operand.ToString(), ENumericBase.Dec, Settings.Default.OperandDisplayBase);
			}

			return operand.ToString();
		}

		#endregion
	}
}