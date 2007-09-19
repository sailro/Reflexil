
#region " Imports "
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.IO;
using System.Reflection;
using System.Drawing;
using Reflector;
using Reflector.CodeModel;
using Reflexil.Properties;
#endregion

namespace Reflexil.Utils
{
	
	internal partial class DataManager : IComparer<OpCode>
	{
		
		#region " Fields "
		private List<OpCode> m_allopcodes;
		private Dictionary<string, string> m_opcodesdesc = new Dictionary<string, string>();
		private Dictionary<string, AssemblyDefinition> m_assemblycache;
		private Bitmap m_images = new Bitmap(16, 16);
		private IAssemblyCollection m_reflectorassemblies;
		#endregion

        #region " Methods "
        static DataManager m_instance = new DataManager();

        public static DataManager GetInstance()
		{
            return m_instance;
		}
		
		public string GetOpcodeDesc(OpCode opcode)
		{
			if (m_opcodesdesc.ContainsKey(opcode.Name))
			{
				return m_opcodesdesc[opcode.Name];
			}
			else
			{
				return string.Empty;
			}
		}
		
		public void ReloadOpcodesDesc(Stream stream)
		{
            const string opcode = "opcode";
            const string desc = "desc";

			StreamReader reader = new StreamReader(stream);
            Regex rex = new Regex(String.Format("^(?<{0}>.*)=(?<{1}>.*)$", opcode, desc));
			
			m_opcodesdesc.Clear();
			while (! reader.EndOfStream)
			{
				string line = reader.ReadLine();
				string[] items = rex.Split(line);
                m_opcodesdesc.Add(items[rex.GroupNumberFromName(opcode)], items[rex.GroupNumberFromName(desc)]);
			}
		}
		
		public void ReloadImages(Stream stream)
		{
			m_images = new Bitmap(stream);
		}
		
		public Bitmap GetAllImages()
		{
			return m_images;
		}
		
		public List<OpCode> GetAllOpCodes()
		{
			return m_allopcodes;
		}
		
		public IAssemblyCollection GetReflectorAssemblies()
		{
			return m_reflectorassemblies;
		}

        public int Compare(OpCode x, OpCode y)
		{
			return x.Name.CompareTo(y.Name);
		}
		
		private DataManager()
		{
			m_allopcodes = new List<OpCode>();
			foreach (FieldInfo finfo in typeof(OpCodes).GetFields())
			{
				m_allopcodes.Add((OpCode) (finfo.GetValue(null)));
			}
			m_allopcodes.Sort(this);
			m_assemblycache = new Dictionary<string, AssemblyDefinition>();
			m_opcodesdesc = new Dictionary<string, string>();
		}

        public Boolean IsAssemblyDefintionLoaded(string location)
        {
   			location = System.Environment.ExpandEnvironmentVariables(location);
            return m_assemblycache.ContainsKey(location);
        }
		
		public AssemblyDefinition GetAssemblyDefinition(string location)
		{
			location = System.Environment.ExpandEnvironmentVariables(location);
			if (! m_assemblycache.ContainsKey(location))
			{
                try
                {
                    AssemblyDefinition asmdef = AssemblyFactory.GetAssembly(location);
                    m_assemblycache.Add(location, asmdef);
                    if ((asmdef.MainModule != null) && (Settings.Default.ShowSymbols))
                    {
                        try
                        {
                            asmdef.MainModule.LoadSymbols();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
			}
			return m_assemblycache[location];
		}
		
		public ICollection GetAllAssemblyDefinitions()
		{
			return m_assemblycache.Values;
		}
		
		public void RemoveAssemblyDefinition(string location)
		{
			location = System.Environment.ExpandEnvironmentVariables(location);
			if (m_assemblycache.ContainsKey(location))
			{
				m_assemblycache.Remove(location);
			}
		}
		
		public void SynchronizeAssemblyDefinitions(IAssemblyCollection assemblies)
		{
			List<string> locations = new List<string>();
			
			foreach (IAssembly asm in assemblies)
			{
				locations.Add(System.Environment.ExpandEnvironmentVariables(asm.Location));
			}
			
			foreach (string location in new ArrayList(m_assemblycache.Keys))
			{
				if (! locations.Contains(location))
				{
					m_assemblycache.Remove(location);
				}
			}
		}
		
		public void ReloadReflectorAssemblyList(IAssemblyCollection assemblies)
		{
			m_reflectorassemblies = assemblies;
		}
		
		public AssemblyDefinition ReloadAssemblyDefinition(string location)
		{
			RemoveAssemblyDefinition(location);
			return GetAssemblyDefinition(location);
		}
		#endregion
		
	}
	
}

