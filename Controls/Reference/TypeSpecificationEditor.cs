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
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	public partial class TypeSpecificationEditor: UserControl
    {

        #region " Fields "
        private bool m_AllowArray = true;
        private bool m_AllowReference = true;
        private bool m_AllowPointer = true;
        private MethodDefinition m_mdef;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
            set
            {
                m_mdef = value;
            }
        }

        public bool AllowArray
        {
            get
            {
                return m_AllowArray;
            }
            set
            {
                m_AllowArray = value;
                UpdateSpecification(value, ETypeSpecification.Array);
            }
        }

        public bool AllowReference
        {
            get
            {
                return m_AllowReference;
            }
            set
            {
                m_AllowReference = value;
                UpdateSpecification(value, ETypeSpecification.Reference);
            }
        }

        public bool AllowPointer
        {
            get
            {
                return m_AllowPointer;
            }
            set
            {
                m_AllowPointer = value;
                UpdateSpecification(value, ETypeSpecification.Pointer);
            }
        }

        private void UpdateSpecification(bool allow, ETypeSpecification specification)
        {
            if (allow && !TypeSpecification.Items.Contains(specification))
            {
                TypeSpecification.Items.Add(specification);
            }
            else if (!allow && TypeSpecification.Items.Contains(specification))
            {
                TypeSpecification.Items.Remove(specification);
            }
        }

        public TypeReference SelectedTypeReference
        {
            get
            {
                IOperandEditor<TypeReference> editor = TypeScope.SelectedItem as IOperandEditor<TypeReference>;
                TypeReference tref = null;
                if (editor!=null) {
                    tref = editor.SelectedOperand;
                }
                switch ((ETypeSpecification)TypeSpecification.SelectedItem)
                {
                    case ETypeSpecification.Array: return new ArrayType(tref);
                    case ETypeSpecification.Reference: return new ByReferenceType(tref);
                    case ETypeSpecification.Pointer: return new PointerType(tref);
                    default: return tref;
                }
            }
            set
            {
                IOperandEditor editor = null;
                foreach (IOperandEditor item in TypeScope.Items)
                {
                    if (item.IsOperandHandled(value))
                    {
                        editor = item;
                        TypeScope.SelectedItem = item;
                        Operands_SelectedIndexChanged(this, EventArgs.Empty);
                        break;
                    }
                }
                TypeSpecification.SelectedItem = ETypeSpecification.Default;
                if (editor != null)
                {
                    if (value is TypeSpecification)
                    {
                        TypeSpecification tspec = value as TypeSpecification;
                        editor.SelectedOperand = tspec.ElementType;
                        if (value is ArrayType)
                        {
                            TypeSpecification.SelectedItem = ETypeSpecification.Array;
                        }
                        else if (value is ByReferenceType)
                        {
                            TypeSpecification.SelectedItem = ETypeSpecification.Reference;
                        }
                        else if (value is PointerType)
                        {
                            TypeSpecification.SelectedItem = ETypeSpecification.Pointer;
                        }
                    }
                    else
                    {
                        editor.SelectedOperand = value;
                    }
                }
            }
        }
        #endregion

        #region " Events "
        //public delegate void SelectedTypeReferenceChangedEventHandler(object sender, EventArgs e);
        //public event SelectedTypeReferenceChangedEventHandler SelectedTypeReferenceChanged;

        protected virtual void Operands_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypPanel.Controls.Clear();
            TypPanel.Controls.Add((Control)TypeScope.SelectedItem);
            if (MethodDefinition != null)
            {
                ((IOperandEditor)TypeScope.SelectedItem).Initialize(MethodDefinition);
            }
        }
        #endregion

        #region " Methods "
        public TypeSpecificationEditor()
        {
            InitializeComponent();

            TypeScope.Items.Add(new GenericTypeReferenceEditor());
            TypeScope.Items.Add(new TypeReferenceEditor());

            TypeSpecification.Items.Add(ETypeSpecification.Default);
            if (AllowArray) TypeSpecification.Items.Add(ETypeSpecification.Array);
            if (AllowReference) TypeSpecification.Items.Add(ETypeSpecification.Reference);
            if (AllowPointer) TypeSpecification.Items.Add(ETypeSpecification.Pointer);
            TypeSpecification.SelectedIndex = 0;
        }
        #endregion

    }
}
