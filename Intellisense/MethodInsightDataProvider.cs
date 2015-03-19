/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

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
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.InsightWindow;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Dom.CSharp;
using ICSharpCode.SharpDevelop.Dom.VBNet;
using ICSharpCode.SharpDevelop.Dom.NRefactoryResolver;
using Reflexil.Forms;
using Reflexil.Compilation;

#endregion

namespace Reflexil.Intellisense
{
	internal class MethodInsightDataProvider : IInsightDataProvider
	{
		#region Fields

		private TextArea _textArea;
		private int _defaultIndex = -1;
		private string[] _insighText;
		private readonly IntellisenseForm _iForm;

		#endregion

		#region Properties

		public int DefaultIndex
		{
			get { return _defaultIndex; }
			set { _defaultIndex = value; }
		}

		protected int ArgumentStartOffset { get; private set; }

		public int InsightDataCount
		{
			get { return (_insighText != null) ? _insighText.Length : 0; }
		}

		#endregion

		#region Methods

		public string GetInsightData(int number)
		{
			return (_insighText != null) ? _insighText[number] : string.Empty;
		}

		public virtual bool CaretOffsetChanged()
		{
			var document = _textArea.Document;
			var closeDataProvider = _textArea.Caret.Offset <= ArgumentStartOffset;
			var brackets = 0;
			var curlyBrackets = 0;
			if (!closeDataProvider)
			{
				var insideChar = false;
				var insideString = false;
				for (var offset = ArgumentStartOffset; offset < Math.Min(_textArea.Caret.Offset, document.TextLength); ++offset)
				{
					var ch = document.GetCharAt(offset);
					switch (ch)
					{
						case '\'':
							insideChar = !insideChar;
							break;
						case '(':
							if (!(insideChar || insideString))
							{
								++brackets;
							}
							break;
						case ')':
							if (!(insideChar || insideString))
							{
								--brackets;
							}
							if (brackets <= 0)
							{
								return true;
							}
							break;
						case '"':
							insideString = !insideString;
							break;
						case '}':
							if (!(insideChar || insideString))
							{
								--curlyBrackets;
							}
							if (curlyBrackets < 0)
							{
								return true;
							}
							break;
						case '{':
							if (!(insideChar || insideString))
							{
								++curlyBrackets;
							}
							break;
						case ';':
							if (!(insideChar || insideString))
							{
								return true;
							}
							break;
					}
				}
			}
			return closeDataProvider;
		}

		public void SetupDataProvider(string fileName, TextArea textArea)
		{
			_textArea = textArea;
			SetupDataProvider(fileName);
		}


		public void SetupDataProvider(string fileName)
		{
			IExpressionFinder expressionFinder;
			if (IntellisenseForm.SupportedLanguage == SupportedLanguage.VisualBasic)
			{
				expressionFinder = new VBExpressionFinder();
			}
			else
			{
				expressionFinder = new CSharpExpressionFinder(_iForm.ParseInformation);
			}

			//TextLocation position = _textArea.Caret.Position;
			//ExpressionResult expression = expressionFinder.FindFullExpression(_textArea.MotherTextEditorControl.Text, _textArea.Document.PositionToOffset(position));
			//if (expression.Region.IsEmpty)
			//{
			//    expression.Region = new DomRegion(position.Line + 1, position.Column + 1);
			//}
			var expression = expressionFinder.FindFullExpression(
				_textArea.MotherTextEditorControl.Text,
				_textArea.MotherTextEditorControl.Document.PositionToOffset(_textArea.Caret.Position) - 1);
			if (expression.Region.IsEmpty)
			{
				expression.Region = new DomRegion(_textArea.Caret.Position.Line + 1, _textArea.Caret.Position.Column + 1);
			}


			var resolver = new NRefactoryResolver(_iForm.ProjectContent.Language);
			var rr = resolver.Resolve(expression, _iForm.ParseInformation, _textArea.MotherTextEditorControl.Text);

			var lines = new List<string>();
			if (rr is MethodGroupResolveResult)
			{
				var mrr = rr as MethodGroupResolveResult;
				var ambience = IntellisenseForm.SupportedLanguage == SupportedLanguage.VisualBasic
					? (IAmbience) new VBNetAmbience()
					: new CSharpAmbience();
				ambience.ConversionFlags = ConversionFlags.StandardConversionFlags | ConversionFlags.ShowAccessibility;

				lines.AddRange(mrr.Methods.SelectMany(methodgroup => methodgroup,
					(methodgroup, method) => ToolTipProvider.GetMemberText(ambience, method)));
			}

			_insighText = (lines.Count > 0) ? lines.ToArray() : null;
			ArgumentStartOffset = _textArea.Caret.Offset;
		}

		public MethodInsightDataProvider(IntellisenseForm iForm)
		{
			_iForm = iForm;
		}

		#endregion
	}
}