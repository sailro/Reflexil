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

#endregion

namespace Reflexil.Handlers
{
	public partial class MergedAssemblyModuleDefinitionHandler : UserControl, IHandler
	{
		#region Fields

		private AssemblyDefinition _adef;

		#endregion

		#region Methods

		public MergedAssemblyModuleDefinitionHandler()
		{
			InitializeComponent();
		}

		bool IHandler.IsItemHandled(object item)
		{
			return PluginFactory.GetInstance().IsAssemblyDefinitionHandled(item);
		}

		string IHandler.Label
		{
			get { return "Assembly && module definition"; }
		}

		object IHandler.TargetObject
		{
			get { return _adef; }
		}

		void IHandler.HandleItem(object item)
		{
			HandleItem(PluginFactory.GetInstance().GetAssemblyDefinition(item));
		}

		private void HandleItem(AssemblyDefinition adef)
		{
			_adef = adef;

			AssemblyNameDefinition.Bind(adef == null ? null : adef.Name);
			AssemblyDefinition.Bind(adef);
			AssemblyCustomAttributes.Bind(adef);

			var module = adef == null ? null : adef.MainModule;
			ModuleDefinition.Bind(module);
			ModuleCustomAttributes.Bind(module);
		}

		#endregion

		#region Events

		void IHandler.OnConfigurationChanged(object sender, EventArgs e)
		{
			AssemblyCustomAttributes.Rehash();
			ModuleCustomAttributes.Rehash();
		}

		private void AssemblyCustomAttributes_GridUpdated(object sender, EventArgs e)
		{
			AssemblyCustomAttributes.Rehash();
		}

		private void ModuleCustomAttributes_GridUpdated(object sender, EventArgs e)
		{
			AssemblyCustomAttributes.Rehash();
			ModuleCustomAttributes.Rehash();
		}

		#endregion
	}
}