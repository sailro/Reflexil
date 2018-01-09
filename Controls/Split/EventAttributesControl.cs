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

using Mono.Cecil;
using System.ComponentModel;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public partial class EventAttributesControl : BaseEventAttributesControl
	{
		public EventAttributesControl()
		{
			InitializeComponent();
		}

		public override void Bind(EventDefinition edef)
		{
			base.Bind(edef);
			EventType.Context = edef != null ? edef.DeclaringType : null;
			EventType.SelectedTypeReference = edef != null ? edef.EventType : null;
		}

		private void EventType_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			var typeSpecification = EventType.SelectedTypeReference as Mono.Cecil.TypeSpecification;
			if (typeSpecification != null)
			{
				var tspec = typeSpecification;
				validated = tspec.ElementType != null;
			}
			else
			{
				validated = EventType.SelectedTypeReference != null;
			}

			if (!validated)
			{
				ErrorProvider.SetError(EventType, "Type is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(EventType, string.Empty);
				if (Item != null && Item.Module != null)
				{
					Item.EventType = CecilImporter.Import(Item.Module, EventType.SelectedTypeReference, EventType.Context);
				}
			}
		}
	}

	public class BaseEventAttributesControl : SplitAttributesControl<EventDefinition>
	{
	}
}