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

#endregion

namespace Reflexil.Handlers
{
	public partial class LinkedResourceHandler : IHandler
	{
		#region Fields

		private LinkedResource _lres;

		#endregion

		#region Properties

		public bool IsItemHandled(object item)
		{
			return PluginFactory.GetInstance().IsLinkedResourceHandled(item);
		}

		object IHandler.TargetObject
		{
			get { return _lres; }
		}

		public string Label
		{
			get { return "Linked resource"; }
		}

		#endregion

		#region Events

		public void OnConfigurationChanged(object sender, EventArgs e)
		{
		}

		#endregion

		#region Methods

		public LinkedResourceHandler()
		{
			InitializeComponent();
		}

		public void HandleItem(object item)
		{
			HandleItem(PluginFactory.GetInstance().GetLinkedResource(item));
		}

		private void HandleItem(LinkedResource lres)
		{
			_lres = lres;
			Attributes.Bind(lres);
		}

		#endregion
	}
}