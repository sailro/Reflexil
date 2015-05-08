/*
    Copyright (C) 2012-2014 de4dot@gmail.com

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
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
    CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using System.Collections.Generic;
using dnlib.Utils;

namespace dnlib.DotNet.Writer {
	struct SectionSizeInfo {
		/// <summary>
		/// Length of section
		/// </summary>
		public readonly uint length;

		/// <summary>
		/// Section characteristics
		/// </summary>
		public readonly uint characteristics;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="length">Length of section</param>
		/// <param name="characteristics">Section characteristics</param>
		public SectionSizeInfo(uint length, uint characteristics) {
			this.length = length;
			this.characteristics = characteristics;
		}
	}

	/// <summary>
	/// Calculates the optional header section sizes
	/// </summary>
	struct SectionSizes {
		public readonly uint SizeOfHeaders;
		public readonly uint SizeOfImage;
		public readonly uint BaseOfData, BaseOfCode;
		public readonly uint SizeOfCode, SizeOfInitdData, SizeOfUninitdData;

		public SectionSizes(uint fileAlignment, uint sectionAlignment, uint headerLen, MFunc<IEnumerable<SectionSizeInfo>> getSectionSizeInfos) {
			SizeOfHeaders = Utils.AlignUp(headerLen, fileAlignment);
			SizeOfImage = Utils.AlignUp(SizeOfHeaders, sectionAlignment);
			BaseOfData = 0;
			BaseOfCode = 0;
			SizeOfCode = 0;
			SizeOfInitdData = 0;
			SizeOfUninitdData = 0;
			foreach (var section in getSectionSizeInfos()) {
				uint sectAlignedVs = Utils.AlignUp(section.length, sectionAlignment);
				uint fileAlignedVs = Utils.AlignUp(section.length, fileAlignment);

				bool isCode = (section.characteristics & 0x20) != 0;
				bool isInitdData = (section.characteristics & 0x40) != 0;
				bool isUnInitdData = (section.characteristics & 0x80) != 0;

				if (BaseOfCode == 0 && isCode)
					BaseOfCode = SizeOfImage;
				if (BaseOfData == 0 && (isInitdData || isUnInitdData))
					BaseOfData = SizeOfImage;
				if (isCode)
					SizeOfCode += fileAlignedVs;
				if (isInitdData)
					SizeOfInitdData += fileAlignedVs;
				if (isUnInitdData)
					SizeOfUninitdData += fileAlignedVs;

				SizeOfImage += sectAlignedVs;
			}
		}
	}
}
