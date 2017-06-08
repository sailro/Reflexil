// CSharp Editor Example with Code Completion
// Copyright (c) 2007, Daniel Grunwald
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
// 
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
// 
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
// 
// - Neither the name of the ICSharpCode team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Text;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Dom.CSharp;
using ICSharpCode.SharpDevelop.Dom.VBNet;
using TextEditor = ICSharpCode.TextEditor;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;
using Reflexil.Forms;
using Reflexil.Compilation;

namespace Reflexil.Intellisense
{
	internal sealed class ToolTipProvider
	{
		private readonly IntellisenseForm _iForm;
		private readonly TextEditor.TextEditorControl _editor;

		private ToolTipProvider(IntellisenseForm iForm, TextEditor.TextEditorControl editor)
		{
			_iForm = iForm;
			_editor = editor;
		}

		public static void Attach(IntellisenseForm iForm, TextEditor.TextEditorControl editor)
		{
			var tp = new ToolTipProvider(iForm, editor);
			editor.ActiveTextAreaControl.TextArea.ToolTipRequest += tp.OnToolTipRequest;
		}

		private void OnToolTipRequest(object sender, TextEditor.ToolTipRequestEventArgs e)
		{
			if (!e.InDocument || e.ToolTipShown)
				return;

			IExpressionFinder expressionFinder;
			if (IntellisenseForm.SupportedLanguage == SupportedLanguage.VisualBasic)
			{
				expressionFinder = new VBExpressionFinder();
			}
			else
			{
				expressionFinder = new CSharpExpressionFinder(_iForm.ParseInformation);
			}

			var expression = expressionFinder.FindFullExpression(
				_editor.Text,
				_editor.Document.PositionToOffset(e.LogicalPosition));
			if (expression.Region.IsEmpty)
			{
				expression.Region = new DomRegion(e.LogicalPosition.Line + 1, e.LogicalPosition.Column + 1);
			}

			var textArea = _editor.ActiveTextAreaControl.TextArea;
			var resolver = new NRefactoryResolver(_iForm.ProjectContent.Language);
			var rr = resolver.Resolve(expression,
				_iForm.ParseInformation,
				textArea.MotherTextEditorControl.Text);

			var toolTipText = GetText(rr);
			if (toolTipText != null)
			{
				e.ShowToolTip(toolTipText);
			}
		}

		private static string GetText(ResolveResult result)
		{
			if (result == null)
			{
				return null;
			}

			var resolveResult = result as MixedResolveResult;
			if (resolveResult != null)
				return GetText(resolveResult.PrimaryResult);

			var ambience = IntellisenseForm.SupportedLanguage == SupportedLanguage.VisualBasic
				? (IAmbience) new VBNetAmbience()
				: new CSharpAmbience();
			ambience.ConversionFlags = ConversionFlags.StandardConversionFlags | ConversionFlags.ShowAccessibility;

			var memberResolveResult = result as MemberResolveResult;
			if (memberResolveResult != null)
			{
				return GetMemberText(ambience, memberResolveResult.ResolvedMember);
			}

			var localResolveResult = result as LocalResolveResult;
			if (localResolveResult != null)
			{
				var rr = localResolveResult;
				ambience.ConversionFlags = ConversionFlags.UseFullyQualifiedTypeNames
				                           | ConversionFlags.ShowReturnType;
				var builder = new StringBuilder();
				builder.Append(rr.IsParameter ? "parameter " : "local variable ");
				builder.Append(ambience.Convert(rr.Field));
				return builder.ToString();
			}

			var namespaceResolveResult = result as NamespaceResolveResult;
			if (namespaceResolveResult != null)
			{
				return "namespace " + namespaceResolveResult.Name;
			}

			var typeResolveResult = result as TypeResolveResult;
			if (typeResolveResult != null)
			{
				var @class = typeResolveResult.ResolvedClass;
				return @class != null ? GetMemberText(ambience, @class) : ambience.Convert(typeResolveResult.ResolvedType);
			}

			var methodGroupResolveResult = result as MethodGroupResolveResult;
			if (methodGroupResolveResult != null)
			{
				var mrr = methodGroupResolveResult;
				var m = mrr.GetMethodIfSingleOverload();
				if (m != null)
					return GetMemberText(ambience, m);

				return "Overload of " + ambience.Convert(mrr.ContainingType) + "." + mrr.Name;
			}

			return null;
		}

		internal static string GetMemberText(IAmbience ambience, IEntity member)
		{
			var text = new StringBuilder();
			// ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
			if (member is IField)
			{
				text.Append(ambience.Convert((IField) member));
			}
			else if (member is IProperty)
			{
				text.Append(ambience.Convert((IProperty) member));
			}
			else if (member is IEvent)
			{
				text.Append(ambience.Convert((IEvent) member));
			}
			else if (member is IMethod)
			{
				text.Append(ambience.Convert((IMethod) member));
			}
			else if (member is IClass)
			{
				text.Append(ambience.Convert((IClass) member));
			}
			else
			{
				text.Append("unknown member ");
				text.Append(member);
			}
			var documentation = member.Documentation;
			if (string.IsNullOrEmpty(documentation))
				return text.ToString();

			text.Append('\n');
			text.Append(CodeCompletionData.XmlDocumentationToText(documentation));
			return text.ToString();
		}
	}
}