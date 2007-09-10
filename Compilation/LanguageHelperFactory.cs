
#region " Imports "
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    public class LanguageHelperFactory
    {

        #region " Methods "
        public static ILanguageHelper GetLanguageHelper(ESupportedLanguage language)
        {
            switch (language)
            {
                case ESupportedLanguage.CSharp: return new CSharpHelper();
                case ESupportedLanguage.VisualBasic: return new VisualBasicHelper(); 
                default: throw new System.NotSupportedException("this language is not supported");
            }
        }
        #endregion

    }
}