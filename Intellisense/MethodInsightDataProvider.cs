/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
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

    class MethodInsightDataProvider : IInsightDataProvider
    {

        #region " Fields "
        TextArea m_textarea = null;
        int m_defaultindex = -1;
        int m_argumentstartoffset;
        string[] m_insighttext;
        IntellisenseForm iForm;
        #endregion

        #region " Properties "
        public int DefaultIndex
        {
            get { return m_defaultindex; }
            set { m_defaultindex = value; }
        }

        protected int ArgumentStartOffset
        {
            get { return m_argumentstartoffset; }
        }

        public int InsightDataCount
        {
            get { return (m_insighttext != null) ? m_insighttext.Length : 0; }
        }
        #endregion

        #region " Methods "
        public string GetInsightData(int number)
        {
            return (m_insighttext != null) ? m_insighttext[number] : string.Empty;
        }

        public virtual bool CaretOffsetChanged()
        {
            IDocument document = m_textarea.Document;
            bool closeDataProvider = m_textarea.Caret.Offset <= ArgumentStartOffset;
            int brackets = 0;
            int curlyBrackets = 0;
            if (!closeDataProvider)
            {
                bool insideChar = false;
                bool insideString = false;
                for (int offset = ArgumentStartOffset; offset < Math.Min(m_textarea.Caret.Offset, document.TextLength); ++offset)
                {
                    char ch = document.GetCharAt(offset);
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
            this.m_textarea = textArea;
            SetupDataProvider(fileName);
        }


        public void SetupDataProvider(string fileName)
        {
            IExpressionFinder expressionFinder;
            if (IntellisenseForm.SupportedLanguage == ESupportedLanguage.VisualBasic)
            {
                expressionFinder = new VBExpressionFinder();
            }
            else
            {
                expressionFinder = new CSharpExpressionFinder(iForm.ParseInformation);
            }

            //TextLocation position = m_textarea.Caret.Position;
            //ExpressionResult expression = expressionFinder.FindFullExpression(m_textarea.MotherTextEditorControl.Text, m_textarea.Document.PositionToOffset(position));
            //if (expression.Region.IsEmpty)
            //{
            //    expression.Region = new DomRegion(position.Line + 1, position.Column + 1);
            //}
            ExpressionResult expression = expressionFinder.FindFullExpression(
                    m_textarea.MotherTextEditorControl.Text,
                    m_textarea.MotherTextEditorControl.Document.PositionToOffset(m_textarea.Caret.Position)-1);
            if (expression.Region.IsEmpty)
            {
                expression.Region = new DomRegion(m_textarea.Caret.Position.Line + 1, m_textarea.Caret.Position.Column + 1);
            }



            NRefactoryResolver resolver = new NRefactoryResolver(iForm.ProjectContent.Language);
            ResolveResult rr = resolver.Resolve(expression, iForm.ParseInformation, m_textarea.MotherTextEditorControl.Text);

            List<string> lines = new List<string>();
            if (rr is MethodGroupResolveResult)
            {
                MethodGroupResolveResult mrr = rr as MethodGroupResolveResult;
                IAmbience ambience = IntellisenseForm.SupportedLanguage == ESupportedLanguage.VisualBasic ? (IAmbience)new VBNetAmbience() : new CSharpAmbience();
                ambience.ConversionFlags = ConversionFlags.StandardConversionFlags | ConversionFlags.ShowAccessibility;
                
                foreach (MethodGroup methodgroup in mrr.Methods)
                {
                    foreach (IMethod method in methodgroup)
                    {
                        lines.Add(ToolTipProvider.GetMemberText(ambience, method));
                    }
                }
            }

            m_insighttext = (lines.Count > 0) ? lines.ToArray() : null;
            m_argumentstartoffset = m_textarea.Caret.Offset;
        }
        
        public MethodInsightDataProvider(IntellisenseForm iForm)
        {
            this.iForm = iForm;
        }
        #endregion
                
	}
}
