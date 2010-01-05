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

        #region " Properties "
        public EInjectType TargetType {
            get
            {
                if (ItemType.SelectedIndex >= 0)
                {
                    return (EInjectType) ItemType.SelectedItem;
                }
                throw new ArgumentException();
            }
            set
            {
                foreach (EInjectType type in ItemType.Items)
                {
                    if (type.Equals(value))
                    {
                        ItemType.SelectedItem = type;
                    }
                }
            }
        }
        #endregion

        #region " Fields "
        private Dictionary<object, EInjectType[]> mappings;
        private List<EInjectType> extratypesupported;
        #endregion

        #region " Methods "
        public InjectForm()
		{
			InitializeComponent();
            mappings = new Dictionary<object, EInjectType[]>();
            extratypesupported = new List<EInjectType>();
            extratypesupported.AddRange(new EInjectType[] { EInjectType.Class,
                                                            EInjectType.Property,
                                                            EInjectType.Field,
                                                            EInjectType.Event});

            var ade = new AssemblyDefinitionEditor();
            var adetypes = new EInjectType[] {  EInjectType.Class,
                                                EInjectType.Interface,
                                                EInjectType.Struct, 
                                                EInjectType.Enum,
                                                EInjectType.AssemblyReference };
            mappings.Add(ade, adetypes);
            OwnerType.Items.Add(ade);

            var tde = new TypeDefinitionEditor();
            var tdetypes = new EInjectType[] {  EInjectType.Class,
                                                EInjectType.Interface,
                                                EInjectType.Struct,
                                                EInjectType.Enum,
                                                EInjectType.Constructor,
                                                EInjectType.Method,
                                                EInjectType.Property,
                                                EInjectType.Field,
                                                EInjectType.Event };
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
                else if (current is ModuleDefinition)
                {
                    OwnerType.SelectedItem = ade;
                    ade.SelectedOperand = (current as ModuleDefinition).Assembly;
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
            if (mappings.ContainsKey(OwnerType.SelectedItem)) {
                ItemType.DataSource = mappings[OwnerType.SelectedItem];
                ItemType.SelectedIndex = 0;
            }
            InjectContextChanged(sender, e);
        }

        private void InjectContextChanged(object sender, EventArgs e)
        {
            IOperandEditor editor = (IOperandEditor)OwnerType.SelectedItem;
            if (ItemType.SelectedIndex >= 0)
            {
                EInjectType targettype = (EInjectType)ItemType.SelectedItem;

                ExtraTypePanel.Visible = extratypesupported.Contains(targettype);
                LabExtraType.Visible = extratypesupported.Contains(targettype);
                LabExtraType.Text = targettype.ToString().Replace("Interface", "Base").Replace("Class", "Base") + " type";
                ItemName.Enabled = targettype != EInjectType.Constructor;

                object owner = editor.SelectedOperand;

                String nameprefix = (editor is AssemblyDefinitionEditor) ? "Namespace.Injected" : "InjectedInner";
                ItemName.Text = "Injected" + targettype.ToString();
                Type extratype = null;

                switch (targettype)
                {
                    case EInjectType.Class:
                    case EInjectType.Interface:
                        ItemName.Text = string.Concat(nameprefix, targettype.ToString());
                        extratype = typeof(object);
                        break;
                    case EInjectType.Enum:
                    case EInjectType.Struct:
                        ItemName.Text = string.Concat(nameprefix, targettype.ToString());
                        break;
                    case EInjectType.Property:
                    case EInjectType.Field:
                        extratype = typeof(int);
                        break;
                    case EInjectType.Event:
                        extratype = typeof(EventHandler);
                        break;
                    case EInjectType.AssemblyReference:
                        ItemName.Text = "System.Windows.Forms";
                        break;
                    case EInjectType.Constructor:
                        ItemName.Text = MethodDefinition.Ctor;
                        break;
                }

                if (extratype != null)
                {
                    if (editor is AssemblyDefinitionEditor)
                    {
                        var aeditor = (editor as AssemblyDefinitionEditor);
                        if (aeditor.SelectedOperand != null) {
                            ExtraType.SelectedOperand = aeditor.SelectedOperand.MainModule.Import(extratype);
                        }
                    }
                    else
                    {
                        var teditor = (editor as TypeDefinitionEditor);
                        if (teditor.SelectedOperand != null)
                        {
                            ExtraType.SelectedOperand = teditor.SelectedOperand.Module.Import(extratype);
                        }
                    }
                }
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            IOperandEditor editor = (IOperandEditor)OwnerType.SelectedItem;
            if (editor != null)
            {
                object owner = editor.SelectedOperand;
                if (owner != null && ItemType.SelectedIndex >= 0)
                {
                    InjectHelper.Inject(owner, TargetType, ItemName.Text, ExtraType.SelectedOperand);
                }
            }
        }

        public void ShowDialog(EInjectType type)
        {
            TargetType = type;
            OwnerType.Enabled = false;
            OwnerPanel.Enabled = false;
            ItemType.Enabled = false;
            ShowDialog();
        }

        private void InjectForm_Load(object sender, EventArgs e)
        {
            InjectContextChanged(sender, e);
        }
        #endregion

    }
}
