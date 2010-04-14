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
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class NoneOperandEditor : TextBox, IOperandEditor<object>
	{
		
		#region " Properties "
		public string Label
		{
			get
			{
				return "[None]";
			}
		}

        public string ShortLabel
        {
            get
            {
                return Label;
            }
        }

        public object SelectedOperand
        {
            get
            {
                return null;
            }
            set
            {
			    Text = value.ToString();
            }
        }
		#endregion
		
		#region " Methods "
		public NoneOperandEditor() : base()
		{
			this.Dock = DockStyle.Fill;
			this.ReadOnly = true;
		}

        public bool IsOperandHandled(object operand)
        {
            return false;
        }

		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode);
		}
		
		public void Initialize(MethodDefinition mdef)
		{
		}
		#endregion
		
	}
	
}

