// CSharp Editor Example with Code Completion
// Copyright (c) 2006, Daniel Grunwald
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

using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

using Dom = ICSharpCode.SharpDevelop.Dom;
using NRefactoryResolver = ICSharpCode.SharpDevelop.Dom.NRefactoryResolver.NRefactoryResolver;

using Reflexil.Forms;
using Reflexil.Compilation;

namespace Reflexil.Intellisense
{
	class CodeCompletionProvider : ICompletionDataProvider
	{
		readonly IntellisenseForm _iForm;
		
		public CodeCompletionProvider(IntellisenseForm iForm)
		{
			_iForm = iForm;
		}
		
		public ImageList ImageList {
			get {
				return _iForm.ImageList;
			}
		}
		
		public string PreSelection {
			get {
				return null;
			}
		}
		
		public int DefaultIndex {
			get {
				return -1;
			}
		}
		
		public CompletionDataProviderKeyResult ProcessKey(char key)
		{
			if (char.IsLetterOrDigit(key) || key == '_') {
				return CompletionDataProviderKeyResult.NormalKey;
			}

			// key triggers insertion of selected items
			return CompletionDataProviderKeyResult.InsertionKey;
		}
		
		/// <summary>
		/// Called when entry should be inserted. Forward to the insertion action of the completion data.
		/// </summary>
		public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
		{
			textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
			return data.InsertAction(textArea, key);
		}
		
		public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
		{
			// We can return code-completion items like this:
			
			//return new ICompletionData[] {
			//	new DefaultCompletionData("Text", "Description", 1)
			//};
			
			var resolver = new NRefactoryResolver(_iForm.ProjectContent.Language);
			var rr = resolver.Resolve(FindExpression(textArea),
			                                        _iForm.ParseInformation,
			                                        textArea.MotherTextEditorControl.Text);
			var resultList = new List<ICompletionData>();
			if (rr != null) {
				var completionData = rr.GetCompletionData(_iForm.ProjectContent);
				if (completionData != null) {
					AddCompletionData(resultList, completionData);
				}
			}
			return resultList.ToArray();
		}
		
		/// <summary>
		/// Find the expression the cursor is at.
		/// Also determines the context (using statement, "new"-expression etc.) the
		/// cursor is at.
		/// </summary>
		Dom.ExpressionResult FindExpression(TextArea textArea)
		{
			Dom.IExpressionFinder finder;
			if (IntellisenseForm.SupportedLanguage == SupportedLanguage.VisualBasic) {
				finder = new Dom.VBNet.VBExpressionFinder();
			} else {
				finder = new Dom.CSharp.CSharpExpressionFinder(_iForm.ParseInformation);
			}

			var expression = finder.FindExpression(textArea.Document.TextContent, textArea.Caret.Offset);
			if (expression.Region.IsEmpty) {
				expression.Region = new Dom.DomRegion(textArea.Caret.Line + 1, textArea.Caret.Column + 1);
			}
			return expression;
		}

		static void AddCompletionData(List<ICompletionData> resultList, IEnumerable<Dom.ICompletionEntry> completionData)
		{
			// used to store the method names for grouping overloads
			var nameDictionary = new Dictionary<string, CodeCompletionData>();
			
			// Add the completion data as returned by SharpDevelop.Dom to the
			// list for the text editor
			foreach (var entry in completionData)
			{
				var @class = entry as Dom.IClass;
				if (@class != null)
				{
					resultList.Add(new CodeCompletionData(@class));
				} else
				{
					var member = entry as Dom.IMember;
					if (member == null) 
						continue;

					if (member is Dom.IMethod && ((member as Dom.IMethod).IsConstructor)) {
						// Skip constructors
						continue;
					}
					// Group results by name and add "(x Overloads)" to the
					// description if there are multiple results with the same name.
					
					CodeCompletionData data;
					if (nameDictionary.TryGetValue(member.Name, out data)) {
						data.AddOverload();
					} else {
						nameDictionary[member.Name] = data = new CodeCompletionData(member);
						resultList.Add(data);
					}
				}
			}
		}

	}
}
