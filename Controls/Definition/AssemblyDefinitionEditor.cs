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
using Reflexil.Plugins;
using Reflexil.Wrappers;

#endregion

namespace Reflexil.Editors
{
	public sealed partial class AssemblyDefinitionEditor : UserControl, IOperandEditor<AssemblyDefinition>
	{
		#region Properties

		public AssemblyDefinition SelectedOperand
		{
			get
			{
				if (CbxAssemblies.SelectedItem is IAssemblyWrapper)
				{
					String location = (CbxAssemblies.SelectedItem as IAssemblyWrapper).Location;
					IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(location);
					return context.AssemblyDefinition;
				}
				return null;
			}
			set
			{
				CbxAssemblies.SelectedIndex = -1;
				if (value != null)
				{
					foreach (IAssemblyWrapper wrapper in CbxAssemblies.Items)
					{
						if (wrapper.Location.Equals(value.MainModule.Image.FileName, StringComparison.OrdinalIgnoreCase))
						{
							CbxAssemblies.SelectedItem = wrapper;
						}
					}
				}
			}
		}

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (AssemblyDefinition) value; }
		}

		public string Label
		{
			get { return "Assembly definition"; }
		}

		public string ShortLabel
		{
			get { return "Assembly"; }
		}

		#endregion

		#region Methods

		public AssemblyDefinitionEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			foreach (var wrapper in PluginFactory.GetInstance().Package.HostAssemblies)
				CbxAssemblies.Items.Add(wrapper);
		}

		public bool IsOperandHandled(object operand)
		{
			return operand is AssemblyDefinition;
		}

		public void Initialize(MethodDefinition mdef)
		{
		}

		public Mono.Cecil.Cil.Instruction CreateInstruction(Mono.Cecil.Cil.ILProcessor worker, Mono.Cecil.Cil.OpCode opcode)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}