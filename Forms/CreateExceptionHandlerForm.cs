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
	
	public partial class CreateExceptionHandlerForm
	{
		
		#region " Events "
		private void ButInsertBefore_Click(System.Object sender, System.EventArgs e)
		{
            ExceptionHandler neweh = CreateExceptionHandler();
            if (neweh != null)
            {
                ExceptionHandlerCollection handlers = MethodDefinition.Body.ExceptionHandlers;
                handlers.Insert(handlers.IndexOf(SelectedExceptionHandler), neweh);
            }
		}
		
		private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
		{
            ExceptionHandler neweh = CreateExceptionHandler();
            if (neweh != null)
            {
                ExceptionHandlerCollection handlers = MethodDefinition.Body.ExceptionHandlers;
                handlers.Insert(handlers.IndexOf(SelectedExceptionHandler) + 1, neweh);
            }
		}
		
		private void ButAppend_Click(System.Object sender, System.EventArgs e)
		{
            ExceptionHandler neweh = CreateExceptionHandler();
            if (neweh != null)
            {
                ExceptionHandlerCollection handlers = MethodDefinition.Body.ExceptionHandlers;
                handlers.Add(neweh);
            }
        }

        private void CreateExceptionHandlerForm_Load(object sender, EventArgs e)
        {
            ButInsertBefore.Enabled = (SelectedExceptionHandler != null);
            ButInsertAfter.Enabled = (SelectedExceptionHandler != null);
        }
		#endregion
		
		#region " Methods "
        public CreateExceptionHandlerForm() : base()
        {
            InitializeComponent();
        }

        public override DialogResult ShowDialog(MethodDefinition mdef, ExceptionHandler selected)
		{
            FillControls(mdef);
            return base.ShowDialog(mdef, selected);
		}
		#endregion

	}
	
}

