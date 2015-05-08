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
using System.Threading;
using dnlib.Utils;
using dnlib.DotNet.MD;

namespace dnlib.DotNet {
	/// <summary>
	/// A high-level representation of a row in the StandAloneSig table
	/// </summary>
	public abstract class StandAloneSig : IHasCustomAttribute {
		/// <summary>
		/// The row id in its table
		/// </summary>
		protected uint rid;

		/// <inheritdoc/>
		public MDToken MDToken {
			get { return new MDToken(Table.StandAloneSig, rid); }
		}

		/// <inheritdoc/>
		public uint Rid {
			get { return rid; }
			set { rid = value; }
		}

		/// <inheritdoc/>
		public int HasCustomAttributeTag {
			get { return 11; }
		}

		/// <summary>
		/// From column StandAloneSig.Signature
		/// </summary>
		public CallingConventionSig Signature {
			get { return signature; }
			set { signature = value; }
		}
		/// <summary/>
		protected CallingConventionSig signature;

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
		/// Gets/sets the method sig
		/// </summary>
		public MethodSig MethodSig {
			get { return signature as MethodSig; }
			set { signature = value; }
		}

		/// <summary>
		/// Gets/sets the locals sig
		/// </summary>
		public LocalSig LocalSig {
			get { return signature as LocalSig; }
			set { signature = value; }
		}
	}

	/// <summary>
	/// A StandAloneSig row created by the user and not present in the original .NET file
	/// </summary>
	public class StandAloneSigUser : StandAloneSig {
		/// <summary>
		/// Default constructor
		/// </summary>
		public StandAloneSigUser() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="localSig">A locals sig</param>
		public StandAloneSigUser(LocalSig localSig) {
			this.signature = localSig;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="methodSig">A method sig</param>
		public StandAloneSigUser(MethodSig methodSig) {
			this.signature = methodSig;
		}
	}

	/// <summary>
	/// Created from a row in the StandAloneSig table
	/// </summary>
	sealed class StandAloneSigMD : StandAloneSig, IMDTokenProviderMD, IContainsGenericParameter {
		/// <summary>The module where this instance is located</summary>
		readonly ModuleDefMD readerModule;

		readonly uint origRid;

		/// <inheritdoc/>
		public uint OrigRid {
			get { return origRid; }
		}

		/// <inheritdoc/>
		protected override void InitializeCustomAttributes() {
			var list = readerModule.MetaData.GetCustomAttributeRidList(Table.StandAloneSig, origRid);
			var tmp = new CustomAttributeCollection((int)list.Length, list, (list2, index) => readerModule.ReadCustomAttribute(((RidList)list2)[index]));
			Interlocked.CompareExchange(ref customAttributes, tmp, null);
		}

		public bool ContainsGenericParameter {
			get { return TypeHelper.ContainsGenericParameter(this); }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="readerModule">The module which contains this <c>StandAloneSig</c> row</param>
		/// <param name="rid">Row ID</param>
		/// <param name="gpContext">Generic parameter context</param>
		/// <exception cref="ArgumentNullException">If <paramref name="readerModule"/> is <c>null</c></exception>
		/// <exception cref="ArgumentException">If <paramref name="rid"/> is invalid</exception>
		public StandAloneSigMD(ModuleDefMD readerModule, uint rid, GenericParamContext gpContext) {
#if DEBUG
			if (readerModule == null)
				throw new ArgumentNullException("readerModule");
			if (readerModule.TablesStream.StandAloneSigTable.IsInvalidRID(rid))
				throw new BadImageFormatException(string.Format("StandAloneSig rid {0} does not exist", rid));
#endif
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			uint signature = readerModule.TablesStream.ReadStandAloneSigRow2(origRid);
			this.signature = readerModule.ReadSignature(signature, gpContext);
		}
	}
}
