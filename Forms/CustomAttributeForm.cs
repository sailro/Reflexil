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

using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;
using System;
using Mono.Collections.Generic;
using Reflexil.Utils;

#endregion

namespace Reflexil.Forms
{
	public partial class CustomAttributeForm : Form
	{
		#region Properties

		public ICustomAttributeProvider SelectedProvider { get; private set; }

		public CustomAttribute SelectedAttribute { get; private set; }

		public CustomAttribute WorkingAttribute { get; set; }

		protected bool IsFormComplete
		{
			get
			{
				foreach (Control ctl in Controls)
				{
					ctl.Focus();
					if (!Validate()) return false;
				}

				TabControl.SelectedTab = TabAttributes;
				Constructor.Focus();
				return Validate();
			}
		}

		#endregion

		#region Methods

		public CustomAttributeForm()
		{
			InitializeComponent();
		}

		public virtual DialogResult ShowDialog(ICustomAttributeProvider provider, CustomAttribute attribute)
		{
			SelectedProvider = provider;
			SelectedAttribute = attribute;
			return ShowDialog();
		}

		protected CustomAttributeArgument FixCustomAttributeArgument(ModuleDefinition module, CustomAttributeArgument argument)
		{
			var value = argument.Value;

			if (value is TypeReference)
				value = module.Import(value as TypeReference);

			if (value is CustomAttributeArgument[])
			{
				var arguments = value as CustomAttributeArgument[];
				for (var i = 0; i < arguments.Length; i++)
					arguments[i] = FixCustomAttributeArgument(module, arguments[i]);
			}

			// Used for wrapped CustomAttributeArgument[]
			if (argument.Type.Module == null)
				argument.Type = module.TypeSystem.LookupType(argument.Type.Namespace, argument.Type.Name);

			return new CustomAttributeArgument(module.Import(argument.Type), value);
		}

		protected void FixCustomAttributeArguments(ModuleDefinition module, Collection<CustomAttributeArgument> arguments)
		{
			for (var i = 0; i < arguments.Count; i++)
				arguments[i] = FixCustomAttributeArgument(module, arguments[i]);
		}

		protected void FixCustomAttributeNamedArguments(ModuleDefinition module,
			Collection<CustomAttributeNamedArgument> narguments)
		{
			for (var i = 0; i < narguments.Count; i++)
				narguments[i] = new CustomAttributeNamedArgument(narguments[i].Name,
					FixCustomAttributeArgument(module, narguments[i].Argument));
		}

		protected void FixAndUpdateWorkingAttribute()
		{
			var module = CecilHelper.GetModuleFromCustomAttributeProvider(SelectedProvider);

			WorkingAttribute.Constructor = module.Import(Constructor.SelectedOperand);

			FixCustomAttributeArguments(module, WorkingAttribute.ConstructorArguments);
			FixCustomAttributeNamedArguments(module, WorkingAttribute.Fields);
			FixCustomAttributeNamedArguments(module, WorkingAttribute.Properties);
		}

		#endregion

		#region Events

		private void ConstructorArguments_GridUpdated(object sender, EventArgs e)
		{
			ConstructorArguments.Rehash();
		}

		private void Fields_GridUpdated(object sender, EventArgs e)
		{
			Fields.Rehash();
		}

		private void Properties_GridUpdated(object sender, EventArgs e)
		{
			Properties.Rehash();
		}

		private void Constructor_SelectedOperandChanged(object sender, EventArgs e)
		{
			AttributeType.SelectedOperand = Constructor.SelectedOperand.DeclaringType;
		}

		private void Constructor_Validating(object sender, CancelEventArgs e)
		{
			if (Constructor.SelectedOperand == null)
			{
				ErrorProvider.SetError(ConstructorPanel, "Constructor is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(ConstructorPanel, string.Empty);
			}
		}

		#endregion
	}
}