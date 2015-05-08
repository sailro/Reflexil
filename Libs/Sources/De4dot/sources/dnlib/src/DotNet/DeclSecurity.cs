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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using dnlib.Utils;
using dnlib.DotNet.MD;
using dnlib.Threading;

#if THREAD_SAFE
using ThreadSafe = dnlib.Threading.Collections;
#else
using ThreadSafe = System.Collections.Generic;
#endif

namespace dnlib.DotNet {
	/// <summary>
	/// A high-level representation of a row in the DeclSecurity table
	/// </summary>
	[DebuggerDisplay("{Action} Count={SecurityAttributes.Count}")]
	public abstract class DeclSecurity : IHasCustomAttribute {
		/// <summary>
		/// The row id in its table
		/// </summary>
		protected uint rid;

		/// <inheritdoc/>
		public MDToken MDToken {
			get { return new MDToken(Table.DeclSecurity, rid); }
		}

		/// <inheritdoc/>
		public uint Rid {
			get { return rid; }
			set { rid = value; }
		}

		/// <inheritdoc/>
		public int HasCustomAttributeTag {
			get { return 8; }
		}

		/// <summary>
		/// From column DeclSecurity.Action
		/// </summary>
		public SecurityAction Action {
			get { return action; }
			set { action = value; }
		}
		/// <summary/>
		protected SecurityAction action;

		/// <summary>
		/// From column DeclSecurity.PermissionSet
		/// </summary>
		public ThreadSafe.IList<SecurityAttribute> SecurityAttributes {
			get {
				if (securityAttributes == null)
					InitializeSecurityAttributes();
				return securityAttributes;
			}
		}
		/// <summary/>
		protected ThreadSafe.IList<SecurityAttribute> securityAttributes;
		/// <summary>Initializes <see cref="securityAttributes"/></summary>
		protected virtual void InitializeSecurityAttributes() {
			Interlocked.CompareExchange(ref securityAttributes, ThreadSafeListCreator.Create<SecurityAttribute>(), null);
		}

		/// <summary>
		/// Gets all custom attributes
		/// </summary>
		public CustomAttributeCollection CustomAttributes {
			get {
				if (customAttributes == null)
					InitializeCustomAttributes();
				return customAttributes;
			}
		}
		/// <summary/>
		protected CustomAttributeCollection customAttributes;
		/// <summary>Initializes <see cref="customAttributes"/></summary>
		protected virtual void InitializeCustomAttributes() {
			Interlocked.CompareExchange(ref customAttributes, new CustomAttributeCollection(), null);
		}

		/// <inheritdoc/>
		public bool HasCustomAttributes {
			get { return CustomAttributes.Count > 0; }
		}

		/// <summary>
		/// <c>true</c> if <see cref="SecurityAttributes"/> is not empty
		/// </summary>
		public bool HasSecurityAttributes {
			get { return SecurityAttributes.Count > 0; }
		}

		/// <summary>
		/// Gets the blob data or <c>null</c> if there's none
		/// </summary>
		/// <returns>Blob data or <c>null</c></returns>
		public abstract byte[] GetBlob();
	}

	/// <summary>
	/// A DeclSecurity row created by the user and not present in the original .NET file
	/// </summary>
	public class DeclSecurityUser : DeclSecurity {
		/// <summary>
		/// Default constructor
		/// </summary>
		public DeclSecurityUser() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="action">The security action</param>
		/// <param name="securityAttrs">The security attributes (now owned by this)</param>
		public DeclSecurityUser(SecurityAction action, IList<SecurityAttribute> securityAttrs) {
			this.action = action;
			this.securityAttributes = ThreadSafeListCreator.MakeThreadSafe(securityAttrs);
		}

		/// <inheritdoc/>
		public override byte[] GetBlob() {
			return null;
		}
	}

	/// <summary>
	/// Created from a row in the DeclSecurity table
	/// </summary>
	sealed class DeclSecurityMD : DeclSecurity, IMDTokenProviderMD {
		/// <summary>The module where this instance is located</summary>
		readonly ModuleDefMD readerModule;

		readonly uint origRid;
		readonly uint permissionSet;

		/// <inheritdoc/>
		public uint OrigRid {
			get { return origRid; }
		}

		/// <inheritdoc/>
		protected override void InitializeSecurityAttributes() {
			var gpContext = new GenericParamContext();
			var tmp = DeclSecurityReader.Read(readerModule, permissionSet, gpContext);
			Interlocked.CompareExchange(ref securityAttributes, tmp, null);
		}

		/// <inheritdoc/>
		protected override void InitializeCustomAttributes() {
			var list = readerModule.MetaData.GetCustomAttributeRidList(Table.DeclSecurity, origRid);
			var tmp = new CustomAttributeCollection((int)list.Length, list, (list2, index) => readerModule.ReadCustomAttribute(((RidList)list2)[index]));
			Interlocked.CompareExchange(ref customAttributes, tmp, null);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="readerModule">The module which contains this <c>DeclSecurity</c> row</param>
		/// <param name="rid">Row ID</param>
		/// <exception cref="ArgumentNullException">If <paramref name="readerModule"/> is <c>null</c></exception>
		/// <exception cref="ArgumentException">If <paramref name="rid"/> is invalid</exception>
		public DeclSecurityMD(ModuleDefMD readerModule, uint rid) {
#if DEBUG
			if (readerModule == null)
				throw new ArgumentNullException("readerModule");
			if (readerModule.TablesStream.DeclSecurityTable.IsInvalidRID(rid))
				throw new BadImageFormatException(string.Format("DeclSecurity rid {0} does not exist", rid));
#endif
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			this.permissionSet = readerModule.TablesStream.ReadDeclSecurityRow(origRid, out this.action);
		}

		/// <inheritdoc/>
		public override byte[] GetBlob() {
			return readerModule.BlobStream.Read(permissionSet);
		}
	}
}
