
#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Editors;
using Reflexil.Handlers;
using Mono.Cecil.Cil;
using Mono.Cecil;
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

