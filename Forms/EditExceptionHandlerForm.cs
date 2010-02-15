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

namespace Reflexil.Forms
{
	
	public partial class EditExceptionHandlerForm
	{
		
		#region " Events "
		private void ButUpdate_Click(Object sender, EventArgs e)
		{
            ExceptionHandler eh = CreateExceptionHandler();
            if (eh != null)
            {
                ExceptionHandlerCollection handlers = MethodDefinition.Body.ExceptionHandlers;
                int index = handlers.IndexOf(SelectedExceptionHandler);
                handlers.RemoveAt(index);
                handlers.Insert(index, eh);
            }
		}

        private void EditExceptionHandlerForm_Load(Object sender, EventArgs e)
		{
            ExceptionHandler eh = SelectedExceptionHandler;
            if (eh != null)
            {
                Types.SelectedItem = eh.Type;
                CatchType.SelectedOperand = eh.CatchType;
                TryStart.SelectedOperand = eh.TryStart;
                TryEnd.SelectedOperand = eh.TryEnd;
                HandlerStart.SelectedOperand = eh.HandlerStart;
                HandlerEnd.SelectedOperand = eh.HandlerEnd;
                FilterStart.SelectedOperand = eh.FilterStart;
                FilterEnd.SelectedOperand = eh.FilterEnd;
            }
		}

        public override DialogResult ShowDialog(MethodDefinition mdef, ExceptionHandler selected)
        {
            FillControls(mdef);
            return base.ShowDialog(mdef, selected);
        }
		#endregion
		
		#region " Methods "
        public EditExceptionHandlerForm() : base()
        {
            InitializeComponent();
        }
		#endregion
	
	}
	
}


