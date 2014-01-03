using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        #region CONSTRUCTOR

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
            : base()
        {
            _Source = source;
            _ShowCounts = showCounts;
            if (_Source is IBindingList)
                ((IBindingList)_Source).ListChanged += new ListChangedEventHandler(ListSelectionWrapper_ListChanged);
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
            _DisplayNameProperty = usePropertyAsDisplayName;
        }

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// Is a Count indicator used.
        /// </summary>
        private bool _ShowCounts;
        /// <summary>
        /// The original List of values wrapped. A "Selected" and possibly "Count" functionality is added.
        /// </summary>
        private IEnumerable _Source;
        /// <summary>
        /// Used to indicate NOT to use ToString(), but read this property instead as a display value.
        /// </summary>
        private string _DisplayNameProperty = null;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// When specified, indicates that ToString() should not be performed on the items. 
        /// This property will be read instead. 
        /// This is specifically useful on DataTable implementations, where PropertyDescriptors are used to read the values.
        /// </summary>
        public string DisplayNameProperty
        {
            get { return _DisplayNameProperty; }
            set { _DisplayNameProperty = value; }
        }
        /// <summary>
        /// Builds a concatenation list of selected items in the list.
        /// </summary>
        public string SelectedNames
        {
            get
            {
                string Text = "";
                foreach (ObjectSelectionWrapper<T> Item in this)
                    if (Item.Selected)
                        Text += (
                            string.IsNullOrEmpty(Text)
                            ? String.Format("\"{0}\"", Item.Name)
                            : String.Format(" & \"{0}\"", Item.Name));
                return Text;
            }
        }
        /// <summary>
        /// Indicates whether the Item display value (Name) should include a count.
        /// </summary>
        public bool ShowCounts
        {
            get { return _ShowCounts; }
            set { _ShowCounts = value; }
        }

        #endregion

        #region HELPER MEMBERS

        /// <summary>
        /// Reset all counts to zero.
        /// </summary>
        public void ClearCounts()
        {
            foreach (ObjectSelectionWrapper<T> Item in this)
                Item.Count = 0;
        }
        /// <summary>
        /// Creates a ObjectSelectionWrapper item.
        /// Note that the constructor signature of sub classes classes are important.
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        private ObjectSelectionWrapper<T> CreateSelectionWrapper(IEnumerator Object)
        {
            Type[] Types = new Type[] { typeof(T), this.GetType() };
            ConstructorInfo CI = typeof(ObjectSelectionWrapper<T>).GetConstructor(Types);
            if (CI == null)
                throw new Exception(String.Format(
                              "The selection wrapper class {0} must have a constructor with ({1} Item, {2} Container) parameters.",
                              typeof(ObjectSelectionWrapper<T>),
                              typeof(T),
                              this.GetType()));
            object[] parameters = new object[] { Object.Current, this };
            object result = CI.Invoke(parameters);
            return (ObjectSelectionWrapper<T>)result;
        }

        public ObjectSelectionWrapper<T> FindObjectWithItem(T Object)
        {
            return Find(new Predicate<ObjectSelectionWrapper<T>>(
                            delegate(ObjectSelectionWrapper<T> target)
                            {
                                return target.Item.Equals(Object);
                            }));
        }

        /*
        public TSelectionWrapper FindObjectWithKey(object key)
        {
            return FindObjectWithKey(new object[] { key });
        }

        public TSelectionWrapper FindObjectWithKey(object[] keys)
        {
            return Find(new Predicate<TSelectionWrapper>(
                            delegate(TSelectionWrapper target)
                            {
                                return
                                    ReflectionHelper.CompareKeyValues(
                                        ReflectionHelper.GetKeyValuesFromObject(target.Item, target.Item.TableInfo),
                                        keys);
                            }));
        }

        public object[] GetArrayOfSelectedKeys()
        {
            List<object> List = new List<object>();
            foreach (TSelectionWrapper Item in this)
                if (Item.Selected)
                {
                    if (Item.Item.TableInfo.KeyProperties.Length == 1)
                        List.Add(ReflectionHelper.GetKeyValueFromObject(Item.Item, Item.Item.TableInfo));
                    else
                        List.Add(ReflectionHelper.GetKeyValuesFromObject(Item.Item, Item.Item.TableInfo));
                }
            return List.ToArray();
        }

        public T[] GetArrayOfSelectedKeys<T>()
        {
            List<T> List = new List<T>();
            foreach (TSelectionWrapper Item in this)
                if (Item.Selected)
                {
                    if (Item.Item.TableInfo.KeyProperties.Length == 1)
                        List.Add((T)ReflectionHelper.GetKeyValueFromObject(Item.Item, Item.Item.TableInfo));
                    else
                        throw new LibraryException("This generator only supports single value keys.");
                    // List.Add((T)ReflectionHelper.GetKeyValuesFromObject(Item.Item, Item.Item.TableInfo));
                }
            return List.ToArray();
        }
        */
        private void Populate()
        {
            Clear();
            /*
            for(int Index = 0; Index <= _Source.Count -1; Index++)
                Add(CreateSelectionWrapper(_Source[Index]));
             */
            IEnumerator Enumerator = _Source.GetEnumerator();
            if (Enumerator != null)
                while (Enumerator.MoveNext())
                    Add(CreateSelectionWrapper(Enumerator));
        }

        #endregion

        #region EVENT HANDLERS

        private void ListSelectionWrapper_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    Add(CreateSelectionWrapper((IEnumerator)((IBindingList)_Source)[e.NewIndex]));
                    break;
                case ListChangedType.ItemDeleted:
                    Remove(FindObjectWithItem((T)((IBindingList)_Source)[e.OldIndex]));
                    break;
                case ListChangedType.Reset:
                    Populate();
                    break;
            }
        }

        #endregion
    }
}
