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
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public abstract partial class GenericOperandEditor<T> : TextComboUserControl, IOperandEditor<T>
	{
		
		#region " Properties "
        public string Label
		{
			get
			{
				return typeof(T).Name;
			}
		}

        public string ShortLabel
        {
            get
            {
                return Label;
            }
        }

        object IOperandEditor.SelectedOperand
        {
            get
            {
                return SelectedOperand;
            }
            set
            {
                SelectedOperand = (T)value;
            }
        }

		public T SelectedOperand
		{
			get
			{
                try
                {
                    return ((T)(Convert.ChangeType(Value, typeof(T))));
                }
                catch
                {
                    return default(T);
                }
			}
            set
            {
                Value = value.ToString();
            }
		}
		#endregion
		
		#region " Methods "
		public GenericOperandEditor()
		{
			this.Dock = DockStyle.Fill;
		}

        public bool IsOperandHandled(object operand)
        {
            return (operand) is T;
        }
		
		public abstract Instruction CreateInstruction(ILProcessor worker, OpCode opcode);
		
		public void Initialize(MethodDefinition mdef)
		{
		}
		#endregion
		
	}
	
}

