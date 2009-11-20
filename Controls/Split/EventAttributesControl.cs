/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Mono.Cecil;
using System.ComponentModel;
using System;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Property attributes editor (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class EventAttributesControl : BaseEventAttributesControl
    {
       
        #region " Methods "
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
        /// <param name="mdef">Event definition to bind</param>
        public override void Bind(EventDefinition edef)
        {
            base.Bind(edef);
            if (edef != null)
            {
                EventType.SelectedTypeReference = edef.EventType;
            }
            else
            {
                EventType.SelectedTypeReference = null;
            }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// Handle text box validation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void EventType_Validating(object sender, CancelEventArgs e)
        {
            bool validated;
            if (EventType.SelectedTypeReference is TypeSpecification)
            {
                TypeSpecification tspec = EventType.SelectedTypeReference as TypeSpecification;
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
                if (Item != null)
                {
                    Item.EventType = EventType.SelectedTypeReference;
                }
            }
        }
        #endregion

    }

    #region " VS Designer generic support "
    public class BaseEventAttributesControl : SplitAttributesControl<EventDefinition>
    {
    }
    #endregion
}
