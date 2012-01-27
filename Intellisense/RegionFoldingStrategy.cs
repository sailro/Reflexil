/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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
