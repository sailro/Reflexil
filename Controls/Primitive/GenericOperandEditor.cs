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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;

#endregion

namespace Reflexil.Editors
{
	public abstract class GenericOperandEditor<T> : TextComboUserControl, IOperandsEditor<T>
	{
		#region Events

		public event EventHandler SelectedOperandChanged;

		#endregion

		#region Properties

		public virtual string Label
		{
			get { return typeof (T).Name; }
		}

		public string ShortLabel
		{
			get { return Label; }
		}

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (T) value; }
		}

		object IOperandsEditor.SelectedOperands
		{
			get { return SelectedOperands; }
			set { SelectedOperands = (T[]) value; }
		}

		public T[] SelectedOperands
		{
			get
			{
				var values = Value.Split(OperandDisplayHelper.ItemSeparator);
				var result = new List<T>();
				foreach (var value in values)
				{
					try
					{
						result.Add((T) (Convert.ChangeType(value, typeof (T))));
					}
					catch
					{
						result.Add(default(T));
					}
				}
				return result.ToArray();
			}
			set
			{
				var sb = new StringBuilder();
				if (value != null)
				{
					for (var i = 0; i < value.Length; i++)
					{
						if (i > 0)
							sb.Append(OperandDisplayHelper.ItemSeparator);
						sb.Append(value[i]);
					}
				}
				Value = sb.ToString();
			}
		}

		public virtual T SelectedOperand
		{
			get
			{
				try
				{
					return ((T) (Convert.ChangeType(Value, typeof (T))));
				}
				catch
				{
					return default(T);
				}
			}
			set
			{
				Value = value.ToString();
				if (SelectedOperandChanged != null)
					SelectedOperandChanged(this, EventArgs.Empty);
			}
		}

		#endregion

		#region Methods

		protected GenericOperandEditor()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
		}

		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}

		public bool IsOperandsHandled(object operands)
		{
			return (operands) is T[];
		}

		public abstract Instruction CreateInstruction(ILProcessor worker, OpCode opcode);

		public void Initialize(MethodDefinition mdef)
		{
		}

		#endregion
	}
}