
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


