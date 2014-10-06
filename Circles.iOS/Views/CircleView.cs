using System;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace Circles.iOS
{
	public class CircleView : UIView
	{
		private CustomColor _colour;
		private bool _showTitle;
		private readonly UIFont _textFont;

		private const float ShadowSize = 4;
		private const float FontSize = 13f;
		private const float ShadowOffset = ShadowSize / 2;
		private float _circleSize;

		public CircleView(RectangleF frame)
			: base(frame)
		{
			BackgroundColor = UIColor.Clear;

			_colour = new CustomColor
			{
				Rgb = new Rgb { Red = 255, Green = 255, Blue = 255 },
				Title = "White"
			};

			_textFont = UIFont.FromName("Courier", FontSize);
				
			_circleSize = Frame.Width - ShadowSize;
		}

		public override void Draw(RectangleF rect)
		{
			using (var ctx = UIGraphics.GetCurrentContext())
			{
				ctx.AddEllipseInRect(new RectangleF(ShadowOffset, ShadowOffset, _circleSize, _circleSize));
				ctx.SetFillColor(_colour.ToUIColor().CGColor);
				ctx.SetShadow(new SizeF(0, 0), ShadowSize);
				ctx.FillPath();

				if (!_showTitle)
					return;
					
				_colour.Title = _colour.Title.Ellipsize(this, _textFont, (int)Frame.Width - 10);

				var textSize = StringSize(_colour.Title, _textFont);

				var viewCenter = Frame.Width / 2;
				var textLeft = viewCenter - (textSize.Width / 2);
				var textTop = (_circleSize + textSize.Height) / 2;

				ctx.SetShadow(new SizeF(0, 0), 0);
				ctx.TranslateCTM (textLeft, textTop);
				ctx.ScaleCTM(1, -1);

				// set general-purpose graphics state
				ctx.SetLineWidth(1.0f);
				ctx.SetFillColor(_colour.ContrastingColour().ToUIColor().CGColor);

				// set text specific graphics state
				ctx.SetTextDrawingMode (CGTextDrawingMode.Fill);

				ctx.SelectFont("Courier", FontSize, CGTextEncoding.MacRoman);

				// show the text
				ctx.ShowText(_colour.Title);

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

		public void ChangeColourAnimated(CustomColor colour)
		{
			var currentR = (float)_colour.Rgb.Red;
			var currentG = (float)_colour.Rgb.Green;
			var currentB = (float)_colour.Rgb.Blue;

			const float numberOfIntervals = 100;
			var intervalR = (colour.Rgb.Red - currentR) / numberOfIntervals;
			var intervalG = (colour.Rgb.Green - currentG) / numberOfIntervals;
			var intervalB = (colour.Rgb.Blue - currentB) / numberOfIntervals;

			for (var i = 0; i <= numberOfIntervals; i++)
			{
				var newColour = new CustomColor { Rgb = new Rgb { Red = (int)currentR, Green = (int)currentG, Blue = (int)currentB }, Title = colour.Title };

				ChangeColour(newColour);
				CATransaction.Flush();

				currentR += intervalR;
				currentG += intervalG;
				currentB += intervalB;
			}
		}
	}
}

