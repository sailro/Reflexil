/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System.Globalization;
using System.Text;

namespace Reflexil.Utils
{
	public static class ByteHelper
	{
		public static string ByteToString(byte[] input)
		{
			if (input == null)
				return string.Empty;

			var sb = new StringBuilder();
			foreach (var b in input)
				sb.Append(b.ToString("x2"));

			return sb.ToString();
		}

		public static byte[] StringToByte(string input)
		{
			var result = new byte[input.Length/2];
			for (var i = 0; i < result.Length; i++)
				result[i] = byte.Parse(input.Substring(i*2, 2), NumberStyles.HexNumber);

			return result;
		}

		public static string GetDisplayBytes(long size)
		{
			const long multi = 1024;
			const long kb = multi;
			const long mb = kb*multi;
			const long gb = mb*multi;
			const long tb = gb*multi;

			string result;
			if (size < kb)
				result = string.Format("{0} Bytes", size);
			else if (size < mb)
				result = string.Format("{0} KB ({1} Bytes)",
					ConvertToOneDigit(size, kb), ConvertBytesDisplay(size));
			else if (size < gb)
				result = string.Format("{0} MB ({1} Bytes)",
					ConvertToOneDigit(size, mb), ConvertBytesDisplay(size));
			else if (size < tb)
				result = string.Format("{0} GB ({1} Bytes)",
					ConvertToOneDigit(size, gb), ConvertBytesDisplay(size));
			else
				result = string.Format("{0} TB ({1} Bytes)",
					ConvertToOneDigit(size, tb), ConvertBytesDisplay(size));

			return result;
		}

		private static string ConvertBytesDisplay(long size)
		{
			return size.ToString("###,###,###,###,###", CultureInfo.CurrentCulture);
		}

		private static string ConvertToOneDigit(long size, long quan)
		{
			var quotient = size/(double) quan;
			var result = quotient.ToString("0.#", CultureInfo.CurrentCulture);
			return result;
		}
	}
}