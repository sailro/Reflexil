using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Handlers;

namespace Reflexil.Editors
{
	public partial class TypeSpecificationEditor: UserControl
    {

        #region " Fields "
        private bool m_AllowArray = true;
        private bool m_AllowReference = true;
        private bool m_AllowPointer = true;
        private MethodDefinitionHandler m_handler;
        #endregion

        #region " Properties "
        public MethodDefinitionHandler Handler
        {
            get
            {
                return m_handler;
            }
            set
            {
                m_handler = value;
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
            }
        }

        public TypeReference SelectedTypeReference
        {
            get
            {
                IOperandEditor<TypeReference> editor = TypeScope.SelectedItem as IOperandEditor<TypeReference>;
                TypeReference tref = null;
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
        public delegate void SelectedTypeReferenceChangedEventHandler(object sender, EventArgs e);
        public event SelectedTypeReferenceChangedEventHandler SelectedTypeReferenceChanged;
        #endregion

        public TypeSpecificationEditor()
		{
			InitializeComponent();

            TypeScope.Items.Add(new GenericTypeReferenceEditor());
            TypeScope.Items.Add(new TypeReferenceEditor());

            TypeSpecification.Items.Add(ETypeSpecification.Default);
            if (AllowArray) TypeSpecification.Items.Add(ETypeSpecification.Array);
            if (AllowReference) TypeSpecification.Items.Add(ETypeSpecification.Reference);
            if (AllowPointer) TypeSpecification.Items.Add(ETypeSpecification.Pointer);
        }

        protected virtual void Operands_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypPanel.Controls.Clear();
            TypPanel.Controls.Add((Control)TypeScope.SelectedItem);
            if (Handler != null)
            {
                ((IGlobalOperandEditor)TypeScope.SelectedItem).Initialize(Handler.MethodDefinition);
            }
        }
	}
}
