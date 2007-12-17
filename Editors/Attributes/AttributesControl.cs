
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Attributes editor control (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class AttributesControl: UserControl
    {

        #region " Fields "
        private object m_item = null;
        private bool m_refreshingFlags = false;
        #endregion

        #region " Properties "
        public object Item
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public AttributesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bind an object to this control
        /// </summary>
        /// <param name="item">Object to bind</param>
        /// <param name="prefixes">Grouping prefixes</param>
        public void Bind(object item, Dictionary<string, string> prefixes)
        {
            Flags.Items.Clear();
            if (item != null)
            {
                foreach (PropertyInfo pinfo in item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if ((pinfo.PropertyType == typeof(bool)) && pinfo.CanRead && pinfo.CanWrite)
                    {
                        Flags.Items.Add(new PropertyWrapper(pinfo, prefixes));
                    }
                }
            }
            m_item = item;
            RefreshFlags();
        }

        /// <summary>
        /// Bind an object to this control
        /// </summary>
        /// <param name="item">Object to bind</param>
        public void Bind(object item)
        {
            Bind(item, new Dictionary<string, string>()); 
        }

        /// <summary>
        /// Refresh attributes from object context
        /// </summary>
        public void RefreshFlags()
        {
            if (m_item == null)
            {
                Flags.ClearSelected();
            }
            else
            {
                m_refreshingFlags = true;
                for (int i = 0; i < Flags.Items.Count; i++)
                {
                    PropertyWrapper wrapper = (PropertyWrapper)Flags.Items[i];
                    Flags.SetItemChecked(i, (bool)wrapper.PropertyInfo.GetValue(m_item, null));
                }
                m_refreshingFlags = false;
            }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// Handle item checking
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">attributes</param>
        private void Flags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!m_refreshingFlags && m_item != null)
            {
                PropertyWrapper wrapper = (PropertyWrapper)Flags.Items[e.Index];
                wrapper.PropertyInfo.SetValue(m_item, e.NewValue == CheckState.Checked, null);
                RefreshFlags();
            }
        }
        #endregion

    }
}
