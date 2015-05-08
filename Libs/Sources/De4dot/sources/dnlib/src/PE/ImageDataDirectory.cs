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

﻿using System;
using System.Diagnostics;
using System.IO;
using dnlib.IO;

namespace dnlib.PE {
	/// <summary>
	/// Represents the IMAGE_DATA_DIRECTORY PE section
	/// </summary>
	[DebuggerDisplay("{virtualAddress} {dataSize}")]
	public sealed class ImageDataDirectory : FileSection {
		readonly RVA virtualAddress;
		readonly uint dataSize;

		/// <summary>
		/// Returns the IMAGE_DATA_DIRECTORY.VirtualAddress field
		/// </summary>
		public RVA VirtualAddress {
			get { return virtualAddress; }
		}

		/// <summary>
		/// Returns the IMAGE_DATA_DIRECTORY.Size field
		/// </summary>
		public uint Size {
			get { return dataSize; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public ImageDataDirectory() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reader">PE file reader pointing to the start of this section</param>
		/// <param name="verify">Verify section</param>
		/// <exception cref="BadImageFormatException">Thrown if verification fails</exception>
		public ImageDataDirectory(IImageStream reader, bool verify) {
			SetStartOffset(reader);
			this.virtualAddress = (RVA)reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			SetEndoffset(reader);
		}
	}
}
