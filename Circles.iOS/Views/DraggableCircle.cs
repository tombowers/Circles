using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Circles.iOS
{
	public class DraggableCircleView : CircleView
	{
		private PointF lastLocation;

		public DraggableCircleView(RectangleF frame)
			: base(frame)
		{
			GestureRecognizers = new UIGestureRecognizer[] { new UIPanGestureRecognizer(MoveView) };
		}

		public override void TouchesBegan(MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			Superview.BringSubviewToFront(this);

			lastLocation = Center;
		}

		private void MoveView(UIPanGestureRecognizer gestureRecognizer)
		{
			var translation = gestureRecognizer.TranslationInView(Superview);
			Center = new PointF(lastLocation.X + translation.X, lastLocation.Y + translation.Y);
		}
	}
}

