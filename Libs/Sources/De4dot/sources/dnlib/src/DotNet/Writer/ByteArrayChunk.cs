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

﻿using System.IO;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer {
	/// <summary>
	/// Stores a byte array
	/// </summary>
	public sealed class ByteArrayChunk : IChunk {
		readonly byte[] array;
		FileOffset offset;
		RVA rva;

		/// <inheritdoc/>
		public FileOffset FileOffset {
			get { return offset; }
		}

		/// <inheritdoc/>
		public RVA RVA {
			get { return rva; }
		}

		/// <summary>
		/// Gets the data
		/// </summary>
		public byte[] Data {
			get { return array; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="array">The data. It will be owned by this instance and can't be modified by
		/// other code if this instance is inserted as a <c>key</c> in a dictionary (because
		/// <see cref="GetHashCode"/> return value will be different if you modify the array). If
		/// it's never inserted as a <c>key</c> in a dictionary, then the contents can be modified,
		/// but shouldn't be resized after <see cref="SetOffset"/> has been called.</param>
		public ByteArrayChunk(byte[] array) {
			this.array = array ?? new byte[0];
		}

		/// <inheritdoc/>
		public void SetOffset(FileOffset offset, RVA rva) {
			this.offset = offset;
			this.rva = rva;
		}

		/// <inheritdoc/>
		public uint GetFileLength() {
			return (uint)array.Length;
		}

		/// <inheritdoc/>
		public uint GetVirtualSize() {
			return GetFileLength();
		}

		/// <inheritdoc/>
		public void WriteTo(BinaryWriter writer) {
			writer.Write(array);
		}

		/// <inheritdoc/>
		public override int GetHashCode() {
			return Utils.GetHashCode(array);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj) {
			var other = obj as ByteArrayChunk;
			return other != null && Utils.Equals(array, other.array);
		}
	}
}
