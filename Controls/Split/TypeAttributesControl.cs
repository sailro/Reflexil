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
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Type attributes editor (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class TypeAttributesControl : BaseTypeAttributesControl
    {

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public TypeAttributesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bind a type definition to this control
        /// </summary>
        /// <param name="tdef">Type definition to bind</param>
        public override void Bind(TypeDefinition tdef)
        {
            base.Bind(tdef);
            if (tdef != null)
            {
                BaseType.SelectedOperand = tdef.BaseType;
            }
            else
            {
                BaseType.SelectedOperand = null;
            }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// Commit changes to the TypeDefinition
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void BaseType_Validated(object sender, System.EventArgs e)
        {
            TypeReference tref = BaseType.SelectedOperand;
            if (tref != null) {
                Item.BaseType = Item.Module.Import(tref);
            } else {
                Item.BaseType = null;
            }
        }
        #endregion

    }

    #region " VS Designer generic support "
    public class BaseTypeAttributesControl : SplitAttributesControl<TypeDefinition>
    {
    }
    #endregion
}
