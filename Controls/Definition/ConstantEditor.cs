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
	public partial class ConstantEditor : UserControl
	{
		#region Events

		protected virtual void ConstantTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			ConstantPanel.Controls.Clear();
			ConstantPanel.Controls.Add((Control) ConstantTypes.SelectedItem);
			((IOperandEditor) ConstantTypes.SelectedItem).Initialize(null);
		}

		#endregion

		#region Methods

		public void Reset()
		{
			if (ConstantTypes.Items.Count > 0)
			{
				ConstantTypes.SelectedItem = ConstantTypes.Items[0];
			}
		}

		public void CopyStateTo(IConstantProvider item)
		{
			if (ConstantTypes.SelectedItem != null)
			{
				var editor = (IOperandEditor) ConstantTypes.SelectedItem;
				item.Constant = editor.SelectedOperand;
				item.HasConstant = !(editor is NoneOperandEditor);
			}
			else
			{
				item.Constant = null;
				item.HasConstant = false;
			}
		}

		public void ReadStateFrom(IConstantProvider item)
		{
			if (item.HasConstant)
			{
				if (item.Constant == null)
				{
					if (ConstantTypes.Items.Count > 1)
					{
						ConstantTypes.SelectedItem = ConstantTypes.Items[1];
					}
				}
				else
				{
					foreach (IOperandEditor editor in ConstantTypes.Items)
					{
						if (editor.IsOperandHandled(item.Constant))
						{
							ConstantTypes.SelectedItem = editor;
							editor.SelectedOperand = item.Constant;
							return;
						}
					}
				}
			}
			else
			{
				if (ConstantTypes.Items.Count > 0)
				{
					ConstantTypes.SelectedItem = ConstantTypes.Items[0];
				}
			}
		}

		public ConstantEditor()
		{
			InitializeComponent();

			ConstantTypes.Items.Add(new NoneOperandEditor());
			ConstantTypes.Items.Add(new NullOperandEditor());
			ConstantTypes.Items.Add(new ByteEditor());
			ConstantTypes.Items.Add(new SByteEditor());
			ConstantTypes.Items.Add(new IntegerEditor());
			ConstantTypes.Items.Add(new LongEditor());
			ConstantTypes.Items.Add(new SingleEditor());
			ConstantTypes.Items.Add(new DoubleEditor());

			var stringEditor = new StringEditor();
			var verbatimStringEditor = new VerbatimStringEditor();
			var bridge = new GenericOperandEditorBridge<string>(stringEditor, verbatimStringEditor);
			Disposed += delegate { bridge.Dispose(); };

			ConstantTypes.Items.Add(stringEditor);
			ConstantTypes.Items.Add(verbatimStringEditor);

			ConstantTypes.SelectedIndex = 0;
		}

		#endregion
	}
}