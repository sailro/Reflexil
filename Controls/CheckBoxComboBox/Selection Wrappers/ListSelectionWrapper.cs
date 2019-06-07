// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Reflexil.Editors
{
	public class ListSelectionWrapper<T> : List<ObjectSelectionWrapper<T>>
	{
		public ListSelectionWrapper(IEnumerable source) : this(source, false)
		{
		}

		public ListSelectionWrapper(IEnumerable source, bool showCounts)
		{
			_source = source;
			ShowCounts = showCounts;

			var list = _source as IBindingList;
			if (list != null)
				list.ListChanged += ListSelectionWrapper_ListChanged;

			Populate();
		}

		public ListSelectionWrapper(IEnumerable source, string usePropertyAsDisplayName) : this(source, false, usePropertyAsDisplayName)
		{
		}

		public ListSelectionWrapper(IEnumerable source, bool showCounts, string usePropertyAsDisplayName) : this(source, showCounts)
		{
			DisplayNameProperty = usePropertyAsDisplayName;
		}

		private readonly IEnumerable _source;

		public string DisplayNameProperty { get; set; }

		public string SelectedNames
		{
			get
			{
				return this.Where(item => item.Selected)
					.Aggregate("",
						(current, item) =>
							current +
							(string.IsNullOrEmpty(current) ? string.Format("\"{0}\"", item.Name) : string.Format(" & \"{0}\"", item.Name)));
			}
		}

		public bool ShowCounts { get; set; }

		public void ClearCounts()
		{
			foreach (var item in this)
				item.Count = 0;
		}

		private ObjectSelectionWrapper<T> CreateSelectionWrapper(IEnumerator Object)
		{
			var types = new[] {typeof(T), GetType()};
			var ci = typeof(ObjectSelectionWrapper<T>).GetConstructor(types);
			if (ci == null)
				throw new Exception(string.Format(
					"The selection wrapper class {0} must have a constructor with ({1} Item, {2} Container) parameters.",
					typeof(ObjectSelectionWrapper<T>),
					typeof(T),
					GetType()));
			var parameters = new[] {Object.Current, this};
			var result = ci.Invoke(parameters);
			return (ObjectSelectionWrapper<T>)result;
		}

		public ObjectSelectionWrapper<T> FindObjectWithItem(T Object)
		{
			return Find(target => target.Item.Equals(Object));
		}

		private void Populate()
		{
			Clear();

			var enumerator = _source.GetEnumerator();
			while (enumerator.MoveNext())
				Add(CreateSelectionWrapper(enumerator));
		}

		private void ListSelectionWrapper_ListChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
				case ListChangedType.ItemAdded:
					Add(CreateSelectionWrapper((IEnumerator)((IBindingList)_source)[e.NewIndex]));
					break;
				case ListChangedType.ItemDeleted:
					Remove(FindObjectWithItem((T)((IBindingList)_source)[e.OldIndex]));
					break;
				case ListChangedType.Reset:
					Populate();
					break;
			}
		}
	}
}
