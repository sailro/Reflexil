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

namespace dnlib.IO {
	/// <summary>
	/// Creates <see cref="UnmanagedMemoryStream"/>s to partially access an
	/// unmanaged memory range
	/// </summary>
	/// <seealso cref="MemoryStreamCreator"/>
	[DebuggerDisplay("mem: D:{data} L:{dataLength} {theFileName}")]
	class UnmanagedMemoryStreamCreator : IImageStreamCreator {
		/// <summary>
		/// Address of data
		/// </summary>
		protected IntPtr data;

		/// <summary>
		/// Length of data
		/// </summary>
		protected long dataLength;

		/// <summary>
		/// Name of file
		/// </summary>
		protected string theFileName;

		/// <summary>
		/// The file name
		/// </summary>
		public string FileName {
			get { return theFileName; }
			set { theFileName = value; }
		}

		/// <summary>
		/// Size of the data
		/// </summary>
		public long Length {
			get { return dataLength; }
			set { dataLength = value; }
		}

		/// <summary>
		/// Returns the base address of the data
		/// </summary>
		public IntPtr Address {
			get { return data; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		protected UnmanagedMemoryStreamCreator() {
		}

		/// <summary>
		/// Constructor for 0 bytes of data
		/// </summary>
		/// <param name="data">Pointer to the data</param>
		public UnmanagedMemoryStreamCreator(IntPtr data)
			: this(data, 0) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">Pointer to the data</param>
		/// <param name="dataLength">Length of data</param>
		/// <exception cref="ArgumentOutOfRangeException">If one of the args is invalid</exception>
		public UnmanagedMemoryStreamCreator(IntPtr data, long dataLength) {
			if (dataLength < 0)
				throw new ArgumentOutOfRangeException("dataLength");
			this.data = data;
			this.dataLength = dataLength;
		}

		/// <inheritdoc/>
		public unsafe IImageStream Create(FileOffset offset, long length) {
			if (offset < 0 || length < 0)
				return MemoryImageStream.CreateEmpty();

			long offs = Math.Min((long)dataLength, (long)offset);
			long len = Math.Min((long)dataLength - offs, length);
			return new UnmanagedMemoryImageStream(offset, (byte*)data.ToPointer() + (long)offs, len);
		}

		/// <inheritdoc/>
		public IImageStream CreateFull() {
			return new UnmanagedMemoryImageStream(0, data, dataLength);
		}

		/// <inheritdoc/>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose method
		/// </summary>
		/// <param name="disposing"><c>true</c> if called by <see cref="Dispose()"/></param>
		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				data = IntPtr.Zero;
				dataLength = 0;
				theFileName = null;
			}
		}
	}
}
