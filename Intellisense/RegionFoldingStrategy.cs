/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.TextEditor.Document;

using Reflexil.Forms;
#endregion

namespace Reflexil.Intellisense
{
    class RegionFoldingStrategy : IFoldingStrategy
    {

        #region " Constants "
        const string gpname = "name";
        const string gpregion = "region";
        const string startregexp = "^.*#(?<" + gpregion + ">" + gpregion + ").*\\\"(?<" + gpname + ">.*)\\\".*$";
        const string stopregexp = "^.*#end.*region.*$";
        #endregion

        #region " Fields "
        private Regex startrxp;
        private Regex endrxp;
        #endregion

        #region " Methods "
        public RegionFoldingStrategy()
        {
            startrxp = new Regex(startregexp, RegexOptions.IgnoreCase);
            endrxp = new Regex(stopregexp, RegexOptions.IgnoreCase);
        }

        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();

            string name = string.Empty;
            int start = 0;
            int startindex = 0;

            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                string text = document.GetText(document.GetLineSegment(i));

                Match startmatch = startrxp.Match(text);
                if (startmatch.Success)
                {
                    name = startmatch.Groups[gpname].Value;
                    startindex = startmatch.Groups[gpregion].Index - 1;
                    start = i;
                }
                else if (endrxp.IsMatch(text))
                {
                    list.Add(new FoldMarker(document, start, startindex, i, document.GetLineSegment(i).Length, FoldType.Region, name, true));
                }
            }

            return list;
        }
        #endregion
        
    }
}
