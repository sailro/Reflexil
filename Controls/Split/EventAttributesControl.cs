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

using Mono.Cecil;
using System.ComponentModel;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Property attributes editor (all object readable/writeble non indexed properties)
	/// </summary>
	public partial class EventAttributesControl : BaseEventAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public EventAttributesControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Bind an event definition to this control
		/// </summary>
		/// <param name="edef">Event definition to bind</param>
		public override void Bind(EventDefinition edef)
		{
			base.Bind(edef);
			EventType.SelectedTypeReference = edef != null ? edef.EventType : null;
		}

		#endregion

		#region Events

		/// <summary>
		/// Handle text box validation
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		private void EventType_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			if (EventType.SelectedTypeReference is Mono.Cecil.TypeSpecification)
			{
				var tspec = EventType.SelectedTypeReference as Mono.Cecil.TypeSpecification;
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
					Item.EventType = Item.Module.Import(EventType.SelectedTypeReference);
				}
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseEventAttributesControl : SplitAttributesControl<EventDefinition>
	{
	}

	#endregion
}