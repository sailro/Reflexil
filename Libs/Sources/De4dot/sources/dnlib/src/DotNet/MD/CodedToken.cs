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

namespace dnlib.DotNet.MD {
	/// <summary>
	/// Contains all possible coded token classes
	/// </summary>
	public sealed class CodedToken {
		/// <summary>TypeDefOrRef coded token</summary>
		public static readonly CodedToken TypeDefOrRef = new CodedToken(2, new Table[3] {
			Table.TypeDef, Table.TypeRef, Table.TypeSpec,
		});

		/// <summary>HasConstant coded token</summary>
		public static readonly CodedToken HasConstant = new CodedToken(2, new Table[3] {
			Table.Field, Table.Param, Table.Property,
		});

		/// <summary>HasCustomAttribute coded token</summary>
		public static readonly CodedToken HasCustomAttribute = new CodedToken(5, new Table[24] {
			Table.Method, Table.Field, Table.TypeRef, Table.TypeDef,
			Table.Param, Table.InterfaceImpl, Table.MemberRef, Table.Module,
			Table.DeclSecurity, Table.Property, Table.Event, Table.StandAloneSig,
			Table.ModuleRef, Table.TypeSpec, Table.Assembly, Table.AssemblyRef,
			Table.File, Table.ExportedType, Table.ManifestResource, Table.GenericParam,
			Table.GenericParamConstraint, Table.MethodSpec, 0, 0,
		});

		/// <summary>HasFieldMarshal coded token</summary>
		public static readonly CodedToken HasFieldMarshal = new CodedToken(1, new Table[2] {
			Table.Field, Table.Param,
		});

		/// <summary>HasDeclSecurity coded token</summary>
		public static readonly CodedToken HasDeclSecurity = new CodedToken(2, new Table[3] {
			Table.TypeDef, Table.Method, Table.Assembly,
		});

		/// <summary>MemberRefParent coded token</summary>
		public static readonly CodedToken MemberRefParent = new CodedToken(3, new Table[5] {
			Table.TypeDef, Table.TypeRef, Table.ModuleRef, Table.Method,
			Table.TypeSpec,
		});

		/// <summary>HasSemantic coded token</summary>
		public static readonly CodedToken HasSemantic = new CodedToken(1, new Table[2] {
			Table.Event, Table.Property,
		});

		/// <summary>MethodDefOrRef coded token</summary>
		public static readonly CodedToken MethodDefOrRef = new CodedToken(1, new Table[2] {
			Table.Method, Table.MemberRef,
		});

		/// <summary>MemberForwarded coded token</summary>
		public static readonly CodedToken MemberForwarded = new CodedToken(1, new Table[2] {
			Table.Field, Table.Method,
		});

		/// <summary>Implementation coded token</summary>
		public static readonly CodedToken Implementation = new CodedToken(2, new Table[3] {
			Table.File, Table.AssemblyRef, Table.ExportedType,
		});

		/// <summary>CustomAttributeType coded token</summary>
		public static readonly CodedToken CustomAttributeType = new CodedToken(3, new Table[4] {
			0, 0, Table.Method, Table.MemberRef,
		});

		/// <summary>ResolutionScope coded token</summary>
		public static readonly CodedToken ResolutionScope = new CodedToken(2, new Table[4] {
			Table.Module, Table.ModuleRef, Table.AssemblyRef, Table.TypeRef,
		});

		/// <summary>TypeOrMethodDef coded token</summary>
		public static readonly CodedToken TypeOrMethodDef = new CodedToken(1, new Table[2] {
			Table.TypeDef, Table.Method,
		});

		readonly Table[] tableTypes;
		readonly int bits;
		readonly int mask;

		/// <summary>
		/// Returns all types of tables
		/// </summary>
		public Table[] TableTypes {
			get { return tableTypes; }
		}

		/// <summary>
		/// Returns the number of bits that is used to encode table type
		/// </summary>
		public int Bits {
			get { return bits; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bits">Number of bits used to encode token type</param>
		/// <param name="tableTypes">All table types</param>
		internal CodedToken(int bits, Table[] tableTypes) {
			this.bits = bits;
			this.mask = (1 << bits) - 1;
			this.tableTypes = tableTypes;
		}

		/// <summary>
		/// Encodes a token
		/// </summary>
		/// <param name="token">The token</param>
		/// <returns>Coded token</returns>
		/// <seealso cref="Encode(MDToken,out uint)"/>
		public uint Encode(MDToken token) {
			return Encode(token.Raw);
		}

		/// <summary>
		/// Encodes a token
		/// </summary>
		/// <param name="token">The token</param>
		/// <returns>Coded token</returns>
		/// <seealso cref="Encode(uint,out uint)"/>
		public uint Encode(uint token) {
			uint codedToken;
			Encode(token, out codedToken);
			return codedToken;
		}

		/// <summary>
		/// Encodes a token
		/// </summary>
		/// <param name="token">The token</param>
		/// <param name="codedToken">Coded token</param>
		/// <returns><c>true</c> if successful</returns>
		public bool Encode(MDToken token, out uint codedToken) {
			return Encode(token.Raw, out codedToken);
		}

		/// <summary>
		/// Encodes a token
		/// </summary>
		/// <param name="token">The token</param>
		/// <param name="codedToken">Coded token</param>
		/// <returns><c>true</c> if successful</returns>
		public bool Encode(uint token, out uint codedToken) {
			int index = Array.IndexOf(tableTypes, MDToken.ToTable(token));
			if (index < 0) {
				codedToken = uint.MaxValue;
				return false;
			}
			// This shift can never overflow a uint since bits < 8 (it's at most 5), and
			// ToRid() returns an integer <= 0x00FFFFFF.
			codedToken = (MDToken.ToRID(token) << bits) | (uint)index;
			return true;
		}

		/// <summary>
		/// Decodes a coded token
		/// </summary>
		/// <param name="codedToken">The coded token</param>
		/// <returns>Decoded token or 0 on failure</returns>
		/// <seealso cref="Decode(uint,out MDToken)"/>
		public MDToken Decode2(uint codedToken) {
			uint token;
			Decode(codedToken, out token);
			return new MDToken(token);
		}

		/// <summary>
		/// Decodes a coded token
		/// </summary>
		/// <param name="codedToken">The coded token</param>
		/// <returns>Decoded token or 0 on failure</returns>
		/// <seealso cref="Decode(uint,out uint)"/>
		public uint Decode(uint codedToken) {
			uint token;
			Decode(codedToken, out token);
			return token;
		}

		/// <summary>
		/// Decodes a coded token
		/// </summary>
		/// <param name="codedToken">The coded token</param>
		/// <param name="token">Decoded token</param>
		/// <returns><c>true</c> if successful</returns>
		public bool Decode(uint codedToken, out MDToken token) {
			uint decodedToken;
			bool result = Decode(codedToken, out decodedToken);
			token = new MDToken(decodedToken);
			return result;
		}

		/// <summary>
		/// Decodes a coded token
		/// </summary>
		/// <param name="codedToken">The coded token</param>
		/// <param name="token">Decoded token</param>
		/// <returns><c>true</c> if successful</returns>
		public bool Decode(uint codedToken, out uint token) {
			uint rid = codedToken >> bits;
			int index = (int)(codedToken & mask);
			if (rid > MDToken.RID_MAX || index >= tableTypes.Length) {
				token = 0;
				return false;
			}

			token = ((uint)tableTypes[index] << MDToken.TABLE_SHIFT) | rid;
			return true;
		}
	}
}
