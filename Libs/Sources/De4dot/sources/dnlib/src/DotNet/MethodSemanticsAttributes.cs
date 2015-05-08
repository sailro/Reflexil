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

namespace dnlib.DotNet {
	/// <summary>
	/// Method semantics flags, see CorHdr.h/CorMethodSemanticsAttr
	/// </summary>
	[Flags]
	public enum MethodSemanticsAttributes : ushort {
		/// <summary>Setter for property</summary>
		Setter		= 0x0001,
		/// <summary>Getter for property</summary>
		Getter		= 0x0002,
		/// <summary>other method for property or event</summary>
		Other		= 0x0004,
		/// <summary>AddOn method for event</summary>
		AddOn		= 0x0008,
		/// <summary>RemoveOn method for event</summary>
		RemoveOn	= 0x0010,
		/// <summary>Fire method for event</summary>
		Fire		= 0x0020,
	}
}
