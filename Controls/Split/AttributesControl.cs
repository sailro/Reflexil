/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Reflexil.Wrappers;

namespace Reflexil.Editors
{
	public partial class AttributesControl : UserControl
	{
		private bool _refreshingFlags;

		public object Item { get; set; }

		public AttributesControl()
		{
			Item = null;
			InitializeComponent();
		}

		public void Bind(object item, Dictionary<string, string> prefixes)
		{
			Flags.Items.Clear();
			if (item != null)
			{
				foreach (
					var pinfo in
					item.GetType()
						.GetProperties(BindingFlags.Instance | BindingFlags.Public)
						.Where(pinfo => (pinfo.PropertyType == typeof(bool)) && pinfo.CanRead && pinfo.CanWrite))
				{
					Flags.Items.Add(new PropertyWrapper(pinfo, prefixes));
				}
			}
			Item = item;
			RefreshFlags();
		}

		public void Bind(object item)
		{
			Bind(item, new Dictionary<string, string>());
		}

		private void RefreshFlags()
		{
			if (Item == null)
			{
				Flags.ClearSelected();
			}
			else
			{
				_refreshingFlags = true;
				for (var i = 0; i < Flags.Items.Count; i++)
				{
					var wrapper = (PropertyWrapper) Flags.Items[i];
					Flags.SetItemChecked(i, (bool) wrapper.PropertyInfo.GetValue(Item, null));
				}
				_refreshingFlags = false;
			}
		}

		private void Flags_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_refreshingFlags || Item == null)
				return;

			var wrapper = (PropertyWrapper) Flags.Items[e.Index];
			wrapper.PropertyInfo.SetValue(Item, e.NewValue == CheckState.Checked, null);
			RefreshFlags();
		}
	}
}