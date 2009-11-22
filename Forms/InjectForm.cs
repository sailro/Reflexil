/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Editors;
using Reflexil.Handlers;
using Reflexil.Plugins;
using Reflexil.Utils;
#endregion

namespace Reflexil.Forms
{
	public partial class InjectForm: Form
    {

        #region " Inner classes "
        private class TypeWrapper
        {
            public Type Type { get; set; }

            public TypeWrapper(Type deftype)
            {
                Type = deftype;
            }

            public override string ToString()
            {
                return Type.Name.Replace("Definition", string.Empty).Replace("AssemblyNameR","Assembly r");
            }

        }
        #endregion

        #region " Fields "
        private Dictionary<object, TypeWrapper[]> mappings;
        #endregion

        #region " Methods "
        public InjectForm()
		{
			InitializeComponent();
            mappings = new Dictionary<object, TypeWrapper[]>();

            var tdw = new TypeWrapper(typeof(TypeDefinition));
            var mdw = new TypeWrapper(typeof(MethodDefinition));
            var pdw = new TypeWrapper(typeof(PropertyDefinition));
            var fdw = new TypeWrapper(typeof(FieldDefinition));
            var edw = new TypeWrapper(typeof(EventDefinition));
            var arw = new TypeWrapper(typeof(AssemblyNameReference));

            var ade = new AssemblyDefinitionEditor();
            var adetypes = new TypeWrapper[] { tdw, arw };
            mappings.Add(ade, adetypes);
            OwnerType.Items.Add(ade);

            var tde = new TypeDefinitionEditor();
            var tdetypes = new TypeWrapper[]{ tdw, mdw, pdw, fdw, edw };
            mappings.Add(tde, tdetypes);
            OwnerType.Items.Add(tde);

            IHandler handler = PluginFactory.GetInstance().Package.ActiveHandler;
            if (handler != null) {
                object current = handler.TargetObject;
                if (current is AssemblyDefinition)
                {
                    OwnerType.SelectedItem = ade;
                    ade.SelectedOperand = current as AssemblyDefinition;
                }
                else if (current is TypeDefinition)
                {
                    OwnerType.SelectedItem = tde;
                    tde.SelectedOperand = current as TypeDefinition;
                }
                else if (current is MemberReference)
                {
                    MemberReference mref = current as MemberReference;
                    if (mref.DeclaringType is TypeDefinition)
                    {
                        OwnerType.SelectedItem = tde;
                        tde.SelectedOperand = mref.DeclaringType as TypeDefinition;
                    }
                }
            }
        }

        private void OwnerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OwnerPanel.Controls.Clear();
            OwnerPanel.Controls.Add((Control)OwnerType.SelectedItem);
            ItemType.Items.Clear();
            if (mappings.ContainsKey(OwnerType.SelectedItem)) {
                ItemType.Items.AddRange(mappings[OwnerType.SelectedItem]);
                ItemType.SelectedIndex = 0;
            }
            InjectContextChanged(sender, e);
        }

        private void InjectContextChanged(object sender, EventArgs e)
        {
            IOperandEditor editor = (IOperandEditor)OwnerType.SelectedItem;
            if (ItemType.SelectedIndex >= 0)
            {
                Type targettype = (ItemType.SelectedItem as TypeWrapper).Type;
                if (targettype.Equals(typeof(TypeDefinition)))
                {
                    if (editor is AssemblyDefinitionEditor)
                    {
                        ItemName.Text = "Namespace.InjectedType";
                    }
                    else
                    {
                        ItemName.Text = "InjectedInnerType";
                    }
                }
                if (targettype.Equals(typeof(MethodDefinition)))
                {
                    ItemName.Text = "InjectedMethod";
                }
                if (targettype.Equals(typeof(PropertyDefinition)))
                {
                    ItemName.Text = "InjectedProperty";
                }
                if (targettype.Equals(typeof(FieldDefinition)))
                {
                    ItemName.Text = "injectedfield";
                }
                if (targettype.Equals(typeof(EventDefinition)))
                {
                    ItemName.Text = "InjectedEvent";
                }
                if (targettype.Equals(typeof(AssemblyNameReference)))
                {
                    ItemName.Text = "System.Windows.Forms";
                }
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            IOperandEditor editor = (IOperandEditor)OwnerType.SelectedItem;
            object owner = editor.SelectedOperand;
            if (owner != null && ItemType.SelectedIndex >= 0)
            {
                Type targettype = (ItemType.SelectedItem as TypeWrapper).Type;
                PluginFactory.GetInstance().Package.ReflexilWindow.HandleItem(CecilHelper.Inject(owner, targettype, ItemName.Text));
            }

        }
        #endregion

    }
}
