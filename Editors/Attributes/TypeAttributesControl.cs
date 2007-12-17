/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Type attributes editor (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class TypeAttributesControl : SplitAttributesControl<TypeDefinition>
    {
        #region " Consts "
        private const string VISIBILITY_MASK = "Visibility";
        private const string LAYOUT_MASK = "Layout";
        private const string CLASS_SEMANTIC_MASK = "Class Semantic";
        private const string STRING_FORMAT_MASK = "String Format";

        private readonly string[] VISIBILITY_PROPERTIES = { "IsNotPublic", "IsPublic", "IsNestedPublic", "IsNestedPrivate", "IsNestedFamily", "IsNestedAssembly", "IsNestedFamilyAndAssembly", "IsNestedFamilyOrAssembly" };
        private readonly string[] LAYOUT_PROPERTIES = { "IsAutoLayout", "IsSequentialLayout", "IsExplicitLayout" };
        private readonly string[] CLASS_SEMANTIC_PROPERTIES = { "IsClass", "IsInterface" };
        private readonly string[] STRING_FORMAT_PROPERTIES = { "IsAnsiClass", "IsUnicodeClass", "IsAutoClass" };
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public TypeAttributesControl()
        {
            InitializeComponent();
            FillPrefixes(m_prefixes, VISIBILITY_MASK, VISIBILITY_PROPERTIES);
            FillPrefixes(m_prefixes, LAYOUT_MASK, LAYOUT_PROPERTIES);
            FillPrefixes(m_prefixes, CLASS_SEMANTIC_MASK, CLASS_SEMANTIC_PROPERTIES);
            FillPrefixes(m_prefixes, STRING_FORMAT_MASK, STRING_FORMAT_PROPERTIES);
        }
        #endregion
    }
}
