/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System.Collections.Generic;
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
        
        #region " Consts "
        private const string VISIBILITY_MASK = "Visibility";
        private const string LAYOUT_MASK = "Layout";
        private const string CLASS_SEMANTIC_MASK = "Class Semantic";
        private const string STRING_FORMAT_MASK = "String Format";
        private const string VTABLE_LAYOUT_MASK = "VTable layout";
        private const string CODE_TYPE_MASK = "Code type";
        private const string MANAGED_MASK = "Managed";

        private readonly string[] VTABLE_LAYOUT_PROPERTIES = { "IsReuseSlot", "IsNewSlot" };
        private readonly string[] CODE_TYPE_PROPERTIES = { "IsIL", "IsNative", "IsRuntime" };
        private readonly string[] MANAGED_PROPERTIES = { "IsUnmanaged", "IsManaged" };
        private readonly string[] VISIBILITY_PROPERTIES = { "IsCompilerControlled", "IsPrivate", "IsFamilyAndAssembly", "IsAssembly", "IsFamily", "IsFamilyOrAssembly", "IsNotPublic", "IsPublic", "IsNestedPublic", "IsNestedPrivate", "IsNestedFamily", "IsNestedAssembly", "IsNestedFamilyAndAssembly", "IsNestedFamilyOrAssembly" };
        private readonly string[] LAYOUT_PROPERTIES = { "IsAutoLayout", "IsSequentialLayout", "IsExplicitLayout" };
        private readonly string[] CLASS_SEMANTIC_PROPERTIES = { "IsClass", "IsInterface" };
        private readonly string[] STRING_FORMAT_PROPERTIES = { "IsAnsiClass", "IsUnicodeClass", "IsAutoClass" };
        #endregion

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
            FillPrefixes(m_prefixes, VISIBILITY_MASK, VISIBILITY_PROPERTIES);
            FillPrefixes(m_prefixes, LAYOUT_MASK, LAYOUT_PROPERTIES);
            FillPrefixes(m_prefixes, CLASS_SEMANTIC_MASK, CLASS_SEMANTIC_PROPERTIES);
            FillPrefixes(m_prefixes, STRING_FORMAT_MASK, STRING_FORMAT_PROPERTIES);
            FillPrefixes(m_prefixes, VTABLE_LAYOUT_MASK, VTABLE_LAYOUT_PROPERTIES);
            FillPrefixes(m_prefixes, CODE_TYPE_MASK, CODE_TYPE_PROPERTIES);
            FillPrefixes(m_prefixes, MANAGED_MASK, MANAGED_PROPERTIES);
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
