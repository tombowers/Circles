using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Circles.iOS
{
	public class CircleView : UIView
	{
		private CustomColor _colour;
		private bool _showTitle;

		public CircleView(RectangleF frame)
			: base(frame)
		{
			BackgroundColor = UIColor.Clear;
		}

		public override void Draw(RectangleF rect)
		{
			using (var ctx = UIGraphics.GetCurrentContext())
			{
				ctx.AddEllipseInRect(new RectangleF(0, 0, Frame.Width, Frame.Height));
				ctx.SetFillColor(UIColor.Blue.CGColor);
				ctx.FillPath();

				if (_showTitle)
				{
					// translate the CTM by the font size so it displays on screen
					const float fontSize = 35f;
					ctx.TranslateCTM (0, fontSize);

					// set general-purpose graphics state
					ctx.SetLineWidth (1.0f);
					ctx.SetFillColor (UIColor.Red.CGColor);

					// set text specific graphics state
					ctx.SetTextDrawingMode (CGTextDrawingMode.FillStroke);
					ctx.SelectFont("Helvetica", fontSize, CGTextEncoding.MacRoman);

					// show the text
					ctx.ShowText(_colour.Title);
				}
			}
		}

		public void ChangeColour(CustomColor colour)
		{
			_colour = colour;
			SetNeedsDisplay();
		}

		public void ToggleTitle()
		{
			_showTitle = !_showTitle;
			SetNeedsDisplay();
		}
	}
}

