using MonoTouch.UIKit;
using System.Drawing;

namespace Circles.iOS
{
	public class SinglePageViewController : UIViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.LightGray;

			var screenSize = UIScreen.MainScreen.Bounds;

			AddCircle(screenSize.Width / 2, screenSize.Height / 2);
		}

		public override void TouchesBegan(MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			var touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				AddCircle(touch.LocationInView(View).X, touch.LocationInView(View).Y);
			}
		}

		private void AddCircle(float xPosition, float yPosition)
		{
			const int radius = 50;

			var circle = new DraggableCircleView(new RectangleF(xPosition - radius, yPosition - radius, radius * 2, radius * 2));

			View.AddSubview(circle);
		}
	}
}

