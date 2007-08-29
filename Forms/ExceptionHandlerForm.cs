
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections;
using System.Windows.Forms;
using Reflexil.Handlers;
using Reflexil.Utils;
using Reflexil.Editors;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Forms
{
	
	public partial class ExceptionHandlerForm 
	{
		
		#region " Fields "
		private MethodDefinitionHandler m_handler;
		#endregion
		
		#region " Properties "
        public MethodDefinitionHandler Handler
        {
            get
            {
                return m_handler;
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

		public void FillControls(MethodDefinitionHandler handler)
		{
            foreach (InstructionReferenceEditor ire in new InstructionReferenceEditor[] { TryStart, TryEnd, HandlerStart, HandlerEnd, FilterStart, FilterEnd })
            {
                ire.ReferencedItems = handler.MethodDefinition.Body.Instructions;
                ire.Initialize(handler.MethodDefinition);
            }
            
            Types.Items.AddRange(new ArrayList(System.Enum.GetValues(typeof(ExceptionHandlerType))).ToArray());
            Types.SelectedIndex = 0;
		}
		
		public virtual DialogResult ShowDialog(MethodDefinitionHandler handler)
		{
			m_handler = handler;
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
                    eh.CatchType = Handler.MethodDefinition.DeclaringType.Module.Import(CatchType.SelectedOperand);
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


