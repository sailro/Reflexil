/* Reflexil Copyright (c) 2007-2021 Sebastien Lebreton

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
using Reflexil.Compilation;
using Reflexil.Properties;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public class MethodReferenceEditor : BaseMethodReferenceEditor
	{
		protected override string PrepareText(MethodReference value)
		{
			if (!(value is GenericInstanceMethod))
				return base.PrepareText(value);

			var helper = LanguageHelperFactory.GetLanguageHelper(Settings.Default.Language);
			return helper.GetMethodSignature(value);
		}

		public override Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			var mdef = Context as MethodDefinition;
			return mdef != null ? worker.Create(opcode, CecilImporter.Import(mdef.DeclaringType.Module, SelectedOperand, mdef)) : null;
		}
	}

	public class BaseMethodReferenceEditor : MemberReferenceEditor<MethodReference>
	{
	}
}
