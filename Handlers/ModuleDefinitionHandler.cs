
#region " Imports "
using System;
using System.IO;
using System.Windows.Forms;
using Reflector.CodeModel;
using Mono.Cecil;
using Reflexil.Utils;
#endregion

namespace Reflexil.Handlers
{
	
	public partial class ModuleDefinitionHandler : IHandler
	{
		
		#region " Fields "
		private AssemblyDefinition m_adef;
		private string m_originallocation;
		#endregion
		
		#region " Properties "
		public AssemblyDefinition AssemblyDefinition
		{
			get
			{
				return m_adef;
			}
		}
		
		public string OriginalLocation
		{
			get
			{
				return m_originallocation;
			}
		}
		
		public bool IsItemHandled(object item)
		{
			return (item) is IModule;
		}
		
		public string Label
		{
			get
			{
				return "Module definition";
			}
		}
		#endregion
		
		#region " Events "
		private void ButSaveAs_Click(Object sender, EventArgs e)
		{
            if (AssemblyDefinition != null)
            {
                SaveFileDialog.InitialDirectory = Path.GetDirectoryName(OriginalLocation);
                SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(OriginalLocation) + ".Patched" + Path.GetExtension(OriginalLocation);
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AssemblyFactory.SaveAssembly(AssemblyDefinition, SaveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
                    }
                }
            }
            else
            {
                MessageBox.Show("Assembly definition is not loaded (not a CLI image?)");
            }
		}
		
		private void ButReload_Click(Object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure to reload assembly, discarding all changes?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				m_adef = DataManager.GetInstance().ReloadAssemblyDefinition(OriginalLocation);
			}
		}
		#endregion
		
		#region " Methods "
        public ModuleDefinitionHandler() : base()
        {
            InitializeComponent();
        }

		public void HandleItem(object item)
		{
			m_originallocation = Environment.ExpandEnvironmentVariables(((IModule) item).Location);
			m_adef = Utils.DataManager.GetInstance().GetAssemblyDefinition(m_originallocation);
		}
		#endregion
		
	}
	
}


