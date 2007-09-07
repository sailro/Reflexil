
#region " Imports "
using System;
using Reflector.CodeModel;
#endregion

namespace Reflexil.Handlers
{
	
	public partial class NotSupportedHandler : IHandler
	{
				
		#region " Properties "
		public bool IsItemHandled(object item)
		{
			return true;
		}
		
		public string Label
		{
			get
			{
				return "Unsupported item";
			}
		}
		#endregion

        #region " Events "
        public void OnConfigurationChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region " Methods "
        public NotSupportedHandler() : base()
        {
            InitializeComponent();
        }

		public void HandleItem(object item)
		{
		}
		#endregion
		
	}
	
}

