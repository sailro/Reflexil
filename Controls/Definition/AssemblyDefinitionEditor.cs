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
using System;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Plugins;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Editors
{
	public partial class AssemblyDefinitionEditor: UserControl, IOperandEditor<AssemblyDefinition>
    {

        #region " Properties "
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
                        if (wrapper.Location.Equals(value.MainModule.Image.FileInformation.FullName, StringComparison.OrdinalIgnoreCase))
                        {
                            CbxAssemblies.SelectedItem = wrapper;
                        }
                    }
                }
            }
        }

        object IOperandEditor.SelectedOperand
        {
            get
            {
                return SelectedOperand;
            }
            set
            {
                SelectedOperand = (AssemblyDefinition)value;
            }
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

        #region " Methods "
        public AssemblyDefinitionEditor()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            foreach (IAssemblyWrapper wrapper in PluginFactory.GetInstance().GetAssemblies(true))
            {
                CbxAssemblies.Items.Add(wrapper);
            }
        }

        public bool IsOperandHandled(object operand)
        {
            return operand is AssemblyDefinition;
        }

        public void Initialize(MethodDefinition mdef)
        {
        }

        public Mono.Cecil.Cil.Instruction CreateInstruction(Mono.Cecil.Cil.CilWorker worker, Mono.Cecil.Cil.OpCode opcode)
        {
            throw new NotImplementedException();
        }
        #endregion
        
    }
}
