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
		#endregion
		
	}
	
}


