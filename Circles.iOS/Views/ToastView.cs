using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Circles.iOS
{
	public class ToastView : NSObject
	{

		ToastSettings theSettings = new ToastSettings ();

		private readonly string _text;
		private UIView _view;

		private int _offsetLeft = 0;
		private int _offsetTop = 0;

		public ToastView(string text, int durationMilliseonds)
		{
			_text = text;
			theSettings.Duration = durationMilliseonds;
		}

		public ToastView SetGravity(ToastGravity gravity, int offSetLeft, int offSetTop)
		{
			theSettings.Gravity = gravity;

			_offsetLeft = offSetLeft;
			_offsetTop = offSetTop;

			return this;
		}

		public ToastView SetPosition(PointF position)
		{
			theSettings.Position = position;
			return this;
		}

		public void Show()
		{
			UIButton v = UIButton.FromType(UIButtonType.Custom);
			_view = v;

			UIFont font = UIFont.SystemFontOfSize(12);
			SizeF textSize = _view.StringSize(_text, font, new SizeF (280, 60));

			var label = new UILabel(new RectangleF (0, 0, textSize.Width + 5, textSize.Height + 5));
			label.BackgroundColor = UIColor.Clear;
			label.TextColor = UIColor.White;
			label.TextAlignment = UITextAlignment.Center;
			label.Font = font;
			label.Text = _text;
			label.Lines = 0;

			v.Frame = new RectangleF(0, 0, textSize.Width + 50, textSize.Height + 20);
			label.Center = new PointF(v.Frame.Size.Width / 2, v.Frame.Height / 2);

			v.AddSubview(label);

			v.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0.6f);
			v.Layer.CornerRadius = v.Frame.Height / 2;

			UIWindow window = UIApplication.SharedApplication.Windows[0];

			var point = new PointF(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);

			switch (theSettings.Gravity)
			{
				case ToastGravity.Top:
					point = new PointF(window.Frame.Size.Width / 2, 45);
					break;
				case ToastGravity.Bottom:
					point = new PointF(window.Frame.Size.Width / 2, window.Frame.Size.Height - 45);
					break;
				case ToastGravity.Center:
					point = new PointF(window.Frame.Size.Width / 2, window.Frame.Size.Height / 2);
					break;
				default:
					point = theSettings.Position;
					break;
			}

			point = new PointF(point.X + _offsetLeft, point.Y + _offsetTop);
			v.Center = point;

			v.Alpha = 0;

			window.AddSubview(v);

			UIView.Animate(0.5, () =>
			{
				v.Alpha = 1;
			});

			v.AllTouchEvents += delegate { Hide(); };

			NSTimer.CreateScheduledTimer(theSettings.DurationSeconds, Hide);
		}


		public void Hide()
		{
			UIView.Animate(0.5, () =>
			{
				_view.Alpha = 0;
			});
		}

		public void HideAndRemove()
		{
			UIView.Animate(0.5, () =>
			{
				_view.Alpha = 0;
			},
			Remove
			);
		}

		public void Remove()
		{
			_view.RemoveFromSuperview();
		}

	}

	public class ToastSettings
	{
		public ToastSettings()
		{
			this.Duration = 500;
			this.Gravity = ToastGravity.Bottom;
		}

		public int Duration
		{
			get;
			set;
		}

		public double DurationSeconds
		{
			get { return (double) Duration / 1000; }

		}

		public ToastGravity Gravity
		{
			get;
			set;
		}

		public PointF Position
		{
			get;
			set;
		}


	}

	public enum ToastGravity
	{
		Top = 0,
		Bottom = 1,
		Center = 2
	}
}