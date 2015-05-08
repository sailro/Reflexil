/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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

#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Editors;
using Reflexil.Plugins;
using Reflexil.Utils;

#endregion

namespace Reflexil.Forms
{
	public partial class InjectForm : Form
	{
		#region Properties

		public InjectType TargetType
		{
			get
			{
				if (ItemType.SelectedIndex >= 0)
					return (InjectType) ItemType.SelectedItem;

				throw new ArgumentException();
			}
			set
			{
				foreach (var type in ItemType.Items.Cast<InjectType>().Where(type => type.Equals(value)))
					ItemType.SelectedItem = type;
			}
		}

		#endregion

		#region Fields

		private readonly Dictionary<object, InjectType[]> _mappings;
		private readonly List<InjectType> _extraTypeSupported;

		#endregion

		#region Methods

		public InjectForm()
		{
			InitializeComponent();
			_mappings = new Dictionary<object, InjectType[]>();
			_extraTypeSupported = new List<InjectType>();
			_extraTypeSupported.AddRange(new[]
			{
				InjectType.Class,
				InjectType.Property,
				InjectType.Field,
				InjectType.Event,
				InjectType.Resource
			});

			var ade = new AssemblyDefinitionEditor();
			var adetypes = new[]
			{
				InjectType.Class,
				InjectType.Interface,
				InjectType.Struct,
				InjectType.Enum,
				InjectType.AssemblyReference,
				InjectType.Resource
			};

			_mappings.Add(ade, adetypes);
			OwnerType.Items.Add(ade);

			var tde = new TypeDefinitionEditor();
			var tdetypes = new[]
			{
				InjectType.Class,
				InjectType.Interface,
				InjectType.Struct,
				InjectType.Enum,
				InjectType.Constructor,
				InjectType.Method,
				InjectType.Property,
				InjectType.Field,
				InjectType.Event
			};

			_mappings.Add(tde, tdetypes);
			OwnerType.Items.Add(tde);

			InitializeOwnerType(ade, tde);
		}

		private void InitializeOwnerType(AssemblyDefinitionEditor ade, TypeDefinitionEditor tde)
		{
			var handler = PluginFactory.GetInstance().Package.ActiveHandler;
			if (handler == null)
				return;

			var current = handler.TargetObject;
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
				var mref = current as MemberReference;
				if (!(mref.DeclaringType is TypeDefinition))
					return;

				OwnerType.SelectedItem = tde;
				tde.SelectedOperand = mref.DeclaringType as TypeDefinition;
			}
		}

		private void OwnerType_SelectedIndexChanged(object sender, EventArgs e)
		{
			OwnerPanel.Controls.Clear();
			OwnerPanel.Controls.Add((Control) OwnerType.SelectedItem);
			if (_mappings.ContainsKey(OwnerType.SelectedItem))
			{
				ItemType.DataSource = _mappings[OwnerType.SelectedItem];
				ItemType.SelectedIndex = 0;
			}
			InjectContextChanged(sender, e);
		}

		private void InjectContextChanged(object sender, EventArgs e)
		{
			var editor = (IOperandEditor) OwnerType.SelectedItem;
			if (ItemType.SelectedIndex < 0)
				return;

			var targettype = (InjectType) ItemType.SelectedItem;

			ExtraTypePanel.Visible = _extraTypeSupported.Contains(targettype);
			LabExtraType.Visible = _extraTypeSupported.Contains(targettype);
			LabExtraType.Text = targettype.ToString().Replace("Interface", "Base").Replace("Class", "Base") + @" type";
			ItemName.Enabled = targettype != InjectType.Constructor;

			var nameprefix = (editor is AssemblyDefinitionEditor) ? "Namespace.Injected" : "InjectedInner";
			ItemName.Text = @"Injected" + targettype;
			Type extratype = null;

			switch (targettype)
			{
				case InjectType.Class:
				case InjectType.Interface:
					ItemName.Text = string.Concat(nameprefix, targettype.ToString());
					extratype = typeof (object);
					break;
				case InjectType.Enum:
				case InjectType.Struct:
					ItemName.Text = string.Concat(nameprefix, targettype.ToString());
					break;
				case InjectType.Property:
				case InjectType.Field:
					extratype = typeof (int);
					break;
				case InjectType.Event:
					extratype = typeof (EventHandler);
					break;
				case InjectType.AssemblyReference:
					ItemName.Text = @"System.Windows.Forms";
					break;
				case InjectType.Constructor:
					ItemName.Text = @".ctor";
					break;
				case InjectType.Resource:
					extratype = typeof (ResourceType);
					break;
			}

			InitializeExtraType(editor, extratype);
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
				}
				else
				{
					ExtraType.Visible = true;
					ExtraTypeList.Visible = false;
					if (editor is AssemblyDefinitionEditor)
					{
						var aeditor = (editor as AssemblyDefinitionEditor);
						if (aeditor.SelectedOperand != null)
						{
							ExtraType.SelectedOperand = LookupTypeReference(aeditor.SelectedOperand.MainModule, extratype);
						}
					}
					else
					{
						var teditor = (editor as TypeDefinitionEditor);
						if (teditor != null && teditor.SelectedOperand != null)
						{
							ExtraType.SelectedOperand = LookupTypeReference(teditor.SelectedOperand.Module, extratype);
						}
					}
				}
			}
		}

		private TypeReference LookupTypeReference(ModuleDefinition module, Type type)
		{
			// Do not use import, we do not want to reference additional assemblies
			if (type == typeof (object))
				return module.TypeSystem.Object;

			if (type == typeof(int))
				return module.TypeSystem.Int32;

			if (type == typeof(EventHandler))
				return module.TypeSystem.EventHandler;

			return null;
		}

		private void Ok_Click(object sender, EventArgs e)
		{
			var editor = (IOperandEditor) OwnerType.SelectedItem;
			if (editor == null)
				return;

			var owner = editor.SelectedOperand;
			if (owner == null || ItemType.SelectedIndex < 0)
				return;

			InjectHelper.Inject(owner, TargetType, ItemName.Text,
				ExtraTypeList.Visible ? ExtraTypeList.SelectedItem : ExtraType.SelectedOperand);
		}

		public DialogResult ShowDialog(InjectType type)
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