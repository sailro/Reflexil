
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{

    public interface IGlobalOperandEditor
    {

        #region " Properties "
        string Label
        {
            get;
        }
        bool IsOperandHandled(object operand);
        #endregion

        #region " Methods "
        void Initialize(MethodDefinition mdef);
        Instruction CreateInstruction(CilWorker worker, OpCode opcode);
        void SelectOperand(object operand);
        #endregion

    }

}

