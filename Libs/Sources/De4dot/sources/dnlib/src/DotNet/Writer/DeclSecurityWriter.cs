﻿/*
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

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dnlib.DotNet.Writer {
	/// <summary>
	/// Writes <c>DeclSecurity</c> blobs
	/// </summary>
	public struct DeclSecurityWriter : ICustomAttributeWriterHelper {
		readonly ModuleDef module;
		readonly IWriterError helper;

		/// <summary>
		/// Creates a <c>DeclSecurity</c> blob from <paramref name="secAttrs"/>
		/// </summary>
		/// <param name="module">Owner module</param>
		/// <param name="secAttrs">List of <see cref="SecurityAttribute"/>s</param>
		/// <param name="helper">Helps this class</param>
		/// <returns>A <c>DeclSecurity</c> blob</returns>
		public static byte[] Write(ModuleDef module, IList<SecurityAttribute> secAttrs, IWriterError helper) {
			return new DeclSecurityWriter(module, helper).Write(secAttrs);
		}

		DeclSecurityWriter(ModuleDef module, IWriterError helper) {
			this.module = module;
			this.helper = helper;
		}

		byte[] Write(IList<SecurityAttribute> secAttrs) {
			if (secAttrs == null)
				secAttrs = new SecurityAttribute[0];

			var xml = GetNet1xXmlString(secAttrs);
			if (xml != null)
				return WriteFormat1(xml);
			return WriteFormat2(secAttrs);
		}

		string GetNet1xXmlString(IList<SecurityAttribute> secAttrs) {
			if (secAttrs == null || secAttrs.Count != 1)
				return null;
			var sa = secAttrs[0];
			if (sa == null || sa.TypeFullName != "System.Security.Permissions.PermissionSetAttribute")
				return null;
			if (sa.NamedArguments.Count != 1)
				return null;
			var na = sa.NamedArguments[0];
			if (na == null || !na.IsProperty || na.Name != "XML")
				return null;
			if (na.ArgumentType.GetElementType() != ElementType.String)
				return null;
			var arg = na.Argument;
			if (arg.Type.GetElementType() != ElementType.String)
				return null;
			var utf8 = arg.Value as UTF8String;
			if ((object)utf8 != null)
				return utf8;
			var s = arg.Value as string;
			if (s != null)
				return s;
			return null;
		}

		byte[] WriteFormat1(string xml) {
			return Encoding.Unicode.GetBytes(xml);
		}

		byte[] WriteFormat2(IList<SecurityAttribute> secAttrs) {
			using (var stream = new MemoryStream())
			using (var writer = new BinaryWriter(stream)) {
				writer.Write((byte)'.');
				WriteCompressedUInt32(writer, (uint)secAttrs.Count);

				foreach (var sa in secAttrs) {
					if (sa == null) {
						helper.Error("SecurityAttribute is null");
						Write(writer, UTF8String.Empty);
						WriteCompressedUInt32(writer, 1);
						WriteCompressedUInt32(writer, 0);
						continue;
					}
					var attrType = sa.AttributeType;
					string fqn;
					if (attrType == null) {
						helper.Error("SecurityAttribute attribute type is null");
						fqn = string.Empty;
					}
					else
						fqn = attrType.AssemblyQualifiedName;
					Write(writer, fqn);

					var namedArgsBlob = CustomAttributeWriter.Write(this, sa.NamedArguments);
					if (namedArgsBlob.Length > 0x1FFFFFFF) {
						helper.Error("Named arguments blob is too big");
						namedArgsBlob = new byte[0];
					}
					WriteCompressedUInt32(writer, (uint)namedArgsBlob.Length);
					writer.Write(namedArgsBlob);
				}

				return stream.ToArray();
			}
		}

		uint WriteCompressedUInt32(BinaryWriter writer, uint value) {
			return writer.WriteCompressedUInt32(helper, value);
		}

		void Write(BinaryWriter writer, UTF8String s) {
			writer.Write(helper, s);
		}

		void IWriterError.Error(string message) {
			helper.Error(message);
		}

		bool IFullNameCreatorHelper.MustUseAssemblyName(IType type) {
			return FullNameCreator.MustUseAssemblyName(module, type);
		}
	}
}
