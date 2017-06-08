// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
using System;
using System.ComponentModel;
using System.Linq;
using System.Data;

namespace Reflexil.Editors
{
	public class ObjectSelectionWrapper<T> : INotifyPropertyChanged
	{
		public ObjectSelectionWrapper(T item, ListSelectionWrapper<T> container)
		{
			Count = 0;
			_container = container;
			Item = item;
		}

		private bool _selected;
		private readonly ListSelectionWrapper<T> _container;

		public int Count { get; set; }
		public T Item { get; set; }

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
						name = ((DataRow) (object) Item)[_container.DisplayNameProperty].ToString();
					else
					{
						var pds = TypeDescriptor.GetProperties(Item);
						foreach (var value in
							pds.Cast<PropertyDescriptor>()
								.Where(
									pd => string.Compare(pd.Name, _container.DisplayNameProperty, StringComparison.Ordinal) == 0)
								.Select(pd => pd.GetValue(Item)))
						{
							if (value != null) name = value.ToString();
							break;
						}

						if (!string.IsNullOrEmpty(name))
							return _container.ShowCounts ? string.Format("{0} [{1}]", name, Count) : name;

						var pi = Item.GetType().GetProperty(_container.DisplayNameProperty);
						if (pi == null)
							throw new Exception(string.Format(
								"Property {0} cannot be found on {1}.",
								_container.DisplayNameProperty,
								Item.GetType()));
						name = pi.GetValue(Item, null).ToString();
					}
				}
				return _container.ShowCounts ? string.Format("{0} [{1}]", name, Count) : name;
			}
		}

		public string NameConcatenated
		{
			get { return _container.SelectedNames; }
		}

		public bool Selected
		{
			get { return _selected; }
			set
			{
				if (_selected == value)
					return;

				_selected = value;
				OnPropertyChanged("Selected");
				OnPropertyChanged("NameConcatenated");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}