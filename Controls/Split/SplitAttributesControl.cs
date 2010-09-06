/* Reflexil Copyright (c) 2007-2010 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

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
