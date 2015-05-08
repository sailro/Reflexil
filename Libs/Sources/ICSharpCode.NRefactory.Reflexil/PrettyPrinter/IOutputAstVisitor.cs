﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.NRefactory.Ast;

namespace ICSharpCode.NRefactory.PrettyPrinter
{
	/// <summary>
	/// Description of IOutputASTVisitor.
	/// </summary>
	public interface IOutputAstVisitor : IAstVisitor
	{
		event Action<INode> BeforeNodeVisit;
		event Action<INode> AfterNodeVisit;
		
		string Text {
			get;
		}
		
		Errors Errors {
			get;
		}
		
		AbstractPrettyPrintOptions Options {
			get;
		}
		IOutputFormatter OutputFormatter {
			get;
		}
	}
	public interface IOutputFormatter
	{
		int IndentationLevel {
			get;
			set;
		}
		string Text {
			get;
		}
		bool IsInMemberBody {
			get;
			set;
		}
		void NewLine();
		void Indent();
		void PrintComment(Comment comment, bool forceWriteInPreviousBlock);
		void PrintPreprocessingDirective(PreprocessingDirective directive, bool forceWriteInPreviousBlock);
		void PrintBlankLine(bool forceWriteInPreviousBlock);
	}
}
