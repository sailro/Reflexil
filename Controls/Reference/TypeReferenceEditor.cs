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

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Windows.Forms;
using Reflexil.Compilation;
using Reflexil.Properties;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public class TypeReferenceEditor : BaseTypeReferenceEditor
	{
		protected override string PrepareText(TypeReference value)
		{
			if (!(value is GenericInstanceType))
				return base.PrepareText(value);

			var helper = LanguageHelperFactory.GetLanguageHelper(Settings.Default.Language);
			return helper.GetTypeSignature(value);
		}

		public override Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			var mdef = Context as MethodDefinition;
			return mdef != null ? worker.Create(opcode, CecilImporter.Import(mdef.DeclaringType.Module, SelectedOperand, mdef)) : null;
		}

		protected override void OnMouseHover(EventArgs e)
		{
			var tooltip = new ToolTip
			{
				ToolTipTitle = "Type",
				UseFading = true,
				UseAnimation = true,
				IsBalloon = true,
				ShowAlways = true,
				AutoPopDelay = 5000,
				InitialDelay = 1000,
				ReshowDelay = 0
			};

			tooltip.SetToolTip(this, Text);
		}
	}

	public class BaseTypeReferenceEditor : MemberReferenceEditor<TypeReference>
	{
	}
}