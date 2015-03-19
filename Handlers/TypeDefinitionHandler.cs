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
	public partial class TypeDefinitionHandler : UserControl, IHandler
	{
		#region Fields

		private TypeDefinition _tdef;

		#endregion

		#region Methods

		public TypeDefinitionHandler()
		{
			InitializeComponent();
		}

		bool IHandler.IsItemHandled(object item)
		{
			return PluginFactory.GetInstance().IsTypeDefinitionHandled(item);
		}

		object IHandler.TargetObject
		{
			get { return _tdef; }
		}

		string IHandler.Label
		{
			get { return "Type definition"; }
		}

		void IHandler.HandleItem(object item)
		{
			HandleItem(PluginFactory.GetInstance().GetTypeDefinition(item));
		}

		private void HandleItem(TypeDefinition tdef)
		{
			_tdef = tdef;
			Attributes.Bind(tdef);
			Interfaces.Bind(tdef);
			CustomAttributes.Bind(tdef);
		}

		#endregion

		#region Events

		void IHandler.OnConfigurationChanged(object sender, EventArgs e)
		{
			Interfaces.Rehash();
			CustomAttributes.Rehash();
		}

		private void Interfaces_GridUpdated(object sender, EventArgs e)
		{
			Interfaces.Rehash();
		}

		private void CustomAttributes_GridUpdated(object sender, EventArgs e)
		{
			CustomAttributes.Rehash();
		}

		#endregion
	}
}