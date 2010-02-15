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
using System.Collections;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Editors;

#endregion

namespace Reflexil.Forms
{
	
	public partial class ExceptionHandlerForm 
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
        private ExceptionHandler m_selectedexceptionhandler;
		#endregion
		
		#region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public ExceptionHandler SelectedExceptionHandler
        {
            get
            {
                return m_selectedexceptionhandler;
            }
        }
		#endregion
		
		#region " Events "
        private void Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Types.SelectedItem != null)
            {
                ExceptionHandlerType ehtype = (ExceptionHandlerType)Types.SelectedItem;
                if (ehtype == ExceptionHandlerType.Filter)
                {
                    FilterStart.Enabled = FilterEnd.Enabled = true;
                }
                else
                {
                    FilterStart.Enabled = FilterEnd.Enabled = false;
                    FilterStart.Text = FilterEnd.Text = string.Empty;
                }
            }
        }
		#endregion
		
		#region " Methods "
        public ExceptionHandlerForm() : base()
        {
            InitializeComponent();
            CatchType.Dock = DockStyle.None;
        }

        public void FillControls(MethodDefinition mdef)
		{
            foreach (InstructionReferenceEditor ire in new InstructionReferenceEditor[] { TryStart, TryEnd, HandlerStart, HandlerEnd, FilterStart, FilterEnd })
            {
                ire.ReferencedItems = mdef.Body.Instructions;
                ire.Initialize(mdef);
            }
            
            Types.Items.AddRange(new ArrayList(System.Enum.GetValues(typeof(ExceptionHandlerType))).ToArray());
            Types.SelectedIndex = 0;
		}
		
		public virtual DialogResult ShowDialog(MethodDefinition mdef, ExceptionHandler selected)
		{
            m_mdef = mdef;
            m_selectedexceptionhandler = selected;
			return base.ShowDialog();
		}

        protected ExceptionHandler CreateExceptionHandler()
		{
            try
            {
                ExceptionHandler eh = new ExceptionHandler((ExceptionHandlerType)Types.SelectedItem);
                if (eh.Type == ExceptionHandlerType.Filter)
                {
                    eh.FilterStart = FilterStart.SelectedOperand;
                    eh.FilterEnd = FilterEnd.SelectedOperand;
                }
                eh.TryStart = TryStart.SelectedOperand;
                eh.TryEnd = TryEnd.SelectedOperand;
                eh.HandlerStart = HandlerStart.SelectedOperand;
                eh.HandlerEnd = HandlerEnd.SelectedOperand;
                if (CatchType.SelectedOperand != null)
                {
                    eh.CatchType = MethodDefinition.DeclaringType.Module.Import(CatchType.SelectedOperand);
                }
                return eh;
            }
            catch (Exception)
            {
                MessageBox.Show("Reflexil is unable to create this exception handler");
                return null;
            }
		}
		#endregion
    }
	
}


