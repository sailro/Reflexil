
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Split view control: attributes editor on the left, custom controls on the right
    /// </summary>
    /// <typeparam name="T">Target object type</typeparam>
	public partial class SplitAttributesControl<T>: UserControl where T:class 
	{

        #region " Fields "
        private bool m_readonly;
        protected Dictionary<string, string> m_prefixes = new Dictionary<string, string>();
        #endregion

        #region " Properties "
        public bool ReadOnly
        {
            get
            {
                return m_readonly;
            }
            set
            {
                m_readonly = value;
                Enabled = !value;
            }
        }

        public T Item
        {
            get
            {
                return Attributes.Item as T;
            }
            set
            {
                Attributes.Item = value;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public SplitAttributesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fills a dictionary 
        /// </summary>
        /// <param name="prefixes">Work dictionary</param>
        /// <param name="prefix">value</param>
        /// <param name="items">keys</param>
        protected static void FillPrefixes(Dictionary<string, string> prefixes, string prefix, string[] items)
        {
            foreach (string item in items)
            {
                prefixes.Add(item, prefix);
            }
        }

        /// <summary>
        /// Bind an item to this control
        /// </summary>
        /// <param name="item">Control to bind</param>
        public virtual void Bind(T item)
        {
            Attributes.Bind(item, m_prefixes);
            if (!ReadOnly)
            {
                Enabled = (item != null);
            }
        }
        #endregion

	}
}
