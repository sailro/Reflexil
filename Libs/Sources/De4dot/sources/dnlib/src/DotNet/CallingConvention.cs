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
using System.Text;

namespace dnlib.DotNet {
	/// <summary>
	/// See CorHdr.h/CorCallingConvention
	/// </summary>
	[Flags, DebuggerDisplay("{Extensions.ToString(this),nq}")]
	public enum CallingConvention : byte {
		/// <summary>The managed calling convention</summary>
		Default			= 0x0,
		/// <summary/>
		C				= 0x1,
		/// <summary/>
		StdCall			= 0x2,
		/// <summary/>
		ThisCall		= 0x3,
		/// <summary/>
		FastCall		= 0x4,
		/// <summary/>
		VarArg			= 0x5,
		/// <summary/>
		Field			= 0x6,
		/// <summary/>
		LocalSig		= 0x7,
		/// <summary/>
		Property		= 0x8,
		/// <summary/>
		Unmanaged		= 0x9,
		/// <summary>generic method instantiation</summary>
		GenericInst		= 0xA,
		/// <summary>used ONLY for 64bit vararg PInvoke calls</summary>
		NativeVarArg	= 0xB,

		/// <summary>Calling convention is bottom 4 bits</summary>
		Mask			= 0x0F,

		/// <summary>Generic method</summary>
		Generic			= 0x10,
		/// <summary>Method needs a 'this' parameter</summary>
		HasThis			= 0x20,
		/// <summary>'this' parameter is the first arg if set (else it's hidden)</summary>
		ExplicitThis	= 0x40,
		/// <summary>Used internally by the CLR</summary>
		ReservedByCLR	= 0x80,
	}

	public static partial class Extensions {
		internal static string ToString(CallingConvention flags) {
			var sb = new StringBuilder();

			switch (flags & CallingConvention.Mask) {
			case CallingConvention.Default: sb.Append("Default"); break;
			case CallingConvention.C: sb.Append("C"); break;
			case CallingConvention.StdCall: sb.Append("StdCall"); break;
			case CallingConvention.ThisCall: sb.Append("ThisCall"); break;
			case CallingConvention.FastCall: sb.Append("FastCall"); break;
			case CallingConvention.VarArg: sb.Append("VarArg"); break;
			case CallingConvention.Field: sb.Append("Field"); break;
			case CallingConvention.LocalSig: sb.Append("LocalSig"); break;
			case CallingConvention.Property: sb.Append("Property"); break;
			case CallingConvention.Unmanaged: sb.Append("Unmanaged"); break;
			case CallingConvention.GenericInst: sb.Append("GenericInst"); break;
			case CallingConvention.NativeVarArg: sb.Append("NativeVarArg"); break;
			default: sb.Append(string.Format("CC_UNKNOWN_0x{0:X}", (int)(flags & CallingConvention.Mask))); break;
			}

			if ((flags & CallingConvention.Generic) != 0)
				sb.Append(" | Generic");

			if ((flags & CallingConvention.HasThis) != 0)
				sb.Append(" | HasThis");

			if ((flags & CallingConvention.ExplicitThis) != 0)
				sb.Append(" | ExplicitThis");

			if ((flags & CallingConvention.ReservedByCLR) != 0)
				sb.Append(" | ReservedByCLR");

			return sb.ToString();
		}
	}
}
