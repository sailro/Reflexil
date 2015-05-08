﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSharpCode.NRefactory.Parser.VB
{	
	public enum Context
	{
		Global,
		TypeDeclaration,
		ObjectCreation,
		ObjectInitializer,
		CollectionInitializer,
		Type,
		Member,
		Parameter,
		Identifier,
		Body,
		Xml,
		Attribute,
		Importable,
		Query,
		Expression,
		Debug,
		Default
	}
	
	public class Block : ICloneable
	{
		public static readonly Block Default = new Block() {
			context = Context.Global,
			lastExpressionStart = Location.Empty
		};
		
		public Context context;
		public Location lastExpressionStart;
		public bool isClosed;
		
		public override string ToString()
		{
			return string.Format("[Block Context={0}, LastExpressionStart={1}, IsClosed={2}]", context, lastExpressionStart, isClosed);
		}
		
		public object Clone()
		{
			return new Block() {
				context = this.context,
				lastExpressionStart = this.lastExpressionStart,
				isClosed = this.isClosed
			};
		}
	}
}
