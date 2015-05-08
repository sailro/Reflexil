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

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Reflexil.Wrappers
{
	/// <summary>
	/// Property wrapper
	/// </summary>
	internal class PropertyWrapper
	{
		#region Fields

		private readonly Dictionary<String, String> _prefixes;

		#endregion

		#region Properties

		public PropertyInfo PropertyInfo { get; private set; }

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinfo">Property info</param>
		/// <param name="prefixes">Prefixes used tio categorize properties</param>
		public PropertyWrapper(PropertyInfo pinfo, Dictionary<String, String> prefixes)
		{
			PropertyInfo = pinfo;
			_prefixes = prefixes;
		}

		/// <summary>
		/// Returns a String that represents the property
		/// </summary>
		/// <returns>A String like category: name</returns>
		public override string ToString()
		{
			string result = PropertyInfo.Name;
			if (_prefixes.ContainsKey(result))
			{
				result = String.Format("{0}: {1}", _prefixes[result], result);
			}
			return result;
		}

		#endregion
	}
}