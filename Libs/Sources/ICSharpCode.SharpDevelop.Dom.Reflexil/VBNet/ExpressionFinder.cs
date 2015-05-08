// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Markus Palme" email="MarkusPalme@gmx.de"/>
//     <version>$Revision: 5529 $</version>
// </file>

using System;
using System.Text;

namespace ICSharpCode.SharpDevelop.Dom.VBNet
{
	public class VBExpressionFinder : IExpressionFinder
	{
		ExpressionResult CreateResult(string expression)
		{
			if (expression == null)
				return ExpressionResult.Empty;
			if (expression.Length > 8 && expression.Substring(0, 8).Equals("Imports ", StringComparison.InvariantCultureIgnoreCase))
				return new ExpressionResult(expression.Substring(8).TrimStart(), ExpressionContext.Type);
			if (expression.Length > 4 && expression.Substring(0, 4).Equals("New ", StringComparison.InvariantCultureIgnoreCase))
				return new ExpressionResult(expression.Substring(4).TrimStart(), ExpressionContext.ObjectCreation);
			if (curTokenType == Ident && lastIdentifier.Equals("as", StringComparison.InvariantCultureIgnoreCase))
				return new ExpressionResult(expression, ExpressionContext.Type);
			return new ExpressionResult(expression);
		}
		
		public ExpressionResult FindExpression(string inText, int offset)
		{
			return CreateResult(FindExpressionInternal(inText, offset));
		}
		
		public string FindExpressionInternal(string inText, int offset)
		{
			offset--; // earlier all ExpressionFinder calls had an inexplicable "cursor - 1".
			// The IExpressionFinder API now uses normal cursor offsets, so we need to adjust the offset
			// because VBExpressionFinder still uses an implementation that expects old offsets
			
			this.text = FilterComments(inText, ref offset);
			this.offset = this.lastAccept = offset;
			this.state = START;
			if (this.text == null)
			{
				return null;
			}
			//Console.WriteLine("---------------");
			while (state != ERROR)
			{
				ReadNextToken();
				//Console.WriteLine("cur state {0} got token {1}/{3} going to {2}", GetStateName(state), GetTokenName(curTokenType), GetStateName(stateTable[state, curTokenType]), curTokenType);
				state = stateTable[state, curTokenType];
				
				if (state == ACCEPT || state == ACCEPT2 || state == DOT)
				{
					lastAccept = this.offset;
				}
				if (state == ACCEPTNOMORE)
				{
					return this.text.Substring(this.offset + 1, offset - this.offset);
				}
			}
			return this.text.Substring(this.lastAccept + 1, offset - this.lastAccept);
		}
		
