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
using Mono.Cecil;
using Reflexil.Plugins;
using Mono.Cecil.Rocks;
using Reflexil.Properties;

#endregion

namespace Reflexil.Handlers
{
	public partial class MethodDefinitionHandler : IHandler
	{
		#region Fields

		private MethodDefinition _mdef;
		private bool _readonly;

		#endregion

		#region Properties

		public bool ReadOnly
		{
			get { return _readonly; }
			set
			{
				Instructions.ReadOnly = value;
				ExceptionHandlers.ReadOnly = value;
				Variables.ReadOnly = value;
				Parameters.ReadOnly = value;
				Overrides.ReadOnly = value;
				Attributes.ReadOnly = value;
				_readonly = value;
			}
		}

		object IHandler.TargetObject
		{
			get { return _mdef; }
		}

		public string Label
		{
			get { return "Method definition"; }
		}

		public MethodDefinition MethodDefinition
		{
			get { return _mdef; }
		}

		#endregion

		#region Events

		private void Instructions_GridUpdated(object sender, EventArgs e)
		{
			if (_mdef.Body != null)
			{
				if (Settings.Default.OptimizeAndFixIL)
				{
					// this will also call ComputeOffsets
					_mdef.Body.SimplifyMacros();
					_mdef.Body.OptimizeMacros();
				}
				else
				{
					_mdef.Body.ComputeOffsets();
				}
			}
			Instructions.Rehash();
			ExceptionHandlers.Rehash();
		}

		private void ExceptionHandlers_GridUpdated(object sender, EventArgs e)
		{
			ExceptionHandlers.Rehash();
		}

		private void Variables_GridUpdated(object sender, EventArgs e)
		{
			Variables.Rehash();
			Instructions.Rehash();
		}

		private void Parameters_GridUpdated(object sender, EventArgs e)
		{
			Parameters.Rehash();
			Instructions.Rehash();
		}

		private void Overrides_GridUpdated(object sender, EventArgs e)
		{
			Overrides.Rehash();
		}

		private void CustomAttributes_GridUpdated(object sender, EventArgs e)
		{
			CustomAttributes.Rehash();
		}

		public void OnConfigurationChanged(object sender, EventArgs e)
		{
			Instructions.Rehash();
			ExceptionHandlers.Rehash();
			Variables.Rehash();
			Parameters.Rehash();
			CustomAttributes.Rehash();
		}

		private void Instructions_BodyReplaced(object sender, EventArgs e)
		{
			HandleItem(MethodDefinition);
		}

		#endregion

		#region Methods

		public MethodDefinitionHandler()
		{
			InitializeComponent();
			_readonly = false;
		}

		public bool IsItemHandled(object item)
		{
			return PluginFactory.GetInstance().IsMethodDefinitionHandled(item);
		}

		public void HandleItem(MethodDefinition mdef)
		{
			_mdef = mdef;
			Instructions.Bind(mdef);
			Variables.Bind(mdef);
			ExceptionHandlers.Bind(mdef);
			Parameters.Bind(mdef);
			Overrides.Bind(mdef);
			Attributes.Bind(mdef);
			CustomAttributes.Bind(mdef);
		}

		public void HandleItem(object item)
		{
			HandleItem(PluginFactory.GetInstance().GetMethodDefinition(item));
		}

		#endregion
	}
}