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

using System;
using Reflexil.Forms;
using Reflexil.Properties;
using Mono.Cecil;
using System.Windows.Forms;
using Reflexil.Handlers;
using Reflexil.Utils;
using System.Collections.Generic;
using Reflexil.Wrappers;
using System.Linq;
using System.IO;

namespace Reflexil.Plugins
{
	public abstract class BasePackage : IPackage
	{
		protected readonly string ReflexilWindowText = string.Format("Sebastien Lebreton's Reflexil v{0}",
			typeof (BasePackage).Assembly.GetName().Version.ToString(2));

		protected readonly string ReflexilButtonText = string.Format("Reflexil v{0}",
			typeof (BasePackage).Assembly.GetName().Version.ToString(2));

		protected const string ReflexilWindowId = "Reflexil.Window";


		public abstract IEnumerable<IAssemblyWrapper> HostAssemblies { get; }
		public abstract object ActiveItem { get; }

		public ReflexilWindow ReflexilWindow { get; protected set; }
		public IHandler ActiveHandler { get; private set; }
		protected bool UpdatingHostObjectModel { get; private set; }

		protected abstract void MainButtonClick(object sender, EventArgs e);

		protected virtual void ActiveItemChanged(object sender, EventArgs e)
		{
			// Try to validate in order to not loose any alteration
			ReflexilWindow.ValidateChildren(ValidationConstraints.Enabled);
			ActiveHandler = ReflexilWindow.HandleItem(ActiveItem);
		}

		protected virtual void AssemblyLoaded(object sender, EventArgs e)
		{
		}

		protected virtual void AssemblyUnloaded(object sender, EventArgs e)
		{
		}

		public virtual void ReloadAssembly(object sender, EventArgs e)
		{
			AssemblyHelper.ReloadAssembly(GetCurrentModuleLocation());
			var handler = PluginFactory.GetInstance().Package.ActiveHandler;

			if (handler != null && handler.IsItemHandled(ActiveItem))
				handler.HandleItem(ActiveItem);
		}

		public virtual void RenameItem(object sender, EventArgs e)
		{
			if (ActiveHandler == null || ActiveHandler.TargetObject == null)
				return;

			using (var frm = new RenameForm())
			{
				if (frm.ShowDialog(ActiveHandler.TargetObject) == DialogResult.OK)
					ItemRenamed(this, EventArgs.Empty);
			}
		}

		public virtual void DeleteItem(object sender, EventArgs e)
		{
			if (ActiveHandler == null || ActiveHandler.TargetObject == null)
				return;

			DeleteHelper.Delete(ActiveHandler.TargetObject);
			ItemDeleted(this, EventArgs.Empty);
		}

		public virtual void SaveAssembly(object sender, EventArgs e)
		{
			AssemblyHelper.SaveAssembly(GetCurrentAssemblyDefinition());
		}

		public virtual void SearchObfuscator(object sender, EventArgs e)
		{
			AssemblyHelper.SearchObfuscator(GetCurrentModuleLocation());
		}

		public virtual void VerifyAssembly(object sender, EventArgs e)
		{
			AssemblyHelper.VerifyAssembly(GetCurrentAssemblyDefinition());
		}

		protected virtual void ItemInjected(object sender, EventArgs e)
		{
			DisplayWarning();
			ActiveItemChanged(this, EventArgs.Empty);
		}

		protected virtual void ItemDeleted(object sender, EventArgs e)
		{
			DisplayWarning();
			ActiveItemChanged(this, EventArgs.Empty);
		}

		protected virtual void ItemRenamed(object sender, EventArgs e)
		{
			DisplayWarning();
			ActiveItemChanged(this, EventArgs.Empty);
		}

		protected virtual void DisplayWarning()
		{
			if (!Settings.Default.DisplayWarning)
				return;

			using (var frm = new SyncWarningForm())
				frm.ShowDialog();
		}

		private AssemblyDefinition GetCurrentAssemblyDefinition()
		{
			if (ActiveHandler == null)
				return null;

			var adef = ActiveHandler.TargetObject as AssemblyDefinition;
			if (adef != null)
				return adef;

			var mdef = ActiveHandler.TargetObject as ModuleDefinition;
			return mdef != null ? mdef.Assembly : null;
		}

		private string GetCurrentModuleLocation()
		{
			var adef = GetCurrentAssemblyDefinition();
			return adef == null ? null : adef.MainModule.Image.FileName;
		}

		protected virtual string GenerateId(string id)
		{
			return string.Concat("Reflexil.", id);
		}

		public virtual void Inject(InjectType type)
		{
			try
			{
				using (var frm = new InjectForm())
				{
					if (frm.ShowDialog(type) == DialogResult.OK)
						ItemInjected(this, EventArgs.Empty);
				}
			}
			catch (AssemblyResolutionException arException)
			{
				ShowMessage(string.Format("Unable to resolve assembly {0}, please load it prior to injection.", arException.AssemblyReference.Name));
			}
		}

		public abstract void ShowMessage(string message);

		public void UpdateHostObjectModel(object sender, EventArgs empty)
		{
			var plugin = PluginFactory.GetInstance();
			if (plugin == null)
				return;

			var context = plugin.GetAssemblyContext(ActiveItem);
			if (context == null)
				return;

			var adef = context.AssemblyDefinition;
			var wrapper = HostAssemblies.FirstOrDefault(a => a.Location == adef.MainModule.Image.FileName);
			if (wrapper == null)
				return;

			try
			{
				// Ok we have everything, write the assembly to stream
				var stream = new MemoryStream();
				adef.MainModule.Write(stream);
				stream.Position = 0;

				// Then hot-replace the assembly
				UpdatingHostObjectModel = true;
				HotReplaceAssembly(wrapper, stream);
			}
			catch (Exception)
			{
				ShowMessage(string.Format("Unable to stream assembly to {0}. In case of item deletion make sure you removed all existing references.", plugin.HostApplication));
			}
			finally
			{
				UpdatingHostObjectModel = false;
			}
		}

		protected virtual void HotReplaceAssembly(IAssemblyWrapper wrapper, MemoryStream stream)
		{
		}
	}
}