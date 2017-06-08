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

using System;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using Reflexil.Forms;
using ICSharpCode.TextEditor.Gui.InsightWindow;

namespace Reflexil.Intellisense
{
	internal class CodeCompletionKeyHandler
	{
		private readonly IntellisenseForm _iForm;
		private readonly TextEditorControl _editor;
		private CodeCompletionWindow _codeCompletionWindow;
		private InsightWindow _insightWindow;

		private CodeCompletionKeyHandler(IntellisenseForm iForm, TextEditorControl editor)
		{
			_iForm = iForm;
			_editor = editor;
		}

		public static CodeCompletionKeyHandler Attach(IntellisenseForm iForm, TextEditorControl editor)
		{
			var handler = new CodeCompletionKeyHandler(iForm, editor);

			editor.ActiveTextAreaControl.TextArea.KeyEventHandler += handler.TextAreaKeyEventHandler;

			// When the editor is disposed, close the code completion window
			editor.Disposed += handler.CloseCodeCompletionWindow;

			return handler;
		}

		private bool TextAreaKeyEventHandler(char key)
		{
			if (_codeCompletionWindow != null)
			{
				// If completion window is open and wants to handle the key, don't let the text area
				// handle it
				if (_codeCompletionWindow.ProcessKeyEvent(key))
					return true;
			}

			if (key == '.')
			{
				ICompletionDataProvider completionDataProvider = new CodeCompletionProvider(_iForm);

				_codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(
					_iForm, // The parent window for the completion window
					_editor, // The text editor to show the window for
					IntellisenseForm.DummyFileName, // Filename - will be passed back to the provider
					completionDataProvider, // Provider to get the list of possible completions
					key // Key pressed - will be passed to the provider
					);
				if (_codeCompletionWindow != null)
				{
					// ShowCompletionWindow can return null when the provider returns an empty list
					_codeCompletionWindow.Closed += CloseCodeCompletionWindow;
				}
			}
			else if (key == '(')
			{
				if (_insightWindow != null && (!_insightWindow.IsDisposed))
				{
					// provider returned an empty list, so the window never been opened
					CloseInsightWindow(this, EventArgs.Empty);
				}
				IInsightDataProvider insightdataprovider = new MethodInsightDataProvider(_iForm);
				_insightWindow = new InsightWindow(_iForm, _editor);
				_insightWindow.Closed += CloseInsightWindow;
				_insightWindow.AddInsightDataProvider(insightdataprovider, IntellisenseForm.DummyFileName);
				_insightWindow.ShowInsightWindow();
			}
			return false;
		}

		private void CloseCodeCompletionWindow(object sender, EventArgs e)
		{
			if (_codeCompletionWindow == null)
				return;

			_codeCompletionWindow.Closed -= CloseCodeCompletionWindow;
			_codeCompletionWindow.Dispose();
			_codeCompletionWindow = null;
		}

		private void CloseInsightWindow(object sender, EventArgs e)
		{
			if (_insightWindow == null)
				return;

			_insightWindow.Closed -= CloseInsightWindow;
			_insightWindow.Dispose();
			_insightWindow = null;
		}
	}
}