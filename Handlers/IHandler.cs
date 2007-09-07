
#region " Imports "
using System;
#endregion

namespace Reflexil.Handlers
{
	
	public interface IHandler
	{
		
		#region " Properties "
		bool IsItemHandled(object item);
		string Label{
			get;
		}
		#endregion
		
		#region " Methods "
		void HandleItem(object item);
        void OnConfigurationChanged(object sender, EventArgs e);
		#endregion
		
	}
	
}


