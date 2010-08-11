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
using System.Collections;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Editors
{

    public partial class GenericOperandReferenceEditor<T, W> : ComboBox, IOperandEditor<T> where W : Reflexil.Wrappers.IWrapper<T>, new()
	{
		
		#region " Fields "
		private ICollection m_referenceditems;
		#endregion
		
		#region " Properties "
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
                W wrapper = ((W)SelectedItem);
                if (wrapper != null)
                {
                    return wrapper.Item;
                }
                return default(T);
            }
            set
            {
                foreach (W wrapper in Items)
                {
                    if (((object)wrapper.Item) == (object)value)
                    {
                        SelectedItem = wrapper;
                    }
                }
            }
        }
		
		public string Label
		{
			get
			{
				return string.Format("-> {0} reference", ShortLabel);
			}
		}

        public string ShortLabel
        {
            get
            {
                return typeof(W).Name.Replace("Wrapper", string.Empty);
            }
        }

        public ICollection ReferencedItems
        {
            get
            {
                return m_referenceditems;
            }
            set
            {
                m_referenceditems = value;
            }
        }
		#endregion
		
		#region " Methods "
        public GenericOperandReferenceEditor()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public bool IsOperandHandled(object operand)
        {
            return (operand) is T;
        }

        public GenericOperandReferenceEditor(ICollection referenceditems)
            : this()
		{
            this.Dock = DockStyle.Fill;
            this.m_referenceditems = referenceditems;
		}
		
		public void Initialize(MethodDefinition mdef)
		{
			Items.Clear();
			if (mdef.HasBody)
			{
				foreach (T refItem in m_referenceditems)
				{
					W item = new W();
					item.Item = refItem;
					item.MethodDefinition = mdef;
					Items.Add(item);
				}
			}
		}
		
		public Mono.Cecil.Cil.Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return ((W) SelectedItem).CreateInstruction(worker, opcode);
		}
	    #endregion
		
	}
	
}


