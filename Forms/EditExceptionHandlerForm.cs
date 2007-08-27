
#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Editors;
using Reflexil.Handlers;
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
                ExceptionHandlerCollection handlers = Handler.MethodDefinition.Body.ExceptionHandlers;
                int index = handlers.IndexOf(Handler.SelectedExceptionHandler);
                handlers.RemoveAt(index);
                handlers.Insert(index, eh);
            }
		}

        private void EditExceptionHandlerForm_Load(Object sender, EventArgs e)
		{
            ExceptionHandler eh = Handler.SelectedExceptionHandler;
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
		#endregion
		
		#region " Methods "
        public EditExceptionHandlerForm() : base()
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


