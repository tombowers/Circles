using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace Circles.iOS
{
	public class SinglePageViewController : UIViewController
	{
		private readonly IRandomColourGenerator _randomColourGenerator;

		public SinglePageViewController()
		{
			_randomColourGenerator = new ColourLoversRandomColourGenerator(new FastLocalRandomColorGenerator());

			var singleTapGestureRecognizer = new UITapGestureRecognizer(HandleSingleTap);
			singleTapGestureRecognizer.NumberOfTapsRequired = 1;

			View.GestureRecognizers = new [] { singleTapGestureRecognizer };
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.FromRGB(240 / 255f, 246 / 255f, 260 / 255f);

			var screenSize = UIScreen.MainScreen.Bounds;

			AddCircle(screenSize.Width / 2, screenSize.Height / 2);
		}

		private void HandleSingleTap(UIGestureRecognizer tapGestureRecognizer)
		{
			AddCircle(tapGestureRecognizer.LocationInView(View).X, tapGestureRecognizer.LocationInView(View).Y);
		}

		private void AddCircle(float xPosition, float yPosition)
		{
			const int radius = 50;

			var circle = new DraggableCircleView(new RectangleF(xPosition - radius, yPosition - radius, radius * 2, radius * 2));

			circle.SingleTap += (v, e) =>
			{
				var clickedCircle = (DraggableCircleView)v;

				var toast = new ToastView("Finding new colour", 3000);
				toast.Show();

				_randomColourGenerator.GetNextAsync(c =>
				{
					clickedCircle.ChangeColourAnimated(c);
					toast.HideAndRemove();
				});
			};

			circle.DoubleTap += (v, e) =>
				((DraggableCircleView)v).ToggleTitle();

			circle.Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);

			View.AddSubview(circle);

			UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseOut, () =>
			{
				circle.Transform =  CGAffineTransform.MakeScale(1.3f, 1.3f);
			},
				() => UIView.Animate(0.1, 0, UIViewAnimationOptions.CurveEaseIn, () =>
				{
					circle.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
				},
				() => {}
				)
			);

			_randomColourGenerator.GetNextAsync(circle.ChangeColourAnimated);
		}
	}
}