		internal int LastExpressionStartPosition {
			get {
				return ((state == ACCEPTNOMORE) ? offset : lastAccept) + 1;
			}
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#")]
		public ExpressionResult FindFullExpression(string inText, int offset)
		{
			if (inText == null)
				throw new ArgumentNullException("inText");
			
			string expressionBeforeOffset = FindExpressionInternal(inText, offset + 1);
			if (expressionBeforeOffset == null || expressionBeforeOffset.Length == 0)
				return CreateResult(null);
			StringBuilder b = new StringBuilder(expressionBeforeOffset);
			// append characters after expression
			for (int i = offset + 1; i < inText.Length; ++i) {
				char c = inText[i];
				if (Char.IsLetterOrDigit(c) || c == '_') {
					if (Char.IsWhiteSpace(inText, i - 1))
						break;
					b.Append(c);
				} else if (c == ' ') {
					b.Append(c);
				} else if (c == '(') {
					int otherBracket = SearchBracketForward(inText, i + 1, '(', ')');
					if (otherBracket < 0)
						break;
					b.Append(inText, i, otherBracket - i + 1);
					break;
				} else {
					break;
				}
			}
			// remove space from end:
			if (b.Length > 0 && b[b.Length - 1] == ' ') {
				b.Length -= 1;
			}
			return CreateResult(b.ToString());
		}
		
		// Like VBNetFormattingStrategy.SearchBracketForward, but operates on a string.
		static int SearchBracketForward(string text, int offset, char openBracket, char closingBracket)
		{
			bool inString  = false;
			bool inComment = false;
			int  brackets  = 1;
			for (int i = offset; i < text.Length; ++i) {
				char ch = text[i];
				if (ch == '\n') {
					inString  = false;
					inComment = false;
				}
				if (inComment) continue;
				if (ch == '"') inString = !inString;
				if (inString)  continue;
				if (ch == '\'') {
					inComment = true;
				} else if (ch == openBracket) {
					++brackets;
				} else if (ch == closingBracket) {
					--brackets;
					if (brackets == 0) return i;
				}
			}
			return -1;
		}
		
		/// <summary>
		/// Removed the last part of the expression.
		/// </summary>
		/// <example>
		/// "obj.Field" => "obj"
		/// "obj.Method(args,...)" => "obj.Method"
		/// </example>
		public string RemoveLastPart(string expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");
			
			text = expression;
			offset = text.Length - 1;
			ReadNextToken();
			if (curTokenType == Ident && Peek() == '.')
				GetNext();
			return text.Substring(0, offset + 1);
		}
		
		#region Comment Filter and 'inside string watcher'
		int initialOffset;
		public string FilterComments(string text, ref int offset)
		{
			if (text == null)
				throw new ArgumentNullException("text");
			
			if (text.Length <= offset)
				return null;
			this.initialOffset = offset;
			StringBuilder outText = new StringBuilder();
			int curOffset = 0;
			while (curOffset <= initialOffset)
			{
				char ch = text[curOffset];
				
				switch (ch)
				{
					case '@':
						if (curOffset + 1 < text.Length && text[curOffset + 1] == '"')
						{
							outText.Append(text[curOffset++]); // @
							outText.Append(text[curOffset++]); // "
							if (!ReadVerbatimString(outText, text, ref curOffset))
							{
								return null;
							}
						}
						else
						{
							outText.Append(ch);
							++curOffset;
						}
						break;
					case '"':
						outText.Append(ch);
						curOffset++;
						if (!ReadString(outText, text, ref curOffset))
						{
							return null;
						}
						break;
					case '\'':
						offset -= 1;
						curOffset += 1;
						if (!ReadToEOL(text, ref curOffset, ref offset))
						{
							return null;
						}
						break;
					default:
						outText.Append(ch);
						++curOffset;
						break;
				}
			}
			
			return outText.ToString();
		}
		
		bool ReadToEOL(string text, ref int curOffset, ref int offset)
		{
			while (curOffset <= initialOffset)
			{
				char ch = text[curOffset++];
				--offset;
				if (ch == '\n')
				{
					return true;
				}
			}
			return false;
		}
		
		bool ReadString(StringBuilder outText, string text, ref int curOffset)
		{
			while (curOffset <= initialOffset)
			{
				char ch = text[curOffset++];
				outText.Append(ch);
				if (ch == '"')
				{
					return true;
				}
			}
			return false;
		}
		
		bool ReadVerbatimString(StringBuilder outText, string text, ref int curOffset)
		{
			while (curOffset <= initialOffset)
			{
				char ch = text[curOffset++];
				outText.Append(ch);
				if (ch == '"')
				{
					if (curOffset < text.Length && text[curOffset] == '"')
					{
						outText.Append(text[curOffset++]);
					}
					else
					{
						return true;
					}
				}
			}
			return false;
		}
		#endregion
		
		#region mini backward lexer
		string text;
		int offset;
		
		char GetNext()
		{
			if (offset >= 0)
			{
				return text[offset--];
			}
			return '\0';
		}
		
		char Peek()
		{
			if (offset >= 0)
			{
				return text[offset];
			}
			return '\0';
		}
		
//		void UnGet()
//		{
//			++offset;
//		}
		
		// tokens for our lexer
		const int Err = 0;
		const int Dot = 1;
		const int StrLit = 2;
		const int Ident = 3;
		const int New = 4;
		//		const int Bracket = 5;
		const int Parent = 6;
		const int Curly = 7;
		const int Using = 8;
		int curTokenType;
		
		string lastIdentifier;
		
		void ReadNextToken()
		{
			char ch = GetNext();
			
			curTokenType = Err;
			if (ch == '\0' || ch == '\n' || ch == '\r')
			{
				return;
			}
			while (Char.IsWhiteSpace(ch))
			{
				ch = GetNext();
				if (ch == '\n' || ch == '\r')
				{
					return;
				}
			}
			
			switch (ch)
			{
				case '}':
					if (ReadBracket('{'))
					{
						curTokenType = Curly;
					}
					break;
				case ')':
					if (ReadBracket('('))
					{
						curTokenType = Parent;
					}
					break;
				case ']':
					if (ReadBracket('['))
					{
						curTokenType = Ident;
					}
					break;
				case '.':
					curTokenType = Dot;
					break;
				case '\'':
				case '"':
					if (ReadStringLiteral(ch))
					{
						curTokenType = StrLit;
					}
					break;
				default:
					if (IsIdentifierPart(ch))
					{
						string ident = ReadIdentifier(ch);
						if (ident != null) {
							if (string.Equals(ident, "new", StringComparison.InvariantCultureIgnoreCase)) {
								curTokenType = New;
							} else if (string.Equals(ident, "imports", StringComparison.InvariantCultureIgnoreCase)) {
								curTokenType = Using;
							} else {
								lastIdentifier = ident;
								curTokenType = Ident;
							}
						}
					}
					break;
			}
		}
		
		bool ReadStringLiteral(char litStart)
		{
			while (true)
			{
				char ch = GetNext();
				if (ch == '\0')
				{
					return false;
				}
				if (ch == litStart)
				{
					if (Peek() == '@' && litStart == '"')
					{
						GetNext();
					}
					return true;
				}
			}
		}
		
		bool ReadBracket(char openBracket)
		{
			int curlyBraceLevel = 0;
			int squareBracketLevel = 0;
			int parenthesisLevel = 0;
			switch (openBracket)
			{
				case '(':
					parenthesisLevel++;
					break;
				case '[':
					squareBracketLevel++;
					break;
				case '{':
					curlyBraceLevel++;
					break;
			}
			
			while (parenthesisLevel != 0 || squareBracketLevel != 0 || curlyBraceLevel != 0)
			{
				char ch = GetNext();
				if (ch == '\0')
				{
					return false;
				}
				switch (ch)
				{
					case '(':
						parenthesisLevel--;
						break;
					case '[':
						squareBracketLevel--;
						break;
					case '{':
						curlyBraceLevel--;
						break;
					case ')':
						parenthesisLevel++;
						break;
					case ']':
						squareBracketLevel++;
						break;
					case '}':
						curlyBraceLevel++;
						break;
				}
			}
			return true;
		}
		
		string ReadIdentifier(char ch)
		{
			string identifier = ch.ToString();
			while (IsIdentifierPart(Peek()))
			{
				identifier = GetNext() + identifier;
			}
			return identifier;
		}
		
		static bool IsIdentifierPart(char ch)
		{
			return Char.IsLetterOrDigit(ch) || ch == '_';
		}
		#endregion
		
		#region finite state machine
		const int ERROR = 0;
		const int START = 1;
		const int DOT = 2;
		const int MORE = 3;
		const int CURLY = 4;
		const int CURLY2 = 5;
		const int CURLY3 = 6;
		
		const int ACCEPT = 7;
		const int ACCEPTNOMORE = 8;
		const int ACCEPT2 = 9;
		
		int state;
		int lastAccept;
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member")]
		static int[,] stateTable = new int[,] {
			//                   Err,     Dot,     Str,      ID,         New,     Brk,     Par,     Cur,   Using
			/*ERROR*/        { ERROR,   ERROR,   ERROR,   ERROR,        ERROR,  ERROR,   ERROR,   ERROR,   ERROR},
			/*START*/        { ERROR,   ERROR,  ACCEPT,  ACCEPT,        ERROR,   MORE, ACCEPT2,   CURLY,   ACCEPTNOMORE},
			/*DOT*/          { ERROR,   ERROR,  ACCEPT,  ACCEPT,        ERROR,   MORE, ACCEPT2,   CURLY,   ERROR},
			/*MORE*/         { ERROR,   ERROR,  ACCEPT,  ACCEPT,        ERROR,   MORE, ACCEPT2,   CURLY,   ERROR},
			/*CURLY*/        { ERROR,   ERROR,   ERROR,   ERROR,        ERROR, CURLY2,   ERROR,   ERROR,   ERROR},
			/*CURLY2*/       { ERROR,   ERROR,   ERROR,  CURLY3,        ERROR,  ERROR,   ERROR,   ERROR,   ERROR},
			/*CURLY3*/       { ERROR,   ERROR,   ERROR,   ERROR, ACCEPTNOMORE,  ERROR,   ERROR,   ERROR,   ERROR},
			/*ACCEPT*/       { ERROR,     DOT,   ERROR,   ERROR,       ACCEPT,  ERROR,   ERROR,   ERROR,   ACCEPTNOMORE},
			/*ACCEPTNOMORE*/ { ERROR,   ERROR,   ERROR,   ERROR,        ERROR,  ERROR,   ERROR,   ERROR,   ERROR},
			/*ACCEPT2*/      { ERROR,     DOT,   ERROR,  ACCEPT,       ACCEPT,  ERROR,   ERROR,   ERROR,   ERROR},
		};
		#endregion
	}
}
