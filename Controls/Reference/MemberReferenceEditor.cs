/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Forms;

namespace Reflexil.Editors
{
	public abstract class MemberReferenceEditor<T> : BasePopupControl, IOperandEditor<T>, IInstructionOperandEditor where T : MemberReference
	{
		private T _operand;

		public AssemblyDefinition AssemblyRestriction { get; set; }

		public bool IsOperandHandled(object operand)
		{
			return operand is T;
		}

		public string Label
		{
			get { return "-> " + typeof(T).Name.Replace("Reference", string.Empty) + " reference"; }
		}

		public string ShortLabel
		{
			get { return typeof(T).Name.Replace("Reference", string.Empty).Replace("Definition", string.Empty); }
		}

		public object Context { get; private set; }

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

		public event EventHandler SelectedOperandChanged;

		protected virtual void RaiseSelectedOperandChanged()
		{
			if (SelectedOperandChanged != null) SelectedOperandChanged(this, EventArgs.Empty);
		}

		private FieldReference HandleGenericParameterProvider(FieldReference field)
		{
			var reference = new FieldReference
			{
				Name = field.Name,
				DeclaringType = (TypeReference) HandleGenericParameterProvider(field.DeclaringType),
				FieldType = field.FieldType,
			};

			return (FieldReference) HandleGenericParameterProvider((MemberReference) reference);
		}

		private MethodReference HandleGenericParameterProvider(MethodReference method)
		{
			var reference = new MethodReference
			{
				Name = method.Name,
				DeclaringType = (TypeReference) HandleGenericParameterProvider(method.DeclaringType),
				HasThis = method.HasThis,
				ExplicitThis = method.ExplicitThis,
				ReturnType = method.ReturnType,
				CallingConvention = method.CallingConvention,
			};

			foreach (var parameter in method.Parameters)
				reference.Parameters.Add(new ParameterDefinition(parameter.ParameterType));

			foreach (var genericParameter in method.GenericParameters)
				reference.GenericParameters.Add(new GenericParameter(genericParameter.Name, reference));

			return (MethodReference) HandleGenericParameterProvider((MemberReference) reference);
		}

		private MemberReference HandleGenericParameterProvider(MemberReference member)
		{
			if (member == null)
				return null;

			var provider = member as IGenericParameterProvider;
			if (provider == null || !provider.HasGenericParameters)
				return member;

			var genericParameterProvider = Context as IGenericParameterProvider;
			var genericContext = genericParameterProvider != null ? genericParameterProvider : provider;

			var form = GenericInstanceFormFactory.GetForm(provider, genericContext);
			if (form == null)
				return member;

			using (form)
			{
				if (form.ShowDialog() != DialogResult.OK)
					throw new OperationCanceledException();

				var instance = form.GenericInstance;
				if (instance != null)
					return (MemberReference) instance;

				return (MemberReference) provider;
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			using (var refselectform = new MemberReferenceForm<T>(_operand, AssemblyRestriction))
			{
				if (refselectform.ShowDialog() != DialogResult.OK)
					return;

				try
				{
					T result;
					if (typeof(T) == typeof(MethodReference))
					{
						// So that we can even downcast a MethodDefinition to a MethodReference and instantiate the generic method + declaring type
						result = HandleGenericParameterProvider(refselectform.SelectedItem as MethodReference) as T;
					}
					else if (typeof(T) == typeof(FieldReference))
					{
						// So that we can even downcast a FieldDefinition to a FieldReference and instantiate the declaring type
						result = HandleGenericParameterProvider(refselectform.SelectedItem as FieldReference) as T;
					}
					else
					{
						result = (T) HandleGenericParameterProvider(refselectform.SelectedItem);
					}

					SelectedOperand = result;
				}
				catch (OperationCanceledException)
				{
				}
			}
		}

		protected MemberReferenceEditor()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
		}

		public virtual Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return null;
		}

		public void Refresh(object context)
		{
			Context = context;
		}
	}
}