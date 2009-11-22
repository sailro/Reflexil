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
using System;
using System.Collections;
using Reflector;
using Reflector.CodeModel;
#endregion

namespace Reflexil.Plugins.Reflector
{
	/// <summary>
	/// Addin entry point
	/// </summary>
    public partial class ReflectorPackage : BasePackage, global::Reflector.IPackage
	{
		
		#region " Constants "
        const string REFLECTOR_TOOLS_ID = "Tools";
        const string REFLECTOR_RESOURCE_OPCODES = "Reflector.Disassembler.txt";
		const string REFLECTOR_RESOURCE_IMAGES = "Reflector.Browser16.png";
		#endregion
		
		#region " Fields "
		private IWindowManager wm;
		private IAssemblyBrowser ab;
		private ICommandBarManager cbm;
		private IAssemblyManager am;
		private IServiceProvider sp;
		private ICommandBarButton mainMenuButton;
        private ICommandBarSeparator mainMenuSeparator;
		#endregion

        #region " Properties "
        public override ICollection Assemblies
        {
            get { return am.Assemblies; }
        }

        public override object ActiveItem
        {
            get { return ab.ActiveItem; }
        }
        #endregion

        #region " Events "
        /// <summary>
        /// 'Reflexil' button click 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
		protected override void Button_Click(object sender, EventArgs e)
		{
			wm.Windows[REFLEXIL_WINDOW_ID].Visible = true;
		}
		#endregion
		
		#region " Methods "
        /// <summary>
        /// Display a message
        /// </summary>
        /// <param name="message">message to display</param>
        public override void ShowMessage(string message)
        {
            wm.ShowMessage(message);
        }

        /// <summary>
        /// Helper method
        /// </summary>
        /// <typeparam name="T">Reflector service interface</typeparam>
        /// <returns>Reflector service implementation</returns>
		public T GetService<T>()
		{
			return ((T) (sp.GetService(typeof(T))));
		}
		
        /// <summary>
        /// Addin load method
        /// </summary>
        /// <param name="serviceProvider">Reflector service provider</param>
		public void Load(System.IServiceProvider serviceProvider)
		{
            PluginFactory.Register(new ReflectorPlugin(this));

			sp = serviceProvider;
			wm = GetService<IWindowManager>();
			ab = GetService<IAssemblyBrowser>();
			cbm = GetService<ICommandBarManager>();
			am = GetService<IAssemblyManager>();

            CheckFrameWorkVersion();

            // Main Window
			reflexilwindow = new Reflexil.Forms.ReflexilWindow();
            wm.Windows.Add(REFLEXIL_WINDOW_ID, reflexilwindow, REFLEXIL_WINDOW_TEXT);
			
            // Menu
			mainMenuSeparator = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddSeparator();
			mainMenuButton = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddButton(REFLEXIL_BUTTON_TEXT, null);
            mainMenuButton.Image = BasePlugin.ReflexilImage;

            // Main events
            ab.ActiveItemChanged += this.ActiveItemChanged;
            am.AssemblyLoaded += this.AssemblyLoaded;
            am.AssemblyUnloaded += this.AssemblyUnloaded;
            
            // Menu events
            mainMenuButton.Click += this.Button_Click;
			
			PluginFactory.GetInstance().ReloadAssemblies(am.Assemblies);
			reflexilwindow.HandleItem(ab.ActiveItem);
		}
		
        /// <summary>
        /// Addin unload method
        /// </summary>
		public void Unload()
		{
            // Main events
            ab.ActiveItemChanged -= this.ActiveItemChanged;
            am.AssemblyLoaded -= this.AssemblyLoaded;
            am.AssemblyUnloaded -= this.AssemblyUnloaded;

            // Menu events
            mainMenuButton.Click -= this.Button_Click;

            // Main Window
            wm.Windows.Remove(REFLEXIL_WINDOW_ID);

            // Menu
            cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(mainMenuButton);
			cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(mainMenuSeparator);
        }
		
		#endregion
    }
}


