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
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Utils;
using Reflexil.Wrappers;
using Reflexil.Plugins;
#endregion

namespace Reflexil.Forms
{
	partial class GenericMemberReferenceForm<T> : IComparer, IReflectionVisitor where T : MemberReference
	{
		
		#region " Constants "
		const string EXPANDER_NODE_KEY = "|-expander-|";
		#endregion
		
		#region " Fields "
        private AssemblyDefinition m_restrictadef;
		private T m_selected;
	    private Thread m_searchthread = null;
        private volatile bool m_requeststopthread = false;
		private Dictionary<object, TreeNode> m_nodes = new Dictionary<object, TreeNode>();
		private Dictionary<IReflectionVisitable, IReflectionVisitable> m_visiteditems = new Dictionary<IReflectionVisitable, IReflectionVisitable>();
		private Dictionary<Type, int> m_orders = new Dictionary<Type, int>();
		#endregion
		
		#region " Properties "
        public AssemblyDefinition AssemblyRestriction
        {
            get
            {
                return m_restrictadef;
            }
            set
            {
                m_restrictadef = value;
            }
        }

		public MemberReference SelectedItem
		{
			get
			{
				return m_selected;
			}
		}
		#endregion
		
		#region " Events "
		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			LoadNodeOnDemand(e.Node);
		}

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (typeof(T).IsAssignableFrom(e.Node.Tag.GetType()))
                {
                    m_selected = (T)e.Node.Tag;
                    ButOk.Enabled = true;
                }
                else
                {
                    ButOk.Enabled = false;
                }
            }
            else
            {
                ButOk.Enabled = false;
            }
        }

		private void MemberReferenceForm_Load(Object sender, EventArgs e)
		{
			TreeView.Focus();
		}

        private void Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var regex = new Regex(Search.Text, RegexOptions.IgnoreCase);
                SearchNodes(regex);
            } catch(Exception)
            {
                Search.ForeColor = Color.Red;
            }
        }

        private void GenericMemberReferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WaitForCompleteCancelation();
        }
		#endregion
		
		#region " Methods "
		public GenericMemberReferenceForm(T selected) : base()
		{
			InitializeComponent();

            string keyword = (typeof(T).Name.Contains("Reference")) ? "Reference" : "Definition";
            Text = Text + typeof(T).Name.Replace(keyword, string.Empty).ToLower();
            ImageList.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());
			
			foreach (IAssemblyWrapper asm in PluginFactory.GetInstance().GetAssemblies(true))
			{
				AppendRootNode(asm);
			}
			
			m_orders.Add(typeof(AssemblyDefinition), 0);
			m_orders.Add(typeof(TypeDefinition), 1);
			m_orders.Add(typeof(MethodDefinition), 2);
			m_orders.Add(typeof(PropertyDefinition), 3);
			m_orders.Add(typeof(EventDefinition), 3);
			m_orders.Add(typeof(FieldDefinition), 5);
			
			TreeView.TreeViewNodeSorter = this;
			
			ButOk.Enabled = selected != null && SelectItem(selected);
		}
		
		#region " Selection "
		public AssemblyDefinition GetAssemblyDefinitionByNodeName(string name)
		{
			foreach (TreeNode subNode in TreeView.Nodes)
			{
				if (subNode.Text == name)
				{
					if ((subNode.Tag) is IAssemblyWrapper)
					{
						LoadNodeOnDemand(subNode);
					}
					return ((AssemblyDefinition) subNode.Tag);
				}
			}
			return null;
		}
		
		public string StripGenerics(TypeReference item, string str)
		{
			if ((item) is GenericInstanceType)
			{
				foreach (TypeReference arg in ((GenericInstanceType) item).GenericArguments)
				{
					str = str.Replace(string.Format("<{0}>", arg.FullName), string.Empty);
				}
			}
			return str;
		}
		
		public TypeDefinition GetTypeDefinition(TypeReference item)
		{
			ModuleDefinition moddef = null;
			
			if ((item.Scope) is ModuleDefinition)
			{
				moddef = (ModuleDefinition) item.Scope;
				GetAssemblyDefinitionByNodeName(moddef.Assembly.Name.Name);
			}
			else if ((item.Scope) is AssemblyNameReference)
			{
				AssemblyNameReference anr = (AssemblyNameReference) item.Scope;
				AssemblyDefinition asmdef = GetAssemblyDefinitionByNodeName(anr.Name);
				if (asmdef != null)
				{
					moddef = asmdef.MainModule;
				}
			}
			
			if (moddef != null)
			{
				TypeDefinition typedef = CecilHelper.FindMatchingType(moddef.Types, StripGenerics(item, item.FullName));
				
				if (typedef != null)
				{
					if (typedef.DeclaringType != null)
					{
						GetTypeDefinition(typedef.DeclaringType);
					}
					LoadNodeOnDemand(m_nodes[moddef]);
					LoadNodeOnDemand(m_nodes[typedef]);
					return typedef;
				}
			}
			
			return null;
		}
		
		public MethodDefinition GetMethodDefinition(MethodReference item)
		{
			TypeDefinition typedef = GetTypeDefinition(item.DeclaringType);
			if (typedef != null)
			{
				foreach (MethodDefinition method in typedef.Methods)
				{
					if (StripGenerics(typedef, method.ToString()) == StripGenerics(item.DeclaringType, item.ToString()))
					{
						return method;
					}
				}
			}
			return null;
		}
		
		public FieldDefinition GetFieldDefinition(FieldReference item)
		{
			TypeDefinition typedef = GetTypeDefinition(item.DeclaringType);
			if (typedef != null)
			{
				foreach (FieldDefinition field in typedef.Fields)
				{
					if (StripGenerics(typedef, field.ToString()) == StripGenerics(item.DeclaringType, item.ToString()))
					{
						return field;
					}
				}
			}
			return null;
		}
		
		public bool SelectItem(MemberReference item)
		{
			object itemtag = null;
			
			if ((item) is TypeReference)
			{
				itemtag = GetTypeDefinition((TypeReference) item);
			}
			else if ((item) is MethodReference)
			{
				itemtag = GetMethodDefinition((MethodReference) item);
			}
			else if ((item) is FieldReference)
			{
				itemtag = GetFieldDefinition((FieldReference) item);
			}
			
			if (itemtag != null&& m_nodes.ContainsKey(itemtag))
			{
				TreeView.SelectedNode = m_nodes[itemtag];
				m_selected = (T) item;
				return true;
			}
			
			return false;
		}
		#endregion
		
		#region " Cosmetic "
		public int Compare(object x, object y)
		{
			TreeNode xn = (TreeNode) x;
			TreeNode yn = (TreeNode) y;
			
			int result = 0;
			if (xn.Tag != null&& yn.Tag != null)
			{
				if (m_orders.ContainsKey(xn.Tag.GetType()))
				{
					result = m_orders[xn.Tag.GetType()].CompareTo(m_orders[yn.Tag.GetType()]);
				}
			}
			if (result == 0)
			{
				result = xn.Text.CompareTo(yn.Text);
			}
			return result;
		}
		
		private int ScopeOffset(int attributes, int mask, int publicValue, int friendValue, int protectedFriendValue, int protectedValue, int privateValue)
		{
			int[] scopes = new int[] { publicValue, friendValue, protectedFriendValue, protectedValue, privateValue };
			attributes = attributes & mask;
			
			for (int i = 0; i <= scopes.Length - 1; i++)
			{
				if (attributes == scopes[i])
				{
					return i;
				}
			}
			
			return 0;
		}
		
		private EBrowserImages ImageIndex(object obj)
		{
			EBrowserImages offset = EBrowserImages.Empty;
			
			if ((obj) is TypeDefinition)
			{
				TypeDefinition typedef = (TypeDefinition) obj;
				if (typedef.IsEnum)
				{
					offset = EBrowserImages.PublicEnum;
				}
				else if (typedef.IsInterface)
				{
					offset = EBrowserImages.PublicInterface;
				}
				else if (typedef.IsValueType)
				{
					offset = EBrowserImages.PublicStructure;
				}
				else
				{
					offset = EBrowserImages.PublicClass;
				}
				if ((typedef.Attributes & TypeAttributes.VisibilityMask) < TypeAttributes.Public)
				{
                    offset = (EBrowserImages)((int)offset + EBrowserImages.FriendClass - EBrowserImages.PublicClass);
				}
				else
				{
					offset = offset + ScopeOffset(Convert.ToInt32(typedef.Attributes), (int)TypeAttributes.VisibilityMask, (int)TypeAttributes.NestedPublic, (int)TypeAttributes.NestedAssembly, (int)TypeAttributes.NestedFamORAssem, (int)TypeAttributes.NestedFamily, (int)TypeAttributes.NestedPrivate);
				}
			}
			else if ((obj) is PropertyDefinition)
			{
				PropertyDefinition propdef = (PropertyDefinition) obj;
				if (propdef.GetMethod == null)
				{
					if (propdef.SetMethod.IsStatic)
					{
						offset = EBrowserImages.PublicSharedWriteOnlyProperty;
					}
					else
					{
						offset = EBrowserImages.PublicWriteOnlyProperty;
					}
				}
				else if (propdef.SetMethod == null)
				{
					if (propdef.GetMethod.IsStatic)
					{
						offset = EBrowserImages.PublicSharedReadOnlyProperty;
					}
					else
					{
						offset = EBrowserImages.PublicReadOnlyProperty;
					}
				}
				else
				{
					if (propdef.GetMethod.IsStatic)
					{
						offset = EBrowserImages.PublicSharedProperty;
					}
					else
					{
						offset = EBrowserImages.PublicProperty;
					}
				}
			}
			else if ((obj) is MethodDefinition)
			{
				MethodDefinition metdef = (MethodDefinition) obj;
				if (metdef.IsConstructor)
				{
					if (metdef.IsStatic)
					{
						offset = EBrowserImages.PublicSharedConstructor;
					}
					else
					{
						offset = EBrowserImages.PublicConstructor;
					}
				}
				else
				{
					if (metdef.IsVirtual)
					{
						if (metdef.IsStatic)
						{
							offset = EBrowserImages.PublicSharedOverrideMethod;
						}
						else
						{
							offset = EBrowserImages.PublicOverrideMethod;
						}
					}
					else
					{
						if (metdef.IsStatic)
						{
							offset = EBrowserImages.PublicSharedMethod;
						}
						else
						{
							offset = EBrowserImages.PublicMethod;
						}
					}
				}
				offset = offset + ScopeOffset((int)metdef.Attributes, (int)MethodAttributes.MemberAccessMask, (int)MethodAttributes.Public, (int)MethodAttributes.Assembly, (int)MethodAttributes.FamORAssem, (int)MethodAttributes.Family, (int)MethodAttributes.Private);
			}
			else if ((obj) is FieldDefinition)
			{
				FieldDefinition field = (FieldDefinition) obj;
				if (field.IsLiteral && field.IsStatic)
				{
					offset = EBrowserImages.PublicEnumValue;
				}
				else
				{
					if (field.IsStatic)
					{
						offset = EBrowserImages.PublicSharedField;
					}
					else
					{
						offset = EBrowserImages.PublicField;
					}
				}
                offset = offset + ScopeOffset((int)field.Attributes, (int)FieldAttributes.FieldAccessMask, (int)FieldAttributes.Public, (int)FieldAttributes.Assembly, (int)FieldAttributes.FamORAssem, (int)FieldAttributes.Family, (int)FieldAttributes.Private);
			}
			else if ((obj) is ModuleDefinition)
			{
				offset = EBrowserImages.Module;
			}
			else if ((obj) is EventDefinition)
			{
				EventDefinition evtdef = (EventDefinition) obj;
				if (evtdef.AddMethod.IsStatic)
				{
					offset = EBrowserImages.PublicSharedEvent;
				}
				else
				{
					offset = EBrowserImages.PublicEvent;
				}
			}
			else if ((obj) is AssemblyDefinition || (obj) is IAssemblyWrapper)
			{
				offset = EBrowserImages.Assembly;
			}
			else if ((obj) is NamespaceWrapper)
			{
				offset = EBrowserImages.PublicNamespace;
			}
			
			return offset;
		}
		
		public string DisplayString(object obj)
		{
			if ((obj) is MethodDefinition)
			{
				MethodDefinition metdef = (MethodDefinition) obj;
				return metdef.ToString().Substring(metdef.ToString().IndexOf("::") + 2) + " : " + metdef.ReturnType.ToString();
			}
			else if ((obj) is PropertyDefinition)
			{
				PropertyDefinition propdef = (PropertyDefinition) obj;
				return propdef.ToString().Substring(propdef.ToString().IndexOf("::") + 2) + " : " + propdef.PropertyType.ToString();
			}
			else if ((obj) is FieldDefinition)
			{
				FieldDefinition flddef = (FieldDefinition) obj;
				return flddef.ToString().Substring(flddef.ToString().IndexOf("::") + 2) + " : " + flddef.FieldType.ToString();
			}
			else if ((obj) is ModuleDefinition)
			{
				ModuleDefinition moddef = (ModuleDefinition) obj;
				return moddef.Name;
			}
			else if ((obj) is TypeDefinition)
			{
				TypeDefinition typedef = (TypeDefinition) obj;
				return typedef.Name;
			}
			else if ((obj) is AssemblyDefinition)
			{
				AssemblyDefinition asmdef = (AssemblyDefinition) obj;
				return asmdef.Name.Name;
			}
			else if ((obj) is IAssemblyWrapper)
			{
                return obj.ToString();
			}
			else if ((obj) is EventDefinition)
			{
				EventDefinition evtdef = (EventDefinition) obj;
				return evtdef.Name;
			}
			else
			{
				return obj.ToString();
			}
		}
		#endregion
		
		#region " Node management "
        private void LoadNodeOnDemand(TreeNode node)
        {
            if (node.Nodes.ContainsKey(EXPANDER_NODE_KEY))
            {
                node.Nodes.RemoveAt(node.Nodes.IndexOfKey(EXPANDER_NODE_KEY));
            }
            if ((node.Tag) is IReflectionVisitable)
            {
                IReflectionVisitable visitable = (IReflectionVisitable)node.Tag;
                if (!m_visiteditems.ContainsKey(visitable))
                {
                    visitable.Accept(this);
                    m_visiteditems.Add(visitable, visitable);
                }
            }
            else if ((node.Tag) is IAssemblyWrapper)
            {
                IAssemblyWrapper iasm = (IAssemblyWrapper)node.Tag;

                IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(iasm.Location);
                if (context != null)
                {
                    AssemblyDefinition asmdef = context.AssemblyDefinition;

                    if ((AssemblyRestriction == null) || (asmdef == AssemblyRestriction))
                    {
                        m_nodes.Remove(node.Tag);
                        m_nodes.Add(asmdef, node);
                        node.Tag = asmdef;

                        foreach (ModuleDefinition moddef in asmdef.Modules)
                        {
                            AppendNode(asmdef, moddef, moddef.Types.Count > 0);
                        }
                    }
                    else
                    {
                        node.Tag = "restricted";
                        node.Text += String.Format(" (You can't use this assembly for selection -> restricted to {0})", AssemblyRestriction.Name.Name);
                    }
                }
            }
        }
		
		private void AppendRootNode(IAssemblyWrapper root)
		{
            if (root.IsValid)
            {
                TreeNode rootnode = new TreeNode(DisplayString(root));
                rootnode.ImageIndex = (int)ImageIndex(root);
                rootnode.SelectedImageIndex = rootnode.ImageIndex;
                rootnode.Tag = root;
                rootnode.Nodes.Add(EXPANDER_NODE_KEY, EXPANDER_NODE_KEY);
                TreeView.Nodes.Add(rootnode);
                m_nodes.Add(root, rootnode);
            }
		}
		
		private void AppendNode(TreeNode ownernode, object child, bool createExpander)
		{
			if (! m_nodes.ContainsKey(child))
			{
				TreeNode childnode = new TreeNode(DisplayString(child));
				childnode.ImageIndex = (int)ImageIndex(child);
				childnode.SelectedImageIndex = childnode.ImageIndex;
				childnode.Tag = child;
				if (createExpander)
				{
					childnode.Nodes.Add(EXPANDER_NODE_KEY, EXPANDER_NODE_KEY);
				}
				ownernode.Nodes.Add(childnode);
				m_nodes.Add(child, childnode);
			}
		}
		
		private void AppendNode(object owner, object child, bool createExpander)
		{
			TreeNode ownernode = m_nodes[owner];
			AppendNode(ownernode, child, createExpander);
		}

        private enum SearchResult
        {
            Found,
            NotFound,
            Canceled
        }

        private void OnSearchCanceled()
        {
            try
            {
                Invoke((Action)(() => TreeView.Visible = true));
            } 
            catch(Exception) { }
        }

        private void OnSearchFound(TreeNode node)
        {
            try
            {
                Invoke((Action)(() => Search.ForeColor = Color.Blue));
                Invoke((Action)(() => TreeView.SelectedNode = node));
                Invoke((Action)(() => TreeView.Visible = true));
            }
            catch (Exception) { }
        }

	    private void OnSearchNotFound()
        {
            try {
                Invoke((Action)(() => Search.ForeColor = Color.Red));
                Invoke((Action)(() => TreeView.Visible = true));
            } catch(Exception) { }
        }

        private SearchResult InternalSearchNodes(TreeNodeCollection nodes, Regex regex, int depth)
        {
            foreach (TreeNode node in nodes)
            {
                if (m_requeststopthread)
                {
                    if (depth == 0)
                        OnSearchCanceled();
                    return SearchResult.Canceled;
                }

                Invoke((Action<TreeNode>) (LoadNodeOnDemand), node);

                if (node.Tag is MemberReference && regex.IsMatch(node.Text))
                {
                    OnSearchFound(node);
                    return SearchResult.Found;
                }

                var result = InternalSearchNodes(node.Nodes, regex, depth + 1);
                if (result == SearchResult.Found)
                    return result;
            }
            if (depth == 0)
                OnSearchNotFound();
            return SearchResult.NotFound;
        }

        public void SafeInternalSearchNodes(Object state)
        {
            try
            {
                InternalSearchNodes(TreeView.Nodes, state as Regex, 0);
            } catch(Exception)
            {
            }
            
        }

	    private void SearchNodes(Regex regex)
        {
            WaitForCompleteCancelation();

            TreeView.Visible = false;
            Search.ForeColor = Color.Gray;

            m_searchthread = new Thread(SafeInternalSearchNodes);
            m_searchthread.Start(regex);
        }

	    private void WaitForCompleteCancelation()
	    {
            try
            {
                if (m_requeststopthread)
                    return;

                int timeout = 200;
                int retry = 5;
                bool done = false;

                if (m_searchthread != null)
                {
                    m_requeststopthread = true;

                    while (!done && retry > 0)
                    {
                        if (!(done = m_searchthread.Join(timeout)))
                            Application.DoEvents();
                        retry--;
                    }

                    if (!done)
                        m_searchthread.Abort();

                    m_requeststopthread = false;
                }
                m_searchthread = null;
            } catch(Exception)
            {
                m_requeststopthread = false;
            }
	    }

	    #endregion
		
		#region " Visitor implementation "
		public void VisitConstructorCollection(Mono.Collections.Generic.Collection<MethodDefinition> ctors)
		{
			foreach (MethodDefinition constructor in ctors)
			{
				AppendNode(constructor.DeclaringType, constructor, false);
			}
		}
		
		public void VisitEventDefinitionCollection(Mono.Collections.Generic.Collection<EventDefinition> events)
		{
			foreach (EventDefinition evt in events)
			{
				AppendNode(evt.DeclaringType, evt, true);
				if (evt.AddMethod != null)
				{
					AppendNode(evt, evt.AddMethod, false);
				}
				if (evt.RemoveMethod != null)
				{
					AppendNode(evt, evt.RemoveMethod, false);
				}
			}
		}
		
		public void VisitFieldDefinitionCollection(Mono.Collections.Generic.Collection<FieldDefinition> fields)
		{
			foreach (FieldDefinition field in fields)
			{
				AppendNode(field.DeclaringType, field, false);
			}
		}
		
		public void VisitMethodDefinitionCollection(Mono.Collections.Generic.Collection<MethodDefinition> methods)
		{
			foreach (MethodDefinition method in methods)
			{
				if (! method.IsSpecialName || method.IsConstructor)
				{
					AppendNode(method.DeclaringType, method, false);
				}
			}
		}
		
		public void VisitNestedTypeCollection(Mono.Collections.Generic.Collection<TypeDefinition> nestedTypes)
		{
			foreach (TypeDefinition nestedType in nestedTypes)
			{
				AppendNode(nestedType.DeclaringType, nestedType, true);
			}
		}
		
		public void VisitPropertyDefinitionCollection(Mono.Collections.Generic.Collection<PropertyDefinition> properties)
		{
			foreach (PropertyDefinition @property in properties)
			{
				AppendNode(@property.DeclaringType, @property, true);
				if (@property.GetMethod != null)
				{
					AppendNode(@property, @property.GetMethod, false);
				}
				if (@property.SetMethod != null)
				{
					AppendNode(@property, @property.SetMethod, false);
				}
			}
		}
		
		public void VisitTypeDefinitionCollection(Mono.Collections.Generic.Collection<TypeDefinition> types)
		{
			foreach (TypeDefinition typedef in types)
			{
                if ((typedef.Attributes & TypeAttributes.VisibilityMask) <= TypeAttributes.Public)
				{
                    NamespaceWrapper wrapper = new NamespaceWrapper(typedef.Module, typedef.Namespace);
                    AppendNode(typedef.Module, wrapper, true);
                    AppendNode(wrapper, typedef, true);
				}
			}
		}
		#endregion
		
		#region " Unimplemented vistor "
		public void VisitEventDefinition(EventDefinition evt)
		{
		}
		
		public void VisitFieldDefinition(FieldDefinition field)
		{
		}
		
		public void VisitModuleDefinition(ModuleDefinition @module)
		{
		}
		
		public void VisitNestedType(TypeDefinition nestedType)
		{
		}
		
		public void VisitPropertyDefinition(PropertyDefinition @property)
		{
		}
		
		public void VisitTypeDefinition(TypeDefinition type)
		{
		}
		
		public void VisitConstructor(MethodDefinition ctor)
		{
		}
		
		public void VisitMethodDefinition(MethodDefinition method)
		{
		}
		
		public void TerminateModuleDefinition(ModuleDefinition @module)
		{
		}
		
		public void VisitExternType(TypeReference externType)
		{
		}
		
		public void VisitExternTypeCollection(Mono.Collections.Generic.Collection<TypeReference> externs)
		{
		}
		
		public void VisitInterface(TypeReference interf)
		{
		}
		
		public void VisitInterfaceCollection(Mono.Collections.Generic.Collection<TypeReference> interfaces)
		{
		}
		
		public void VisitMemberReference(MemberReference member)
		{
		}
		
		public void VisitMemberReferenceCollection(Mono.Collections.Generic.Collection<MemberReference> members)
		{
		}
		
		public void VisitCustomAttribute(CustomAttribute customAttr)
		{
		}
		
		public void VisitCustomAttributeCollection(Mono.Collections.Generic.Collection<CustomAttribute> customAttrs)
		{
		}
		
		public void VisitGenericParameter(GenericParameter genparam)
		{
		}
		
		public void VisitGenericParameterCollection(Mono.Collections.Generic.Collection<GenericParameter> genparams)
		{
		}
		
		public void VisitMarshalSpec(MarshalInfo MarshalInfo)
		{
		}
		
		public void VisitSecurityDeclaration(SecurityDeclaration secDecl)
		{
		}
		
		public void VisitSecurityDeclarationCollection(Mono.Collections.Generic.Collection<SecurityDeclaration> secDecls)
		{
		}
		
		public void VisitTypeReference(TypeReference type)
		{
		}
		
		public void VisitTypeReferenceCollection(Mono.Collections.Generic.Collection<TypeReference> refs)
		{
		}
		
		public void VisitOverride(MethodReference ov)
		{
		}
		
		public void VisitOverrideCollection(Mono.Collections.Generic.Collection<MethodReference> meth)
		{
		}
		
		public void VisitParameterDefinition(ParameterDefinition parameter)
		{
		}
		
		public void VisitParameterDefinitionCollection(Mono.Collections.Generic.Collection<ParameterDefinition> parameters)
		{
		}
		
		public void VisitPInvokeInfo(PInvokeInfo pinvk)
		{
		}
		#endregion

		#endregion
	}
}


