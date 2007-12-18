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
                    case ETypeSpecification.Reference: return new ReferenceType(tref);
                    case ETypeSpecification.Pointer: return new PointerType(tref);
                    default: return tref;
                }
            }
            set
            {
                IGlobalOperandEditor editor = null;
                foreach (IGlobalOperandEditor item in TypeScope.Items)
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
                        editor.SelectOperand(tspec.ElementType);
                        if (value is ArrayType)
                        {
                            TypeSpecification.SelectedItem = ETypeSpecification.Array;
                        }
                        else if (value is ReferenceType)
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
                        editor.SelectOperand(value);
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
                ((IGlobalOperandEditor)TypeScope.SelectedItem).Initialize(MethodDefinition);
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
