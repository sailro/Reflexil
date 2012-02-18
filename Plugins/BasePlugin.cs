/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;
using Reflexil.Properties;
using System.Text;
using Reflexil.Utils;

#endregion

namespace Reflexil.Plugins
{
    /// <summary>
    /// Base plugin implementation
    /// </summary>
    public abstract class BasePlugin : IPlugin, IComparer<OpCode>
    {

        #region " Fields "
        private List<OpCode> m_allopcodes;
        private Dictionary<string, string> m_opcodesdesc = new Dictionary<string, string>();
        private Bitmap m_browserimages = new Bitmap(16, 16);
        private Bitmap m_barimages = new Bitmap(16, 16);
        private IPackage m_package;
        protected Dictionary<string, IAssemblyContext> m_assemblycache;
        protected ICollection m_assemblies;
        #endregion

        #region " Properties "
        public abstract string HostApplication {
            get;
        }

        public static Bitmap ReflexilImage
        {
            get { return Resources.reflexil; }
        }

        public static bool ShowSymbols
        {
            get { return Settings.Default.ShowSymbols; }
        }

        public static bool AutoDetectObfuscators
        {
            get { return Settings.Default.AutoDetectObfuscators; }
        }

        public IPackage Package
        {
            get
            {
                return m_package;
            }
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public BasePlugin(IPackage package)
        {
            m_package = package;
            m_allopcodes = new List<OpCode>();
            foreach (FieldInfo finfo in typeof(OpCodes).GetFields())
            {
                m_allopcodes.Add((OpCode)(finfo.GetValue(null)));
            }
            m_allopcodes.Sort(this);
            m_opcodesdesc = new Dictionary<string, string>();
            m_assemblycache = new Dictionary<string, IAssemblyContext>();
            m_assemblies = new ArrayList();

            m_browserimages = Resources.browser;
            m_barimages = Resources.bar;
            ReloadOpcodesDesc(new MemoryStream(Encoding.ASCII.GetBytes(Resources.opcodes)));
        }

        /// <summary>
        /// Get an opcode description
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <returns>The opcode description or an empty string if not found</returns>
        public virtual string GetOpcodeDesc(OpCode opcode)
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
        /// Return all opcodes
        /// </summary>
        /// <returns>Opcodes</returns>
        public virtual List<OpCode> GetAllOpCodes()
        {
            return m_allopcodes;
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
        /// Reload all opcode descriptions from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        private void ReloadOpcodesDesc(Stream stream)
        {
            const string opcode = "opcode";
            const string desc = "desc";

            StreamReader reader = new StreamReader(stream);
            Regex rex = new Regex(String.Format("^(?<{0}>.*)=(?<{1}>.*)$", opcode, desc));

            m_opcodesdesc.Clear();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] items = rex.Split(line);
                m_opcodesdesc.Add(items[rex.GroupNumberFromName(opcode)], items[rex.GroupNumberFromName(desc)]);
            }
        }

        /// <summary>
        /// Return all images as a single bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetAllBrowserImages()
        {
            return m_browserimages;
        }

