
#region " Imports "
using System;
using Mono.Cecil;
using Reflector;
using Reflector.CodeModel;
using Reflexil.Forms;
using Reflexil.Properties;
#endregion

namespace Reflexil.Utils
{
	
	public partial class ReflexilPackage : IPackage
	{
		
		#region " Constants "
		const string REFLECTOR_TOOLS_ID = "Tools";
		readonly string REFLEXIL_WINDOW_TEXT = string.Format("Sebastien LEBRETON's Reflexil v{0}", typeof(ReflexilPackage).Assembly.GetName().Version.ToString(2));
        readonly string REFLEXIL_BUTTON_TEXT = string.Format("Reflexil v{0}", typeof(ReflexilPackage).Assembly.GetName().Version.ToString(2));
        const string REFLEXIL_WINDOW_ID = "Reflexil.Window";
		const string REFLECTOR_RESOURCE_OPCODES = "Reflector.Disassembler.txt";
		const string REFLECTOR_RESOURCE_IMAGES = "Reflector.Browser16.png";
		#endregion
		
		#region " Fields "
		private IWindowManager wm;
		private IAssemblyBrowser ab;
		private ICommandBarManager cbm;
		private IAssemblyManager am;
		private IServiceProvider sp;
		
		private ICommandBarButton button;
		private ICommandBarSeparator separator;
		private ReflexilWindow window;
		#endregion
		
		#region " Events "
		private void button_Click(object sender, EventArgs e)
		{
			wm.Windows[REFLEXIL_WINDOW_ID].Visible = true;
		}
		
		private void ab_ActiveItemChanged(object sender, EventArgs e)
		{
			window.HandleItem(ab.ActiveItem);
		}
		
		private void am_AssemblyLoaded(object sender, EventArgs e)
		{
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
		}
		
		private void am_AssemblyUnloaded(object sender, EventArgs e)
		{
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
			DataManager.GetInstance().SynchronizeAssemblyContexts(am.Assemblies);
		}
		#endregion
		
		#region " Methods "
		public T GetService<T>()
		{
			return ((T) (sp.GetService(typeof(T))));
		}
		
		public void Load(System.IServiceProvider serviceProvider)
		{
			sp = serviceProvider;
			wm = GetService<IWindowManager>();
			ab = GetService<IAssemblyBrowser>();
			cbm = GetService<ICommandBarManager>();
			am = GetService<IAssemblyManager>();
			sp = GetService<IServiceProvider>();

			window = new Reflexil.Forms.ReflexilWindow();
			wm.Windows.Add(REFLEXIL_WINDOW_ID, window, REFLEXIL_WINDOW_TEXT);
			
			separator = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddSeparator();
			button = cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.AddButton(REFLEXIL_BUTTON_TEXT, null);
			button.Image = Resources.reflexil;

            ab.ActiveItemChanged += this.ab_ActiveItemChanged;
            am.AssemblyLoaded += this.am_AssemblyLoaded;
            am.AssemblyUnloaded += this.am_AssemblyUnloaded;
            button.Click += this.button_Click;
			
			System.Reflection.Assembly reflectorAssembly = ((object) serviceProvider).GetType().Assembly;
            DataManager.GetInstance().ReloadOpcodesDesc(reflectorAssembly.GetManifestResourceStream(REFLECTOR_RESOURCE_OPCODES));
            DataManager.GetInstance().ReloadImages(reflectorAssembly.GetManifestResourceStream(REFLECTOR_RESOURCE_IMAGES));
			
			DataManager.GetInstance().ReloadReflectorAssemblyList(am.Assemblies);
			window.HandleItem(ab.ActiveItem);
		}
		
		public void Unload()
		{
            ab.ActiveItemChanged -= this.ab_ActiveItemChanged;
            am.AssemblyLoaded -= this.am_AssemblyLoaded;
            am.AssemblyUnloaded -= this.am_AssemblyUnloaded;
            button.Click -= this.button_Click;

			wm.Windows.Remove(REFLEXIL_WINDOW_ID);
			cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(button);
			cbm.CommandBars[REFLECTOR_TOOLS_ID].Items.Remove(separator);
		}
		
		#endregion
		
	}
	
}


