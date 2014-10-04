using System;
using Android.Views;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Android.Text;
using Android.Animation;
using Android.Views.Animations;
using Android.Widget;

namespace Circles.Droid
{
	public class CircleView : View
	{
		private Paint _paint;
		private TextPaint _textPaint;

		private int _radius;
		private int _shadowSize;
		private int _xPosition;
		private int _yPosition;

		private CustomColor _backgroundColour;
		private bool _showTitle;

		// Manual instantiation
		public CircleView(Context context, int radius, int xPosition, int yPosition) : base(context)
		{
			if (radius < 1)
				throw new ArgumentOutOfRangeException("radius", "radius must be 1 or greater");
			if (xPosition < 1)
				throw new ArgumentOutOfRangeException("xPosition", "xPosition must be 1 or greater");
			if (yPosition < 1)
				throw new ArgumentOutOfRangeException("yPosition", "yPosition must be 1 or greater");
				
			SharedConstruction(radius, xPosition, yPosition);
		}

		// Instantiation by Android from axml
		public CircleView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			var attributes = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.CircleView, 0, 0);

			try
			{
				SharedConstruction(attributes.GetInteger(Resource.Styleable.CircleView_radius, 0), 0, 0);
			}
			finally
			{
				attributes.Recycle();
			}
		}

		private void SharedConstruction(int radius, int xPosition, int yPosition)
		{
			_shadowSize = 10;
			_radius = radius - (_shadowSize * 2);

			_xPosition = xPosition;
			_yPosition = yPosition;

			_paint = new Paint();
			_paint.AntiAlias = true;
			_paint.SetShadowLayer(_shadowSize, 0, 0.2f, Color.DarkGray);

			_textPaint = new TextPaint();
			_textPaint.SetTypeface(Typeface.Monospace);
			_textPaint.AntiAlias = true;
			_textPaint.TextSize = 36;

			_backgroundColour = new CustomColor
			{
				Rgb = new Rgb { Red = 255, Green = 255, Blue = 255 },
				Title = "White"
			};
		}

		protected override void OnAttachedToWindow()
		{
			var halfViewWidth = _radius + _shadowSize;

			LayoutParameters.Width = (halfViewWidth) * 2;
			LayoutParameters.Height = (halfViewWidth) * 2;
			((RelativeLayout.LayoutParams)LayoutParameters).LeftMargin = _xPosition - halfViewWidth;
			((RelativeLayout.LayoutParams)LayoutParameters).TopMargin = _yPosition - halfViewWidth;

			base.OnAttachedToWindow();

			var anim = new ScaleAnimation(0, 1, 0, 1, halfViewWidth, halfViewWidth);
			anim.Duration = 400;
			anim.FillAfter = true;
			anim.Interpolator = new OvershootInterpolator();
			StartAnimation(anim);
		}

		public override void Draw(Canvas canvas)
		{
			base.Draw(canvas);

			_paint.Color = _backgroundColour.ToAndroid();

			var viewCenter = _radius + _shadowSize;

			canvas.DrawCircle(viewCenter, viewCenter, _radius, _paint);

			if (!_showTitle)
				return;

			_textPaint.Color = _backgroundColour.ContrastingColour().ToAndroid();

			// TextUtils.Ellipsize not working, using hand-written method.
			//_backgroundColour.Title = TextUtils.Ellipsize(_backgroundColour.Title, _textPaint, Width - 20, TextUtils.TruncateAt.End);
			_backgroundColour.Title = _backgroundColour.Title.Ellipsize(_textPaint, Width - 20);

			var bounds = new Rect();
			_textPaint.GetTextBounds(_backgroundColour.Title, 0, _backgroundColour.Title.Length, bounds);

			canvas.DrawText(_backgroundColour.Title, viewCenter - bounds.CenterX(), viewCenter - bounds.CenterY(), _textPaint);
		}

		public void ChangeColour(CustomColor colour)
		{
			_backgroundColour = colour;
			Invalidate();
		}

		public void ToggleTitle()
		{
			_showTitle = !_showTitle;
			Invalidate();
		}

		public void ChangeColourAnimated(CustomColor colour)
		{
			var fromVal = _backgroundColour.ToAndroid().ToArgb();
			var toVal = colour.ToAndroid().ToArgb();

			var colourAnimation = ValueAnimator.OfObject(new ArgbEvaluator(), fromVal, toVal);
			colourAnimation.Update += (s, e) =>
			{
				var result = (int)e.Animation.AnimatedValue;

				var r = 0xFF & (result >> 16);
				var g = 0xFF & (result >> 8);
				var b = 0xFF & (result >> 0);

				_backgroundColour = new CustomColor { Rgb = new Rgb { Red = r, Green = g, Blue = b }, Title = colour.Title };
				Invalidate();
			};

			colourAnimation.Start();
		}
	}
}

