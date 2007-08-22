
#region " Imports "
using System;
using System.Windows.Forms;
#endregion

namespace Reflexil.Editors
{
	
	public abstract partial class BasePopupEditor
	{
		
		#region " Events "
		protected abstract void OnSelectClick(System.Object sender, System.EventArgs e);
		#endregion

        #region " Methods "
        public BasePopupEditor()
            : base()
        {
            InitializeComponent();
        }
        #endregion
		
	}
	
}

