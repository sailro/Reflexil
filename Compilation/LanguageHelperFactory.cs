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

namespace Reflexil.Compilation
{
    /// <summary>
    /// Factory for ILanguageHelper implementations
    /// </summary>
    public class LanguageHelperFactory
    {

        #region " Methods "
        /// <summary>
        /// Get a ILanguageHelper from ESupportedLanguage enum.
        /// </summary>
        /// <param name="language">supported language</param>
        /// <returns>ILanguageHelper implementation</returns>
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