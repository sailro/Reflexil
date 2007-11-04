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
    /// <summary>
    /// Reflexil data manager (singleton)
    /// </summary>
	internal partial class DataManager : IComparer<OpCode>
	{
		
		#region " Fields "
		private List<OpCode> m_allopcodes;
		private Dictionary<string, string> m_opcodesdesc = new Dictionary<string, string>();
        private Dictionary<string, AssemblyContext> m_assemblycache;
		private Bitmap m_images = new Bitmap(16, 16);
		private IAssemblyCollection m_reflectorassemblies;
        static DataManager m_instance = new DataManager();
        #endregion

        #region " Methods "
        /// <summary>
        /// Get the DataManager instance
        /// </summary>
        /// <returns>Singleton</returns>
        public static DataManager GetInstance()
		{
            return m_instance;
		}
		
        /// <summary>
        /// Get an opcode description
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <returns>The opcode description or an empty string if not found</returns>
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

        /// <summary>
        /// Reload all opcode descriptions from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
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
		
        /// <summary>
        /// Reload all images from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
		public void ReloadImages(Stream stream)
		{
			m_images = new Bitmap(stream);
		}
		
		/// <summary>
		/// Return all images as a single bitmap
		/// </summary>
		/// <returns>Bitmap</returns>
        public Bitmap GetAllImages()
		{
			return m_images;
		}
		
		/// <summary>
		/// Return all opcodes
		/// </summary>
		/// <returns>Opcodes</returns>
        public List<OpCode> GetAllOpCodes()
        {
            return m_allopcodes;
        }
		
		/// <summary>
		/// Return all assemblies loaded into Reflector
		/// </summary>
		/// <returns>Assemblies</returns>
        public IAssemblyCollection GetReflectorAssemblies()
		{
			return m_reflectorassemblies;
		}

        /// <summary>
        /// Compare two opcodes by name
        /// </summary>
        /// <param name="x">Opcode</param>
        /// <param name="y">Opcode</param>
        /// <returns>IComparer<OpCode>.CompareTo</returns>
        public int Compare(OpCode x, OpCode y)
		{
			return x.Name.CompareTo(y.Name);
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
        private DataManager()
		{
			m_allopcodes = new List<OpCode>();
			foreach (FieldInfo finfo in typeof(OpCodes).GetFields())
			{
				m_allopcodes.Add((OpCode) (finfo.GetValue(null)));
			}
			m_allopcodes.Sort(this);
            m_assemblycache = new Dictionary<string, AssemblyContext>();
			m_opcodesdesc = new Dictionary<string, string>();
		}

        /// <summary>
        /// Check if an assembly context is loaded
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>True is already loaded</returns>
        public Boolean IsAssemblyContextLoaded(string location)
        {
   			location = System.Environment.ExpandEnvironmentVariables(location);
            return m_assemblycache.ContainsKey(location);
        }

        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        public AssemblyContext GetAssemblyContext(string location)
		{
			location = System.Environment.ExpandEnvironmentVariables(location);
			if (! m_assemblycache.ContainsKey(location))
			{
                try
                {
                    AssemblyDefinition asmdef = AssemblyFactory.GetAssembly(location);
                    m_assemblycache.Add(location, new AssemblyContext(asmdef));
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
	
		/// <summary>
		/// Remove an assembly context from cache
		/// </summary>
		/// <param name="location">Assembly location</param>
        public void RemoveAssemblyContext(string location)
		{
			location = System.Environment.ExpandEnvironmentVariables(location);
			if (m_assemblycache.ContainsKey(location))
			{
				m_assemblycache.Remove(location);
			}
		}
		
		/// <summary>
		/// Synchronize assembly contexts with Reflector's loaded assemblies
		/// </summary>
		/// <param name="assemblies">Assemblies</param>
        public void SynchronizeAssemblyContexts(IAssemblyCollection assemblies)
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
		
		/// <summary>
		/// Reload reflector assemblies
		/// </summary>
		/// <param name="assemblies">Assemblies</param>
        public void ReloadReflectorAssemblyList(IAssemblyCollection assemblies)
		{
			m_reflectorassemblies = assemblies;
		}
		
		/// <summary>
		/// Reload an assembly context 
		/// </summary>
		/// <param name="location">Assembly location</param>
		/// <returns>A new assembly context</returns>
        public AssemblyContext ReloadAssemblyContext(string location)
		{
			RemoveAssemblyContext(location);
			return GetAssemblyContext(location);
		}
		#endregion
		
	}
}

