
#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Editors;
using Reflexil.Handlers;
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
                ExceptionHandlerCollection handlers = Handler.MethodDefinition.Body.ExceptionHandlers;
                handlers.Insert(handlers.IndexOf(Handler.SelectedExceptionHandler), neweh);
            }
		}
		
		private void ButInsertAfter_Click(System.Object sender, System.EventArgs e)
		{
            ExceptionHandler neweh = CreateExceptionHandler();
            if (neweh != null)
            {
                ExceptionHandlerCollection handlers = Handler.MethodDefinition.Body.ExceptionHandlers;
                handlers.Insert(handlers.IndexOf(Handler.SelectedExceptionHandler)+1, neweh);
            }
		}
		
		private void ButAppend_Click(System.Object sender, System.EventArgs e)
		{
            ExceptionHandler neweh = CreateExceptionHandler();
            if (neweh != null)
            {
                ExceptionHandlerCollection handlers = Handler.MethodDefinition.Body.ExceptionHandlers;
                handlers.Add(neweh);
            }
        }
		#endregion
		
		#region " Methods "
        public CreateExceptionHandlerForm() : base()
        {
            InitializeComponent();
        }

		public override DialogResult ShowDialog(MethodDefinitionHandler handler)
		{
			FillControls(handler);
			return base.ShowDialog(handler);
		}
		#endregion
		
	}
	
}

