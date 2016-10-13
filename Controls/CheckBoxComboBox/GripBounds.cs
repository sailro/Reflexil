// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
using System.Drawing;

namespace Reflexil.Editors
{
	internal struct GripBounds
	{
		private const int GripSize = 6;
		private const int CornerGripSize = GripSize << 1;

		public GripBounds(Rectangle clientRectangle)
		{
			_clientRectangle = clientRectangle;
		}

		private readonly Rectangle _clientRectangle;

		public Rectangle ClientRectangle
		{
			get { return _clientRectangle; }
			//set { clientRectangle = value; }
		}

		public Rectangle Bottom
		{
			get
			{
				var rect = ClientRectangle;
				rect.Y = rect.Bottom - GripSize + 1;
				rect.Height = GripSize;
				return rect;
			}
		}

		public Rectangle BottomRight
		{
			get
			{
				var rect = ClientRectangle;
				rect.Y = rect.Bottom - CornerGripSize + 1;
				rect.Height = CornerGripSize;
				rect.X = rect.Width - CornerGripSize + 1;
				rect.Width = CornerGripSize;
				return rect;
			}
		}

		public Rectangle Top
		{
			get
			{
				var rect = ClientRectangle;
				rect.Height = GripSize;
				return rect;
			}
		}

		public Rectangle TopRight
		{
			get
			{
				var rect = ClientRectangle;
				rect.Height = CornerGripSize;
				rect.X = rect.Width - CornerGripSize + 1;
				rect.Width = CornerGripSize;
				return rect;
			}
		}

		public Rectangle Left
		{
			get
			{
				var rect = ClientRectangle;
				rect.Width = GripSize;
				return rect;
			}
		}

		public Rectangle BottomLeft
		{
			get
			{
				var rect = ClientRectangle;
				rect.Width = CornerGripSize;
				rect.Y = rect.Height - CornerGripSize + 1;
				rect.Height = CornerGripSize;
				return rect;
			}
		}

		public Rectangle Right
		{
			get
			{
				var rect = ClientRectangle;
				rect.X = rect.Right - GripSize + 1;
				rect.Width = GripSize;
				return rect;
			}
		}

		public Rectangle TopLeft
		{
			get
			{
				var rect = ClientRectangle;
				rect.Width = CornerGripSize;
				rect.Height = CornerGripSize;
				return rect;
			}
		}
	}
}