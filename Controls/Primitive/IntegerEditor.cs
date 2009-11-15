/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Mono.Cecil.Cil;
using System;
#endregion

namespace Reflexil.Editors
{

    public partial class IntegerEditor : BaseIntegerEditor
	{
		
		#region " Methods "
		public override Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, SelectedOperand);
		}
		#endregion
		
	}

    #region " VS Designer generic support "
    public class BaseIntegerEditor : GenericOperandEditor<int>
    {
        public override Instruction CreateInstruction(CilWorker worker, OpCode opcode)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
	
}

