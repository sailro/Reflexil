
#region " Imports "
using System.Collections.Generic;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    public interface ILanguageHelper
    {

        #region " Methods "
        string GetMethodSignature(MethodDefinition mdef);
        string GetMethod(MethodDefinition mdef);
        string GetField(FieldDefinition mdef);
        string GetTypeSignature(TypeDefinition tdef);
        string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references);
        #endregion

    }
}