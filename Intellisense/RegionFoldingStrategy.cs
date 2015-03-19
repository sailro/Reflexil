/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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

#region Imports

using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.TextEditor.Document;

#endregion

namespace Reflexil.Intellisense
{
	internal class RegionFoldingStrategy : IFoldingStrategy
	{
		#region Constants

		private const string GpName = "name";
		private const string GpRegion = "region";
		private const string StartRegexp = "^.*#(?<" + GpRegion + ">" + GpRegion + ").*\\\"(?<" + GpName + ">.*)\\\".*$";
		private const string StopRegexp = "^.*#end.*region.*$";

		#endregion

		#region Fields

		private readonly Regex _startrxp;
		private readonly Regex _endrxp;

		#endregion

		#region Methods

		public RegionFoldingStrategy()
		{
			_startrxp = new Regex(StartRegexp, RegexOptions.IgnoreCase);
			_endrxp = new Regex(StopRegexp, RegexOptions.IgnoreCase);
		}

		public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
		{
			var list = new List<FoldMarker>();

			var name = string.Empty;
			var start = 0;
			var startindex = 0;

			for (var i = 0; i < document.TotalNumberOfLines; i++)
			{
				var text = document.GetText(document.GetLineSegment(i));

				var startmatch = _startrxp.Match(text);
				if (startmatch.Success)
				{
					name = startmatch.Groups[GpName].Value;
					startindex = startmatch.Groups[GpRegion].Index - 1;
					start = i;
				}
				else if (_endrxp.IsMatch(text))
				{
					list.Add(new FoldMarker(document, start, startindex, i, document.GetLineSegment(i).Length, FoldType.Region, name,
						true));
				}
			}

			return list;
		}

		#endregion
	}
}