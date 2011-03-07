using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ICSharpCode.NRefactory.Extensions
{
    public static class XmlConvertExtensions
    {
        static Char[] ws_items = new Char[] { '\u0020', '\u1680', '\u180E', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u202F', '\u205F', '\u3000', '\u2028', '\u2029', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u0085', '\u00A0' };

        public static bool IsWhitespaceChar(char ch)
        {
            return ws_items.Contains(ch);
        }

        public static bool IsExtender(char ch)
        {
            if ((ch >= '\u3031') && (ch <= '\u3035')) return true;
            if ((ch >= '\u30FC') && (ch <= '\u30FE')) return true;

            switch (ch)
            {
                case '\u00B7':
                case '\u02D0':
                case '\u02D1':
                case '\u0387':
                case '\u0640':
                case '\u0E46':
                case '\u0EC6':
                case '\u3005':
                case '\u309D':
                case '\u309E':
                    return true;
                default:
                    break;
            }
            return false;
        }

        public static bool IsNCNameChar(char ch)
        {
            if (Char.IsLetter(ch) || Char.IsDigit(ch) || IsExtender(ch))
                return true;

            switch (ch)
            {
                case '.':
                case '-':
                case '_':
                    return true;
                default:
                    return false;
            }
        }

    }
}
