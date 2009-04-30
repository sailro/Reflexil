/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using Reflector;
using Reflector.CodeModel;
using Reflector.CodeModel.Memory;
using Reflexil.Forms;
using Reflexil.Properties;
using Mono.Cecil;
using System.Windows.Forms;
using Reflexil.Handlers;
#endregion

namespace Reflexil.Utils
{
	/// <summary>
	/// Addin entry point
	/// </summary>
	public partial class ReflexilPackage : IPackage
	{
		
		#region " Constants "
		const string REFLECTOR_TOOLS_ID = "Tools";
        //const string REFLECTOR_TYPEDEC_ID = "Browser.TypeDeclaration";
		readonly string REFLEXIL_WINDOW_TEXT = string.Format("Sebastien LEBRETON's Reflexil v{0}", typeof(ReflexilPackage).Assembly.GetName().Version.ToString(2));
        readonly string REFLEXIL_BUTTON_TEXT = string.Format("Reflexil v{0}", typeof(ReflexilPackage).Assembly.GetName().Version.ToString(2));
        public const string REFLEXIL_WINDOW_ID = "Reflexil.Window";
        //const string REFLEXIL_TYPEDEC_MENU_ID = "Reflexil.TypeDeclaration.Menu";
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
        //private ICommandBarMenu typeMenu;
        //private ICommandBarSeparator typeMenuSeparator;
        private ICommandBarSeparator mainMenuSeparator;
		private ReflexilWindow window;
        private IHandler activehandler;
		#endregion
		
		#region " Events "
        // Next version
        // enum must match TypeDefinitionHandler methods names
        //private enum TypeAction { AddConstructor, AddField, AddMethod, AddProperty, AddEvent};
        //private void HandleTypeAction(TypeAction action)
        //{
        //    if (ab.ActiveItem is ITypeDeclaration && activehandler is TypeDefinitionHandler)
        //    {
        //        ITypeDeclaration activeitem = ab.ActiveItem as ITypeDeclaration;

        //        // Retrieve infos
        //        string tempfile = System.IO.Path.GetTempFileName();
        //        IModule module = CecilHelper.GetModule(activeitem);
        //        IAssembly refasm = module.Assembly;
        //        AssemblyDefinition asmdef = DataManager.GetInstance().GetAssemblyContext(refasm.Location).AssemblyDefinition;

        //        // Update assembly
        //        typeof(TypeDefinitionHandler).GetMethod(action.ToString()).Invoke(activehandler, null);
                
        //        // Save assembly then unload
        //        AssemblyFactory.SaveAssembly(asmdef, tempfile);
        //        DataManager.GetInstance().RemoveAssemblyContext(refasm.Location);
        //        am.Unload(refasm);

        //        // Reload
        //        refasm = am.LoadFile(tempfile);
        //        asmdef = DataManager.GetInstance().GetAssemblyContext(refasm.Location).AssemblyDefinition;

        //        activeitem = CecilHelper.FindMatchingType(refasm, activeitem);
        //        if (activeitem != null)
        //        {
        //            ab.ActiveItem = activeitem;
        //        }
        //    }
        //}
        
        //private void AddConstructor(object sender, EventArgs e)
        //{
        //    HandleTypeAction(TypeAction.AddConstructor);
        //}

        //private void AddField(object sender, EventArgs e)
        //{
        //    HandleTypeAction(TypeAction.AddField);
        //}

        //private void AddMethod(object sender, EventArgs e)
        //{
        //    HandleTypeAction(TypeAction.AddMethod);
        //}

        //private void AddProperty(object sender, EventArgs e)
        //{
        //    HandleTypeAction(TypeAction.AddProperty);
        //}

        //private void AddEvent(object sender, EventArgs e)
        //{
        //    HandleTypeAction(TypeAction.AddEvent);
        //}

        /// <summary>
        /// 'Reflexil' button click 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
		private void button_Click(object sender, EventArgs e)
		{
			wm.Windows[REFLEXIL_WINDOW_ID].Visible = true;
		}
		
