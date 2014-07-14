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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Controls.Primitive;
using Mono.Cecil;
using System.Collections;
#endregion

namespace Reflexil.Editors
{
	public partial class CustomAttributeArgumentEditor: UserControl
    {

        #region " Fields "
        private bool m_AllowArray = true;
        #endregion

        #region " Properties "
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

        public CustomAttributeArgument SelectedArgument
        {
            get
            {
                TypeReference tref = TypeReferenceEditor.SelectedOperand;
                switch ((ETypeSpecification)TypeSpecification.SelectedItem)
                {
                    case ETypeSpecification.Array: 
                        tref = new ArrayType(tref);
                        break;
                    default: 
                        break;
                }

                object value = null;
                if (ArgumentTypes.SelectedItem != null)
                {
                    var editor = (IOperandEditor)ArgumentTypes.SelectedItem;

                    if (tref is ArrayType)
                    {
                        // Even with arraytype, editor can be IOperandEditor only (TypeReference)
                        if (ArgumentTypes.SelectedItem is IOperandsEditor)
                        {
                            var xeditor = (IOperandsEditor)editor;
                            value = WrapValues(xeditor.SelectedOperands);
                        }
                        else
                            value = WrapValues(new[] { editor.SelectedOperand });
                    } else
                        value = editor.SelectedOperand;
                }
                return new CustomAttributeArgument(tref, value);
            }
            set
            {
                TypeReferenceEditor.SelectedOperand = value.Type;
                TypeSpecification.SelectedItem = ETypeSpecification.Default;
                if (value.Type is TypeSpecification)
                {
                    var tspec = value.Type as TypeSpecification;
                    TypeReferenceEditor.SelectedOperand = tspec.ElementType;
                    if (value.Type is ArrayType)
                        TypeSpecification.SelectedItem = ETypeSpecification.Array;
                }

                if (value.Value == null)
                {
                    if (ArgumentTypes.Items.Count > 0)
                        ArgumentTypes.SelectedItem = ArgumentTypes.Items[0];
                }
                else
                {
                    if (value.Value is CustomAttributeArgument)
                    {
                        SelectedArgument = (CustomAttributeArgument)value.Value;
                        return;
                    }

                    foreach (IOperandEditor editor in ArgumentTypes.Items)
                    {
                        if (editor is IOperandsEditor && (ETypeSpecification)TypeSpecification.SelectedItem == ETypeSpecification.Array)
                        {
                            var xeditor = (IOperandsEditor) editor;
                            var values = UnwrapValues(value.Value);
                            if (xeditor.IsOperandsHandled(values))
                            {
                                ArgumentTypes.SelectedItem = xeditor;
                                xeditor.SelectedOperands = values;
								return;
                            }
                        }

                        if (editor.IsOperandHandled(value.Value))
                        {
                            ArgumentTypes.SelectedItem = editor;
                            editor.SelectedOperand = value.Value;
	                        return;
                        } 
                    }
                }
            }
        }

        private static object WrapValues(object values)
        {
            if (values is Array)
            {
                var array = values as Array;
                var etype = array.GetType().GetElementType();
                var tref = new TypeReference(etype.Namespace, etype.Name, null, null);

                return (from object item in array select new CustomAttributeArgument(tref, item)).ToArray();
            }
            return null;
        }

        private static object UnwrapValues(object values)
        {
            ArrayList result = new ArrayList();
            var arguments = values as CustomAttributeArgument[];
            Type rType = null;

            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    if (rType == null && argument.Value != null)
                        rType = argument.Value.GetType();
                    result.Add(argument.Value);
                }
            }

            if (rType == null)
                return null;

            return result.ToArray(rType);
        }
        #endregion

        #region " Events "
        private void ArgumentTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArgumentPanel.Controls.Clear();
            ArgumentPanel.Controls.Add((Control)ArgumentTypes.SelectedItem);
            ((IOperandEditor)ArgumentTypes.SelectedItem).Initialize(null);
        }
        #endregion

        #region " Methods "
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

        public CustomAttributeArgumentEditor()
        {
            InitializeComponent();

            TypeSpecification.Items.Add(ETypeSpecification.Default);
            if (AllowArray) TypeSpecification.Items.Add(ETypeSpecification.Array);
            TypeSpecification.SelectedIndex = 0;

            ArgumentTypes.Items.Add(new NullOperandEditor());
            ArgumentTypes.Items.Add(new BooleanEditor());
            ArgumentTypes.Items.Add(new ByteEditor());
            ArgumentTypes.Items.Add(new SByteEditor());
            ArgumentTypes.Items.Add(new IntegerEditor());
            ArgumentTypes.Items.Add(new LongEditor());
            ArgumentTypes.Items.Add(new SingleEditor());
            ArgumentTypes.Items.Add(new DoubleEditor());

			var stringEditor = new StringEditor();
			var verbatimStringEditor = new VerbatimStringEditor();
			var bridge = new GenericOperandEditorBridge<string>(stringEditor, verbatimStringEditor);
			Disposed += delegate { bridge.Dispose(); };

			ArgumentTypes.Items.Add(stringEditor);
			ArgumentTypes.Items.Add(verbatimStringEditor);
			
			ArgumentTypes.Items.Add(new TypeReferenceEditor());

            ArgumentTypes.SelectedIndex = 0;
        }
        #endregion


    }
}
