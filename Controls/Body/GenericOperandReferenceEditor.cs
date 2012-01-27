/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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


