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
using System.Windows.Forms;
using Mono.Cecil;

#endregion

namespace Reflexil.Editors
{
	public sealed partial class TypeSpecificationEditor : UserControl
	{
		#region Fields

		private bool _allowArray = true;
		private bool _allowReference = true;
		private bool _allowPointer = true;

		#endregion

		#region Properties

		public MethodDefinition MethodDefinition { get; set; }

		public bool AllowArray
		{
			get { return _allowArray; }
			set
			{
				_allowArray = value;
				UpdateSpecification(value, TypeSpecification.Array);
			}
		}

		public bool AllowReference
		{
			get { return _allowReference; }
			set
			{
				_allowReference = value;
				UpdateSpecification(value, TypeSpecification.Reference);
			}
		}

		public bool AllowPointer
		{
			get { return _allowPointer; }
			set
			{
				_allowPointer = value;
				UpdateSpecification(value, TypeSpecification.Pointer);
			}
		}

		private void UpdateSpecification(bool allow, TypeSpecification specification)
		{
			foreach (var tslevel in new[] {TypeSpecificationL1, TypeSpecificationL2, TypeSpecificationL3})
			{
				if (allow && !tslevel.Items.Contains(specification))
				{
					tslevel.Items.Add(specification);
				}
				else if (!allow && tslevel.Items.Contains(specification))
				{
					tslevel.Items.Remove(specification);
				}
			}
		}

		public TypeReference SelectedTypeReference
		{
			get
			{
				var editor = TypeScope.SelectedItem as IOperandEditor<TypeReference>;
				TypeReference tref = null;
				if (editor != null)
				{
					tref = editor.SelectedOperand;
				}

				foreach (var tslevel in new[] {TypeSpecificationL3, TypeSpecificationL2, TypeSpecificationL1})
				{
					switch ((TypeSpecification) tslevel.SelectedItem)
					{
						case TypeSpecification.Array:
							tref = new ArrayType(tref);
							break;
						case TypeSpecification.Reference:
							tref = new ByReferenceType(tref);
							break;
						case TypeSpecification.Pointer:
							tref = new PointerType(tref);
							break;
					}
				}

				return tref;
			}
			set
			{
				IOperandEditor editor = null;
				foreach (IOperandEditor item in TypeScope.Items)
				{
					if (!item.IsOperandHandled(value))
						continue;

					editor = item;
					TypeScope.SelectedItem = item;
					Operands_SelectedIndexChanged(this, EventArgs.Empty);
					break;
				}

				var nested = value;
				foreach (var tslevel in new[] {TypeSpecificationL1, TypeSpecificationL2, TypeSpecificationL3})
				{
					tslevel.SelectedItem = TypeSpecification.Default;

					if (!(nested is Mono.Cecil.TypeSpecification))
						continue;

					if (nested is ArrayType)
					{
						tslevel.SelectedItem = TypeSpecification.Array;
					}
					else if (nested is ByReferenceType)
					{
						tslevel.SelectedItem = TypeSpecification.Reference;
					}
					else if (nested is PointerType)
					{
						tslevel.SelectedItem = TypeSpecification.Pointer;
					}
					else if (nested is GenericInstanceType)
						continue;

					var tspec = nested as Mono.Cecil.TypeSpecification;
					nested = tspec.ElementType;
				}

				if (editor != null)
					editor.SelectedOperand = nested;
			}
		}

		#endregion

		#region Events

		//public delegate void SelectedTypeReferenceChangedEventHandler(object sender, EventArgs e);
		//public event SelectedTypeReferenceChangedEventHandler SelectedTypeReferenceChanged;

		private void Operands_SelectedIndexChanged(object sender, EventArgs e)
		{
			TypPanel.Controls.Clear();
			TypPanel.Controls.Add((Control) TypeScope.SelectedItem);
			if (MethodDefinition != null)
			{
				((IOperandEditor) TypeScope.SelectedItem).Initialize(MethodDefinition);
			}
		}

		#endregion

		#region Methods

		public TypeSpecificationEditor()
		{
			InitializeComponent();

			TypeScope.Items.Add(new GenericTypeReferenceEditor());
			TypeScope.Items.Add(new TypeReferenceEditor());

			foreach (var tslevel in new[] {TypeSpecificationL1, TypeSpecificationL2, TypeSpecificationL3})
			{
				tslevel.Items.Add(TypeSpecification.Default);

				if (AllowArray) tslevel.Items.Add(TypeSpecification.Array);
				if (AllowReference) tslevel.Items.Add(TypeSpecification.Reference);
				if (AllowPointer) tslevel.Items.Add(TypeSpecification.Pointer);

				tslevel.SelectedIndex = 0;
			}
		}

		#endregion
	}
}