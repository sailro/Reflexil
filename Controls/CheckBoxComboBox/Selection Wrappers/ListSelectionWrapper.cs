using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Reflexil.Editors
{
    /// <summary>
    /// Maintains an additional "Selected" & "Count" value for each item in a List.
    /// Useful in the CheckBoxComboBox. It holds a reference to the List[Index] Item and 
    /// whether it is selected or not.
    /// It also caters for a Count, if needed.
    /// </summary>
    public class ListSelectionWrapper<T> : List<ObjectSelectionWrapper<T>>
    {
        #region Constructors

        /// <summary>
        /// No property on the object is specified for display purposes, so simple ToString() operation 
        /// will be performed. And no Counts will be displayed
        /// </summary>
        public ListSelectionWrapper(IEnumerable source) : this(source, false) { }
        /// <summary>
        /// No property on the object is specified for display purposes, so simple ToString() operation 
        /// will be performed.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, bool showCounts)
        {
            _source = source;
            ShowCounts = showCounts;

			var list = _source as IBindingList;
	        if (list != null)
                list.ListChanged += ListSelectionWrapper_ListChanged;

			Populate();
        }
        /// <summary>
        /// A Display "Name" property is specified. ToString() will not be performed on items.
        /// This is specifically useful on DataTable implementations, or where PropertyDescriptors are used to read the values.
        /// If a PropertyDescriptor is not found, a Property will be used.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, string usePropertyAsDisplayName) : this(source, false, usePropertyAsDisplayName) { }
        /// <summary>
        /// A Display "Name" property is specified. ToString() will not be performed on items.
        /// This is specifically useful on DataTable implementations, or where PropertyDescriptors are used to read the values.
        /// If a PropertyDescriptor is not found, a Property will be used.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, bool showCounts, string usePropertyAsDisplayName)
            : this(source, showCounts)
        {
            DisplayNameProperty = usePropertyAsDisplayName;
        }

        #endregion

        #region Properties

	    /// <summary>
        /// The original List of values wrapped. A "Selected" and possibly "Count" functionality is added.
        /// </summary>
        private readonly IEnumerable _source;

	    /// <summary>
	    /// When specified, indicates that ToString() should not be performed on the items. 
	    /// This property will be read instead. 
	    /// This is specifically useful on DataTable implementations, where PropertyDescriptors are used to read the values.
	    /// </summary>
	    public string DisplayNameProperty { get; set; }

	    /// <summary>
        /// Builds a concatenation list of selected items in the list.
        /// </summary>
        public string SelectedNames
        {
            get
            {
	            return this.Where(item => item.Selected).Aggregate("", (current, item) => current + (string.IsNullOrEmpty(current) ? String.Format("\"{0}\"", item.Name) : String.Format(" & \"{0}\"", item.Name)));
            }
        }

	    /// <summary>
	    /// Indicates whether the Item display value (Name) should include a count.
	    /// </summary>
	    public bool ShowCounts { get; set; }

	    #endregion

        #region Methods

        /// <summary>
        /// Reset all counts to zero.
        /// </summary>
        public void ClearCounts()
        {
            foreach (var item in this)
                item.Count = 0;
        }
        /// <summary>
        /// Creates a ObjectSelectionWrapper item.
        /// Note that the constructor signature of sub classes classes are important.
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        private ObjectSelectionWrapper<T> CreateSelectionWrapper(IEnumerator Object)
        {
            var types = new[] { typeof(T), GetType() };
            var ci = typeof(ObjectSelectionWrapper<T>).GetConstructor(types);
            if (ci == null)
                throw new Exception(String.Format(
                              "The selection wrapper class {0} must have a constructor with ({1} Item, {2} Container) parameters.",
                              typeof(ObjectSelectionWrapper<T>),
                              typeof(T),
                              GetType()));
            var parameters = new[] { Object.Current, this };
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

        #endregion

        #region Events

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

        #endregion
    }
}
