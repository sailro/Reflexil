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

#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
	sealed partial class GenericMemberReferenceForm<T> : IComparer, IReflectionVisitor where T : MemberReference
	{
		#region Constants

		private const string ExpanderNodeKey = "|-expander-|";

		#endregion

		#region Fields

		private T _selected;
		private Thread _searchThread;
		private volatile bool _requestStopThread;
		private readonly Dictionary<object, TreeNode> _nodes = new Dictionary<object, TreeNode>();

		private readonly Dictionary<IReflectionVisitable, IReflectionVisitable> _visitedItems =
			new Dictionary<IReflectionVisitable, IReflectionVisitable>();

		private readonly Dictionary<Type, int> _orders = new Dictionary<Type, int>();

		#endregion

		#region Properties

		private AssemblyDefinition AssemblyRestriction { get; set; }

		public MemberReference SelectedItem
		{
			get { return _selected; }
		}

		#endregion

		#region Events

		private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			LoadNodeOnDemand(e.Node);
		}

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				if (e.Node.Tag is T)
				{
					_selected = (T) e.Node.Tag;
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

		private void MemberReferenceForm_Shown(Object sender, EventArgs e)
		{
			TreeView.Focus();
		}

		private void Search_TextChanged(object sender, EventArgs e)
		{
			try
			{
				var regex = new Regex(Search.Text, RegexOptions.IgnoreCase);
				SearchNodes(regex);
			}
			catch (Exception)
			{
				Search.ForeColor = Color.Red;
			}
		}

		private void GenericMemberReferenceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			WaitForCompleteCancelation();
		}

		#endregion

		#region Methods

		public GenericMemberReferenceForm(T selected, AssemblyDefinition assemblyRestriction)
		{
			InitializeComponent();

			var keyword = (typeof (T).Name.Contains("Reference")) ? "Reference" : "Definition";
			Text = Text + typeof (T).Name.Replace(keyword, string.Empty).ToLower();
			ImageList.Images.AddStrip(PluginFactory.GetInstance().GetAllBrowserImages());

			foreach (var asm in PluginFactory.GetInstance().Package.HostAssemblies)
				AppendRootNode(asm);

			_orders.Add(typeof (AssemblyDefinition), 0);
			_orders.Add(typeof (TypeDefinition), 1);
			_orders.Add(typeof (MethodDefinition), 2);
			_orders.Add(typeof (PropertyDefinition), 3);
			_orders.Add(typeof (EventDefinition), 3);
			_orders.Add(typeof (FieldDefinition), 5);

			TreeView.TreeViewNodeSorter = this;
			AssemblyRestriction = assemblyRestriction;

			ButOk.Enabled = selected != null && SelectItem(selected);
		}

		#region Selection

		private IEnumerable<AssemblyDefinition> GetAssemblyDefinitionsByNodeName(string name)
		{
			foreach (TreeNode subNode in TreeView.Nodes)
			{
				if (subNode.Text != name)
					continue;

				if ((subNode.Tag) is IAssemblyWrapper)
					LoadNodeOnDemand(subNode);

				var adef = subNode.Tag as AssemblyDefinition;
				if (adef != null)
					yield return adef;
			}
		}

		private static string StripGenerics(TypeReference type, MemberReference member = null)
		{
			var output = member == null ? type.ToString() : member.ToString();

			var gim = member as GenericInstanceMethod;
			if (gim != null)
				output = output.Replace(member.ToString(), gim.ElementMethod.ToString());

			var git = type as GenericInstanceType;
			if (git != null)
				output = output.Replace(type.ToString(), git.ElementType.ToString());

			return output;
		}

		private TypeDefinition GetTypeDefinition(TypeReference item)
		{
			ModuleDefinition moddef = null;

			if ((item.Scope) is ModuleDefinition)
			{
				moddef = (ModuleDefinition) item.Scope;
				
				// Force node lazy load for all candidates, we already have the module
				// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				GetAssemblyDefinitionsByNodeName(moddef.Assembly.Name.Name).ToList();
			}
			else if ((item.Scope) is AssemblyNameReference)
			{
				var anr = (AssemblyNameReference) item.Scope;
				var asmdef = GetAssemblyDefinitionsByNodeName(anr.Name).FirstOrDefault();

				if (asmdef != null)
					moddef = asmdef.MainModule;
			}

			if (moddef == null)
				return null;

			var typedef = CecilHelper.FindMatchingType(moddef.Types, StripGenerics(item));
			if (typedef == null)
				return null;

			if (typedef.DeclaringType != null)
				GetTypeDefinition(typedef.DeclaringType);

			TreeNode moduleNode;
			TreeNode typeNode;

			if (_nodes.TryGetValue(moddef, out moduleNode))
				LoadNodeOnDemand(moduleNode);

			if (_nodes.TryGetValue(typedef, out typeNode))
				LoadNodeOnDemand(typeNode);

			return typedef;
		}

		private MethodDefinition GetMethodDefinition(MethodReference item)
		{
			var typedef = GetTypeDefinition(item.DeclaringType);
			return typedef == null
				? null
				: typedef.Methods.FirstOrDefault(
					method => StripGenerics(typedef, method) == StripGenerics(item.DeclaringType, item));
		}

		private FieldDefinition GetFieldDefinition(FieldReference item)
		{
			var typedef = GetTypeDefinition(item.DeclaringType);
			return typedef != null
				? typedef.Fields.FirstOrDefault(
					field => StripGenerics(typedef, field) == StripGenerics(item.DeclaringType, item))
				: null;
		}

		private bool SelectItem(MemberReference item)
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

			if (itemtag != null && _nodes.ContainsKey(itemtag))
			{
				TreeView.SelectedNode = _nodes[itemtag];
				_selected = (T) item;
				return true;
			}

			return false;
		}

		#endregion

		#region Cosmetic

		public int Compare(object x, object y)
		{
			var xn = (TreeNode) x;
			var yn = (TreeNode) y;

			var result = 0;
			if (xn.Tag != null && yn.Tag != null)
			{
				if (_orders.ContainsKey(xn.Tag.GetType()))
					result = _orders[xn.Tag.GetType()].CompareTo(_orders[yn.Tag.GetType()]);
			}

			if (result == 0)
				result = String.Compare(xn.Text, yn.Text, StringComparison.Ordinal);

			return result;
		}

		private static int ScopeOffset(int attributes, int mask, int publicValue, int friendValue, int protectedFriendValue,
			int protectedValue, int privateValue)
		{
			int[] scopes = {publicValue, friendValue, protectedFriendValue, protectedValue, privateValue};
			attributes = attributes & mask;

			for (var i = 0; i <= scopes.Length - 1; i++)
			{
				if (attributes == scopes[i])
					return i;
			}

			return 0;
		}

		private static EBrowserImages ImageIndex(object obj)
		{
			var offset = EBrowserImages.Empty;

			var tdef = obj as TypeDefinition;
			if (tdef != null)
			{
				if (tdef.IsEnum)
				{
					offset = EBrowserImages.PublicEnum;
				}
				else if (tdef.IsInterface)
				{
					offset = EBrowserImages.PublicInterface;
				}
				else if (tdef.IsValueType)
				{
					offset = EBrowserImages.PublicStructure;
				}
				else
				{
					offset = EBrowserImages.PublicClass;
				}
				if ((tdef.Attributes & TypeAttributes.VisibilityMask) < TypeAttributes.Public)
				{
					offset = (EBrowserImages) ((int) offset + EBrowserImages.FriendClass - EBrowserImages.PublicClass);
				}
				else
				{
					offset = offset +
					         ScopeOffset(Convert.ToInt32(tdef.Attributes), (int) TypeAttributes.VisibilityMask,
						         (int) TypeAttributes.NestedPublic, (int) TypeAttributes.NestedAssembly,
						         (int) TypeAttributes.NestedFamORAssem, (int) TypeAttributes.NestedFamily,
						         (int) TypeAttributes.NestedPrivate);
				}
			}
			else
			{
				var pdef = obj as PropertyDefinition;
				if (pdef != null)
				{
					offset = EBrowserImages.PublicProperty;

					if (pdef.GetMethod == null && pdef.SetMethod != null)
					{
						offset = pdef.SetMethod.IsStatic
							? EBrowserImages.PublicSharedWriteOnlyProperty
							: EBrowserImages.PublicWriteOnlyProperty;
					}
					else if (pdef.GetMethod != null && pdef.SetMethod == null)
					{
						offset = pdef.GetMethod.IsStatic
							? EBrowserImages.PublicSharedReadOnlyProperty
							: EBrowserImages.PublicReadOnlyProperty;
					}
					else if (pdef.GetMethod != null && pdef.SetMethod != null)
					{
						offset = pdef.GetMethod.IsStatic ? EBrowserImages.PublicSharedProperty : EBrowserImages.PublicProperty;
					}
				}
				else
				{
					var mdef = obj as MethodDefinition;
					if (mdef != null)
					{
						if (mdef.IsConstructor)
						{
							offset = mdef.IsStatic ? EBrowserImages.PublicSharedConstructor : EBrowserImages.PublicConstructor;
						}
						else
						{
							if (mdef.IsVirtual)
							{
								offset = mdef.IsStatic ? EBrowserImages.PublicSharedOverrideMethod : EBrowserImages.PublicOverrideMethod;
							}
							else
							{
								offset = mdef.IsStatic ? EBrowserImages.PublicSharedMethod : EBrowserImages.PublicMethod;
							}
						}
						offset = offset +
						         ScopeOffset((int) mdef.Attributes, (int) MethodAttributes.MemberAccessMask, (int) MethodAttributes.Public,
							         (int) MethodAttributes.Assembly, (int) MethodAttributes.FamORAssem, (int) MethodAttributes.Family,
							         (int) MethodAttributes.Private);
					}
					else
					{
						var fdef = obj as FieldDefinition;
						if (fdef != null)
						{
							if (fdef.IsLiteral && fdef.IsStatic)
							{
								offset = EBrowserImages.PublicEnumValue;
							}
							else
							{
								offset = fdef.IsStatic ? EBrowserImages.PublicSharedField : EBrowserImages.PublicField;
							}
							offset = offset +
							         ScopeOffset((int) fdef.Attributes, (int) FieldAttributes.FieldAccessMask, (int) FieldAttributes.Public,
								         (int) FieldAttributes.Assembly, (int) FieldAttributes.FamORAssem, (int) FieldAttributes.Family,
								         (int) FieldAttributes.Private);
						}
						else if (obj is ModuleDefinition)
						{
							offset = EBrowserImages.Module;
						}
						else
						{
							var edef = obj as EventDefinition;
							if (edef != null)
							{
								offset = edef.AddMethod.IsStatic ? EBrowserImages.PublicSharedEvent : EBrowserImages.PublicEvent;
							}
							else if (obj is AssemblyDefinition || (obj) is IAssemblyWrapper)
							{
								offset = EBrowserImages.Assembly;
							}
							else if (obj is NamespaceWrapper)
							{
								offset = EBrowserImages.PublicNamespace;
							}
						}
					}
				}
			}

			return offset;
		}

		private static string DisplayString(object obj)
		{
			var mdef = obj as MethodDefinition;
			if (mdef != null)
				return mdef.ToString().Substring(mdef.ToString().IndexOf("::", StringComparison.Ordinal) + 2) + " : " +
				       mdef.ReturnType;

			var pdef = obj as PropertyDefinition;
			if (pdef != null)
				return pdef.ToString().Substring(pdef.ToString().IndexOf("::", StringComparison.Ordinal) + 2) + " : " +
				       pdef.PropertyType;

			var fdef = obj as FieldDefinition;
			if (fdef != null)
				return fdef.ToString().Substring(fdef.ToString().IndexOf("::", StringComparison.Ordinal) + 2) + " : " +
				       fdef.FieldType;

			var modef = obj as ModuleDefinition;
			if (modef != null)
				return modef.Name;

			var tdef = obj as TypeDefinition;
			if (tdef != null)
				return tdef.Name;

			var adef = obj as AssemblyDefinition;
			if (adef != null)
				return adef.Name.Name;

			if (obj is IAssemblyWrapper)
				return obj.ToString();

			var edef = obj as EventDefinition;
			if (edef != null)
				return edef.Name;

			return obj.ToString();
		}

		#endregion

		#region Node management

		private void LoadNodeOnDemand(TreeNode node)
		{
			if (node.Nodes.ContainsKey(ExpanderNodeKey))
				node.Nodes.RemoveAt(node.Nodes.IndexOfKey(ExpanderNodeKey));

			var visitable = node.Tag as IReflectionVisitable;
			if (visitable != null)
			{
				if (!_visitedItems.ContainsKey(visitable))
				{
					visitable.Accept(this);
					_visitedItems.Add(visitable, visitable);
				}
			}

			else if ((node.Tag) is IAssemblyWrapper)
			{
				var iasm = (IAssemblyWrapper) node.Tag;
				var context = PluginFactory.GetInstance().GetAssemblyContext(iasm.Location);
				if (context == null)
					return;

				var asmdef = context.AssemblyDefinition;
				if ((AssemblyRestriction == null) || (asmdef == AssemblyRestriction))
				{
					_nodes.Remove(node.Tag);
					_nodes.Add(asmdef, node);
					node.Tag = asmdef;

					foreach (var moddef in asmdef.Modules)
						AppendNode(asmdef, moddef, moddef.Types.Count > 0);
				}
				else
				{
					node.Tag = "restricted";
					node.Text += String.Format(" (You can't use this assembly for selection -> restricted to {0})",
						AssemblyRestriction.Name.Name);
				}
			}
		}

		private void AppendRootNode(IAssemblyWrapper root)
		{
			if (!root.IsValid)
				return;

			var rootnode = new TreeNode(DisplayString(root)) {ImageIndex = (int) ImageIndex(root)};
			rootnode.SelectedImageIndex = rootnode.ImageIndex;
			rootnode.Tag = root;
			rootnode.Nodes.Add(ExpanderNodeKey, ExpanderNodeKey);
			TreeView.Nodes.Add(rootnode);
			_nodes.Add(root, rootnode);
		}

		private void AppendNode(TreeNode ownernode, object child, bool createExpander)
		{
			if (_nodes.ContainsKey(child))
				return;

			var childnode = new TreeNode(DisplayString(child)) {ImageIndex = (int) ImageIndex(child)};
			childnode.SelectedImageIndex = childnode.ImageIndex;
			childnode.Tag = child;

			if (createExpander)
				childnode.Nodes.Add(ExpanderNodeKey, ExpanderNodeKey);

			ownernode.Nodes.Add(childnode);
			_nodes.Add(child, childnode);
		}

		private void AppendNode(object owner, object child, bool createExpander)
		{
			TreeNode ownernode;
			
			if (_nodes.TryGetValue(owner, out ownernode))
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
				Invoke((Action) (() => TreeView.Visible = true));
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception)
			{
			}
		}

		private void OnSearchFound(TreeNode node)
		{
			try
			{
				Invoke((Action) (() => Search.ForeColor = Color.Blue));
				Invoke((Action) (() => TreeView.SelectedNode = node));
				Invoke((Action) (() => TreeView.Visible = true));
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception)
			{
			}
		}

		private void OnSearchNotFound()
		{
			try
			{
				Invoke((Action) (() => Search.ForeColor = Color.Red));
				Invoke((Action) (() => TreeView.Visible = true));
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch (Exception)
			{
			}
		}

		private SearchResult InternalSearchNodes(TreeNodeCollection nodes, Regex regex, int depth)
		{
			foreach (TreeNode node in nodes)
			{
				if (_requestStopThread)
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

		private void SafeInternalSearchNodes(Object state)
		{
			try
			{
				InternalSearchNodes(TreeView.Nodes, state as Regex, 0);
			}
				// ReSharper disable once EmptyGeneralCatchClause			
			catch (Exception)
			{
			}
		}

		private void SearchNodes(Regex regex)
		{
			WaitForCompleteCancelation();

			TreeView.Visible = false;
			Search.ForeColor = Color.Gray;

			_searchThread = new Thread(SafeInternalSearchNodes);
			_searchThread.Start(regex);
		}

		private void WaitForCompleteCancelation()
		{
			try
			{
				if (_requestStopThread)
					return;

				const int timeout = 200;
				var retry = 5;
				var done = false;

				if (_searchThread != null)
				{
					_requestStopThread = true;

					while (!done && retry > 0)
					{
						if (!(done = _searchThread.Join(timeout)))
							Application.DoEvents();
						retry--;
					}

					if (!done)
						_searchThread.Abort();

					_requestStopThread = false;
				}
				_searchThread = null;
			}
			catch (Exception)
			{
				_requestStopThread = false;
			}
		}

		#endregion

		#region Visitor implementation

		public void VisitConstructorCollection(Mono.Collections.Generic.Collection<MethodDefinition> ctors)
		{
			foreach (var constructor in ctors)
				AppendNode(constructor.DeclaringType, constructor, false);
		}

		public void VisitEventDefinitionCollection(Mono.Collections.Generic.Collection<EventDefinition> events)
		{
			foreach (var evt in events)
			{
				AppendNode(evt.DeclaringType, evt, true);
				if (evt.AddMethod != null)
					AppendNode(evt, evt.AddMethod, false);

				if (evt.RemoveMethod != null)
					AppendNode(evt, evt.RemoveMethod, false);
			}
		}

		public void VisitFieldDefinitionCollection(Mono.Collections.Generic.Collection<FieldDefinition> fields)
		{
			foreach (var field in fields)
				AppendNode(field.DeclaringType, field, false);
		}

		public void VisitMethodDefinitionCollection(Mono.Collections.Generic.Collection<MethodDefinition> methods)
		{
			foreach (var method in methods)
			{
				if (! method.IsSpecialName || method.IsConstructor)
					AppendNode(method.DeclaringType, method, false);
			}
		}

		public void VisitNestedTypeCollection(Mono.Collections.Generic.Collection<TypeDefinition> nestedTypes)
		{
			foreach (var nestedType in nestedTypes)
				AppendNode(nestedType.DeclaringType, nestedType, true);
		}

		public void VisitPropertyDefinitionCollection(Mono.Collections.Generic.Collection<PropertyDefinition> properties)
		{
			foreach (var @property in properties)
			{
				AppendNode(@property.DeclaringType, @property, true);
				if (@property.GetMethod != null)
					AppendNode(@property, @property.GetMethod, false);

				if (@property.SetMethod != null)
					AppendNode(@property, @property.SetMethod, false);
			}
		}

		public void VisitTypeDefinitionCollection(Mono.Collections.Generic.Collection<TypeDefinition> types)
		{
			foreach (var typedef in types)
			{
				if ((typedef.Attributes & TypeAttributes.VisibilityMask) > TypeAttributes.Public)
					continue;

				var wrapper = new NamespaceWrapper(typedef.Module, typedef.Namespace);
				AppendNode(typedef.Module, wrapper, true);
				AppendNode(wrapper, typedef, true);
			}
		}

		#endregion

		#region Unimplemented vistor

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

		public void VisitMethodReference(MethodReference method)
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

		public void VisitMarshalSpec(MarshalInfo marshalInfo)
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