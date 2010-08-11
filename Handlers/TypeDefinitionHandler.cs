﻿/*
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
using Reflexil.Utils;
using Reflexil.Forms;
using Reflexil.Plugins;
using System.Text;
#endregion

namespace Reflexil.Handlers
{
	public partial class TypeDefinitionHandler: UserControl, IHandler
    {

        #region " Fields "
        private TypeDefinition tdef;
        #endregion

        #region " Methods "
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
            get { return tdef; }
        }

        string IHandler.Label
        {
            get {
                return "Type definition";
            }
        }

        void IHandler.HandleItem(object item)
        {
            HandleItem(PluginFactory.GetInstance().GetTypeDefinition(item));
        }

        void HandleItem(TypeDefinition tdef)
        {
            this.tdef = tdef;
            Attributes.Bind(tdef);
            Interfaces.Bind(tdef);
        }

        void IHandler.OnConfigurationChanged(object sender, EventArgs e)
        {
            Interfaces.Rehash();
        }

        private void Interfaces_GridUpdated(object sender, EventArgs e)
        {
            Interfaces.Rehash();
        }
        #endregion
        
    }
}
