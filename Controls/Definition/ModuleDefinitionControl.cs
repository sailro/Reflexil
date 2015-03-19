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
using System.Windows.Forms;
using Mono.Cecil;

#endregion

namespace Reflexil.Editors
{
	public partial class ModuleDefinitionControl : UserControl
	{
		#region Fields

		private bool _readonly;

		#endregion

		#region Properties

		public bool ReadOnly
		{
			get { return _readonly; }
			set
			{
				_readonly = value;
				Enabled = !value;
			}
		}

		public ModuleDefinition Item { get; set; }

		#endregion

		#region Events

		private void TargetRuntime_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Runtime = (TargetRuntime) TargetRuntime.SelectedItem;
		}

		private void Kind_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Kind = (ModuleKind)Kind.SelectedItem;
		}

		private void Architecture_Validated(object sender, EventArgs e)
		{
			if (Item != null)
				Item.Architecture = (TargetArchitecture)Architecture.SelectedItem;
		}

		private void Characteristics_Validated(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Characteristics = 0;
			for (var i = 1; i < Characteristics.Items.Count; i++)
				if (Characteristics.CheckBoxItems[i].Checked)
					Item.Characteristics += (int) Characteristics.Items[i];
		}

		private void Attributes_Validated(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Attributes = 0;
			for (var i = 1; i < Attributes.Items.Count; i++)
				if (Attributes.CheckBoxItems[i].Checked)
					Item.Attributes += (int) Attributes.Items[i];
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public ModuleDefinitionControl()
		{
			InitializeComponent();
			Kind.DataSource = Enum.GetValues(typeof (ModuleKind));
			TargetRuntime.DataSource = Enum.GetValues(typeof (TargetRuntime));
			Architecture.DataSource = Enum.GetValues(typeof (TargetArchitecture));

			foreach (var mc in Enum.GetValues(typeof (ModuleCharacteristics)))
				Characteristics.Items.Add(mc);

			foreach (var mc in Enum.GetValues(typeof (ModuleAttributes)))
				Attributes.Items.Add(mc);
		}

		/// <summary>
		/// Bind an ModuleDefinition to this control
		/// </summary>
		/// <param name="item">ModuleDefinition to bind</param>
		public virtual void Bind(ModuleDefinition item)
		{
			Item = item;

			if (item != null)
			{
				Kind.SelectedItem = item.Kind;
				TargetRuntime.SelectedItem = item.Runtime;
				Architecture.SelectedItem = item.Architecture;

				for (var i = 1; i < Characteristics.Items.Count; i++)
					Characteristics.CheckBoxItems[i].Checked = ((int) item.Characteristics & (int) Characteristics.Items[i]) != 0;

				for (var i = 1; i < Attributes.Items.Count; i++)
					Attributes.CheckBoxItems[i].Checked = ((int) item.Attributes & (int) Attributes.Items[i]) != 0;
			}
			else
			{
				Kind.SelectedIndex = -1;
				TargetRuntime.SelectedIndex = -1;
				Architecture.SelectedIndex = -1;
				Characteristics.SelectedIndex = -1;
				Attributes.SelectedIndex = -1;
			}

			if (!ReadOnly)
			{
				Enabled = (item != null);
			}
		}

		#endregion
	}
}