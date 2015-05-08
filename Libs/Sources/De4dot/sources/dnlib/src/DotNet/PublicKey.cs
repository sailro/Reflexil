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

using dnlib.Threading;

﻿namespace dnlib.DotNet {
	/// <summary>
	/// Represents a public key
	/// </summary>
	public sealed class PublicKey : PublicKeyBase {
		const AssemblyHashAlgorithm DEFAULT_ALGORITHM = AssemblyHashAlgorithm.SHA1;
		PublicKeyToken publicKeyToken;
#if THREAD_SAFE
		readonly Lock theLock = Lock.Create();
#endif

		/// <summary>
		/// Gets the <see cref="PublicKeyToken"/>
		/// </summary>
		public override PublicKeyToken Token {
			get {
#if THREAD_SAFE
				theLock.EnterWriteLock(); try {
#endif
				if (publicKeyToken == null && !IsNullOrEmpty_NoLock)
					publicKeyToken = AssemblyHash.CreatePublicKeyToken(data);
				return publicKeyToken;
#if THREAD_SAFE
				} finally { theLock.ExitWriteLock(); }
#endif
			}
		}

		/// <inheritdoc/>
		public override byte[] Data {
			get {
#if THREAD_SAFE
				theLock.EnterReadLock(); try {
#endif
				return data;
#if THREAD_SAFE
				} finally { theLock.ExitReadLock(); }
#endif
			}
			set {
#if THREAD_SAFE
				theLock.EnterWriteLock(); try {
#endif
				if (data == value)
					return;
				data = value;
				publicKeyToken = null;
#if THREAD_SAFE
				} finally { theLock.ExitWriteLock(); }
#endif
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public PublicKey() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">Public key data</param>
		public PublicKey(byte[] data)
			: base(data) {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="hexString">Public key data as a hex string or the string <c>"null"</c>
		/// to set public key data to <c>null</c></param>
		public PublicKey(string hexString)
			: base(hexString) {
		}

		/// <inheritdoc/>
		public override bool Equals(object obj) {
			if ((object)this == obj)
				return true;
			var other = obj as PublicKey;
			if (other == null)
				return false;
			return Utils.Equals(Data, other.Data);
		}

		/// <inheritdoc/>
		public override int GetHashCode() {
			return Utils.GetHashCode(Data);
		}
	}
}
