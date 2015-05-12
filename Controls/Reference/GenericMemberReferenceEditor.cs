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
using Mono.Cecil.Cil;
using Reflexil.Forms;

#endregion

namespace Reflexil.Editors
{
	public abstract class GenericMemberReferenceEditor<T> : BasePopupControl, IOperandEditor<T> where T : MemberReference
	{
		#region Fields

		private MethodDefinition _mdef;
		private T _operand;

		#endregion

		#region Properties

		public AssemblyDefinition AssemblyRestriction { get; set; }

		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}

		public string Label
		{
			get { return "-> " + typeof (T).Name.Replace("Reference", string.Empty) + " reference"; }
		}

		public string ShortLabel
		{
			get { return typeof (T).Name.Replace("Reference", string.Empty).Replace("Definition", string.Empty); }
		}

		public MethodDefinition MethodDefinition
		{
			get { return _mdef; }
		}

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (T) value; }
		}

		public T SelectedOperand
		{
			get { return _operand; }
			set
			{
				_operand = value;
				Text = PrepareText(value);
				RaiseSelectedOperandChanged();
			}
		}

		protected virtual string PrepareText(T value)
		{
			return _operand != null ? value.Name : string.Empty;
		}

		#endregion

		#region Events

		public event EventHandler SelectedOperandChanged;

		protected virtual void RaiseSelectedOperandChanged()
		{
			if (SelectedOperandChanged != null) SelectedOperandChanged(this, EventArgs.Empty);
		}

		private static MemberReference HandleGenericParameterProvider(MemberReference member)
		{
			if (member == null)
				return null;

			var provider = member as IGenericParameterProvider;
			if (provider == null || !provider.HasGenericParameters)
				return member;

			var form = GenericInstanceFormFactory.GetForm(member);
			if (form == null)
				return member;

			using (form)
			{
				if (form.ShowDialog() == DialogResult.OK)
					return (MemberReference) form.GenericInstance;
			}

			return member;
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			using (var refselectform = new GenericMemberReferenceForm<T>(_operand, AssemblyRestriction))
			{
				if (refselectform.ShowDialog() != DialogResult.OK)
					return;

				SelectedOperand = (T) HandleGenericParameterProvider(refselectform.SelectedItem);
			}
		}

		#endregion

		#region Methods

		protected GenericMemberReferenceEditor()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
		}

		public abstract Instruction CreateInstruction(ILProcessor worker, OpCode opcode);

		public void Initialize(MethodDefinition mdef)
		{
			_mdef = mdef;
		}

		#endregion
	}
}