        /// <summary>
        /// Reflector browser active item changed 
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        private void ab_ActiveItemChanged(object sender, EventArgs e)
		{
            activehandler = window.HandleItem(ab.ActiveItem);
		}
		
        /// <summary>
        /// Assembly loaded into Reflector
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        private void am_AssemblyLoaded(object sender, EventArgs e)
		{
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
		}
		
        /// <summary>
        /// Assembly unloaded from Reflector
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        private void am_AssemblyUnloaded(object sender, EventArgs e)
		{
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
			DataManager.GetInstance().SynchronizeAssemblyContexts(am.Assemblies);
		}
		#endregion
		
		#region " Methods "
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
			sp = serviceProvider;
			wm = GetService<IWindowManager>();
			ab = GetService<IAssemblyBrowser>();
			cbm = GetService<ICommandBarManager>();
			am = GetService<IAssemblyManager>();
			sp = GetService<IServiceProvider>();

            // Check framework version
            if (!FrameworkVersionChecker.IsVersionInstalled(FrameworkVersions.v3_5))
            {
                wm.ShowMessage("Warning, Reflexil is unable to locate .NET Framework 3.5! This framework is required!");
            }

            // Main Window
			window = new Reflexil.Forms.ReflexilWindow();
			wm.Windows.Add(REFLEXIL_WINDOW_ID, window, REFLEXIL_WINDOW_TEXT);
			
            // Menu
			mainMenuSeparator = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddSeparator();
			mainMenuButton = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddButton(REFLEXIL_BUTTON_TEXT, null);
			mainMenuButton.Image = Resources.reflexil;

            // Type declaration menu
            //typeMenuSeparator = cbm.CommandBars[REFLECTOR_TYPEDEC_ID].Items.AddSeparator();
            //typeMenu = cbm.CommandBars[REFLECTOR_TYPEDEC_ID].Items.AddMenu(REFLEXIL_TYPEDEC_MENU_ID, REFLEXIL_BUTTON_TEXT);
            //typeMenu.Image = Resources.reflexil;
            //typeMenu.Items.AddButton("Add constructor", AddConstructor);
            //typeMenu.Items.AddButton("Add event", AddEvent);
            //typeMenu.Items.AddButton("Add field", AddField);
            //typeMenu.Items.AddButton("Add method", AddMethod);
            //typeMenu.Items.AddButton("Add property", AddProperty);

            // Constructor declaration menu

            // Main events
            ab.ActiveItemChanged += this.ab_ActiveItemChanged;
            am.AssemblyLoaded += this.am_AssemblyLoaded;
            am.AssemblyUnloaded += this.am_AssemblyUnloaded;
            
            // Menu events
            mainMenuButton.Click += this.button_Click;
			
			System.Reflection.Assembly reflectorAssembly = ((object) serviceProvider).GetType().Assembly;
            DataManager.GetInstance().ReloadOpcodesDesc(reflectorAssembly.GetManifestResourceStream(REFLECTOR_RESOURCE_OPCODES));
            DataManager.GetInstance().ReloadImages(reflectorAssembly.GetManifestResourceStream(REFLECTOR_RESOURCE_IMAGES));
			
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
			window.HandleItem(ab.ActiveItem);
		}
		
        /// <summary>
        /// Addin unload method
        /// </summary>
		public void Unload()
		{
            // Main events
            ab.ActiveItemChanged -= this.ab_ActiveItemChanged;
            am.AssemblyLoaded -= this.am_AssemblyLoaded;
            am.AssemblyUnloaded -= this.am_AssemblyUnloaded;

            // Menu events
            mainMenuButton.Click -= this.button_Click;

            // Main Window
            wm.Windows.Remove(REFLEXIL_WINDOW_ID);

            // Menu
            cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(mainMenuButton);
			cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(mainMenuSeparator);

            // Type declaration menu
            //cbm.CommandBars[REFLECTOR_TYPEDEC_ID].Items.Remove(typeMenu);
            //cbm.CommandBars[REFLECTOR_TYPEDEC_ID].Items.Remove(typeMenuSeparator);
        }
		
		#endregion
		
	}
}


