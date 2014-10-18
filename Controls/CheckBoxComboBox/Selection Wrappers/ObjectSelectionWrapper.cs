using System;
using System.ComponentModel;
using System.Linq;
using System.Data;

namespace Reflexil.Editors
{
    /// <summary>
    /// Used together with the ListSelectionWrapper in order to wrap data sources for a CheckBoxComboBox.
    /// It helps to ensure you don't add an extra "Selected" property to a class that don't really need or want that information.
    /// </summary>
    public class ObjectSelectionWrapper<T> : INotifyPropertyChanged
    {
        public ObjectSelectionWrapper(T item, ListSelectionWrapper<T> container)
        {
	        Count = 0;
	        _container = container;
            Item = item;
        }

        #region Fields
	    /// <summary>
        /// Is this item selected.
        /// </summary>
        private bool _selected;

	    /// <summary>
        /// The containing list for these selections.
        /// </summary>
        private readonly ListSelectionWrapper<T> _container;
        #endregion

        #region Properties
	    /// <summary>
	    /// An indicator of how many items with the specified status is available for the current filter level.
	    /// Thaught this would make the app a bit more user-friendly and help not to miss items in Statusses
	    /// that are not often used.
	    /// </summary>
	    public int Count { get; set; }

	    /// <summary>
	    /// A reference to the item wrapped.
	    /// </summary>
	    public T Item { get; set; }

	    /// <summary>
        /// The item display value. If ShowCount is true, it displays the "Name [Count]".
        /// </summary>
        public string Name
        {
            get 
            {
                string name = null;
                if (string.IsNullOrEmpty(_container.DisplayNameProperty))
                    name = Item.ToString();
                else
                {
	                var dataRow = Item as DataRow;
	                if (dataRow != null) // A specific implementation for DataRow
		                name = ((DataRow)((Object)Item))[_container.DisplayNameProperty].ToString();
	                else
	                {
		                var pds = TypeDescriptor.GetProperties(Item);
		                foreach (var value in
			                pds.Cast<PropertyDescriptor>()
				                .Where(
					                pd => String.Compare(pd.Name, _container.DisplayNameProperty, StringComparison.Ordinal) == 0)
				                .Select(pd => pd.GetValue(Item)))
		                {
			                if (value != null) name = value.ToString();
			                break;
		                }

		                if (!string.IsNullOrEmpty(name))
			                return _container.ShowCounts ? String.Format("{0} [{1}]", name, Count) : name;
	                
		                var pi = Item.GetType().GetProperty(_container.DisplayNameProperty);
		                if (pi == null)
			                throw new Exception(String.Format(
				                "Property {0} cannot be found on {1}.",
				                _container.DisplayNameProperty,
				                Item.GetType()));
		                name = pi.GetValue(Item, null).ToString();
	                }
                }
	            return _container.ShowCounts ? String.Format("{0} [{1}]", name, Count) : name;
            }
        }
        /// <summary>
        /// The textbox display value. The names concatenated.
        /// </summary>
        public string NameConcatenated
        {
            get { return _container.SelectedNames; }
        }
        /// <summary>
        /// Indicates whether the item is selected.
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set 
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("NameConcatenated");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
