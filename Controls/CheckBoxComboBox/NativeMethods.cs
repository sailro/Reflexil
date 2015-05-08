using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Reflexil.Editors
{
	/// <summary>
	/// CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
	/// </summary>
	internal static class NativeMethods
	{
		// ReSharper disable InconsistentNaming
		internal const int WM_NCHITTEST = 0x0084,
			WM_NCACTIVATE = 0x0086,
			WS_EX_NOACTIVATE = 0x08000000,
			HTTRANSPARENT = -1,
			HTLEFT = 10,
			HTRIGHT = 11,
			HTTOP = 12,
			HTTOPLEFT = 13,
			HTTOPRIGHT = 14,
			HTBOTTOM = 15,
			HTBOTTOMLEFT = 16,
			HTBOTTOMRIGHT = 17,
			WM_USER = 0x0400,
			WM_REFLECT = WM_USER + 0x1C00,
			WM_COMMAND = 0x0111,
			CBN_DROPDOWN = 7,
			WM_GETMINMAXINFO = 0x0024;

		// ReSharper restore InconsistentNaming

		internal static int HiWord(int n)
		{
			return (n >> 16) & 0xffff;
		}

		internal static int HiWord(IntPtr n)
		{
			return HiWord(unchecked((int) (long) n));
		}

		internal static int LoWord(int n)
		{
			return n & 0xffff;
		}

		internal static int LoWord(IntPtr n)
		{
			return LoWord(unchecked((int) (long) n));
		}

		[StructLayout(LayoutKind.Sequential)]
		// ReSharper disable once InconsistentNaming
		internal struct MINMAXINFO
		{
			public Point reserved;
			public Size maxSize;
			public Point maxPosition;
			public Size minTrackSize;
			public Size maxTrackSize;
		}
	}
}