        /// <summary>
        /// Return all bar images as a single bitmap
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetAllBarImages()
        {
            return m_barimages;
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve a Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsLinkedResourceHandled(object item);

        /// <summary>
        /// Return all assemblies loaded into the host program
        /// </summary>
        /// <param name="wrap">true when wrapping native objects into IAssemblyWrapper</param>
        /// <returns>Assemblies</returns>
        public abstract ICollection GetAssemblies(bool wrap);

        /// <summary>
        /// Check if an assembly context is loaded
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>True is already loaded</returns>
        public bool IsAssemblyContextLoaded(string location)
        {
            location = System.Environment.ExpandEnvironmentVariables(location);
            return m_assemblycache.ContainsKey(location);
        }

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsAssemblyNameReferenceHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsAssemblyDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsTypeDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsPropertyDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsFieldDefinitionHandled(object item);
        
        /// <summary>
        /// Determine if the plugin is able to retrieve a Module Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsModuleDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsMethodDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsEventDefinitionHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Embedded Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsEmbeddedResourceHandled(object item);

        /// <summary>
        /// Determine if the plugin is able to retrieve an Assembly Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>true if handled</returns>
        public abstract bool IsAssemblyLinkedResourceHandled(object item);

        /// <summary>
        /// Retrieve a Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Linked Resource</returns>
        public abstract LinkedResource GetLinkedResource(object item);

        /// <summary>
        /// Retrieve a Method Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Method Definition</returns>
        public abstract MethodDefinition GetMethodDefinition(object item);

        /// <summary>
        /// Retrieve a Property Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Property Definition</returns>
        public abstract PropertyDefinition GetPropertyDefinition(object item);

        /// <summary>
        /// Retrieve a Field Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Field Definition</returns>
        public abstract FieldDefinition GetFieldDefinition(object item);

        /// <summary>
        /// Retrieve an Event Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Event Definition</returns>
        public abstract EventDefinition GetEventDefinition(object item);

        /// <summary>
        /// Retrieve an Embedded Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Embedded Resource</returns>
        public abstract EmbeddedResource GetEmbeddedResource(object item);

        /// <summary>
        /// Retrieve an Assembly Linked Resource from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching A.L. Resource</returns>
        public abstract AssemblyLinkedResource GetAssemblyLinkedResource(object item);

        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        public abstract IAssemblyContext GetAssemblyContext(string location);

        /// <summary>
        /// Load assembly from disk
        /// </summary>
        /// <param name="location">assembly location</param>
        /// <returns></returns>
        public abstract AssemblyDefinition LoadAssembly(string location, bool readsymbols);
      
        /// <summary>
        /// Get an assembly context in cache or create a new one if necessary
        /// </summary>
        /// <typeparam name="T">Context type</typeparam>
        /// <param name="location">Assembly location</param>
        /// <returns>Null if unable to load the assembly</returns>
        public IAssemblyContext GetAssemblyContext<T>(string location) where T : IAssemblyContext, new()
        {
            location = System.Environment.ExpandEnvironmentVariables(location);
            if (!m_assemblycache.ContainsKey(location))
            {
                try
                {
                    // Check for obfuscators
                    if (AutoDetectObfuscators)
                    {
                        AssemblyHelper.SearchObfuscator(location, true);
                    }
                    AssemblyDefinition asmdef = LoadAssembly(location, BasePlugin.ShowSymbols);
                    IAssemblyContext context = new T();
                    context.AssemblyDefinition = asmdef;
                    m_assemblycache.Add(location, context);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return m_assemblycache[location];
        }

        /// <summary>
        /// Retrieve an Assembly Name Reference from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Name Reference</returns>
        public abstract AssemblyNameReference GetAssemblyNameReference(object item);

        /// <summary>
        /// Retrieve an Assembly Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Assembly Definition</returns>
        public abstract AssemblyDefinition GetAssemblyDefinition(object item);

        /// <summary>
        /// Retrieve a Type Definition from the object
        /// </summary>
        /// <param name="item">the object</param>
        /// <returns>The matching Type Definition</returns>
        public abstract TypeDefinition GetTypeDefinition(object item);

        /// <summary>
        /// Retrieve the location of the module object
        /// </summary>
        /// <param name="item">the module object</param>
        /// <returns>the location</returns>
        public abstract string GetModuleLocation(object item);

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
        /// Reload an assembly context
        /// </summary>
        /// <param name="location">location (key to retrieve the cached assembly context)</param>
        /// <returns>Returns the reloaded assembly context</returns>
        public IAssemblyContext ReloadAssemblyContext(string location)
        {
            RemoveAssemblyContext(location);
            return GetAssemblyContext(location);
        }

        /// <summary>
        /// Synchronize assembly contexts with host' loaded assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        public abstract void SynchronizeAssemblyContexts(ICollection assemblies);

        /// <summary>
        /// Reload assemblies
        /// </summary>
        /// <param name="assemblies">Assemblies</param>
        public void ReloadAssemblies(ICollection assemblies)
        {
            m_assemblies = assemblies;
        }
        #endregion

    }
}
