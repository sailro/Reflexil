/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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
using System;
using System.Linq;
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
                                                            EInjectType.Event,
                                                            EInjectType.Resource});

            var ade = new AssemblyDefinitionEditor();
            var adetypes = new EInjectType[] {  EInjectType.Class,
                                                EInjectType.Interface,
                                                EInjectType.Struct, 
                                                EInjectType.Enum,
                                                EInjectType.AssemblyReference,
                                                EInjectType.Resource };
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

            InitializeOwnerType(ade, tde);
		}

	    private void InitializeOwnerType(AssemblyDefinitionEditor ade, TypeDefinitionEditor tde)
	    {
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
                        ItemName.Text = ".ctor";
                        break;
                    case EInjectType.Resource:
                        extratype = typeof (ResourceType);
                        break;
                }

                InitializeExtraType(editor, extratype);
            }
        }

	    private void InitializeExtraType(IOperandEditor editor, Type extratype)
	    {
	        if (extratype != null)
	        {
	            if (extratype.IsEnum)
	            {
	                ExtraType.Visible = false;
	                ExtraTypeList.Visible = true;
	                ExtraTypeList.DataSource = Enum.GetValues(extratype);
	                ExtraTypeList.SelectedIndex = 0;
	            } else
	            {
	                ExtraType.Visible = true;
	                ExtraTypeList.Visible = false;
	                if (editor is AssemblyDefinitionEditor)
	                {
	                    var aeditor = (editor as AssemblyDefinitionEditor);
	                    if (aeditor.SelectedOperand != null)
	                    {
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
                    if (ExtraTypeList.Visible)
                        InjectHelper.Inject(owner, TargetType, ItemName.Text, ExtraTypeList.SelectedItem);
                    else
                        InjectHelper.Inject(owner, TargetType, ItemName.Text, ExtraType.SelectedOperand);
                }
            }
        }

        public DialogResult ShowDialog(EInjectType type)
        {
            TargetType = type;
            OwnerType.Enabled = false;
            OwnerPanel.Enabled = false;
            ItemType.Enabled = false;
            return ShowDialog();
        }

        private void InjectForm_Load(object sender, EventArgs e)
        {
            InjectContextChanged(sender, e);
        }
        #endregion

    }
}
