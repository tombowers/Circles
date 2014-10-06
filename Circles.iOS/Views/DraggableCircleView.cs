using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System;

namespace Circles.iOS
{
	public class DraggableCircleView : CircleView, ViewEvents.ITapAwareView
	{
		private PointF lastLocation;

		public DraggableCircleView(RectangleF frame)
			: base(frame)
		{
			SetupGestureRecognizers();

			lastLocation = Center;
		}

		private void SetupGestureRecognizers()
		{
			var singleTapGestureRecognizer = new UITapGestureRecognizer(HandleSingleTap);
			singleTapGestureRecognizer.NumberOfTapsRequired = 1;
			//singleTapGestureRecognizer.CancelsTouchesInView = true;

			var doubleTapGestureRecognizer = new UITapGestureRecognizer(HandleDoubleTap);
			doubleTapGestureRecognizer.NumberOfTapsRequired = 2;

			var panGestureRecognizer = new UIPanGestureRecognizer(HandlePan);

			singleTapGestureRecognizer.RequireGestureRecognizerToFail(doubleTapGestureRecognizer);
			//singleTapGestureRecognizer.RequireGestureRecognizerToFail(panGestureRecognizer);

			GestureRecognizers = new UIGestureRecognizer[] { singleTapGestureRecognizer, doubleTapGestureRecognizer, panGestureRecognizer };
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			lastLocation = Center;

			base.TouchesBegan(touches, evt);
		}

		private void HandleSingleTap(UIGestureRecognizer tapGestureRecognizer)
		{
			Superview.BringSubviewToFront(this);

			var singleTap = SingleTap;
			if (singleTap != null)
				singleTap(this, new ViewEvents.TouchEvent(tapGestureRecognizer.LocationInView(null).X, tapGestureRecognizer.LocationInView(null).Y));
		}

		private void HandleDoubleTap(UIGestureRecognizer tapGestureRecognizer)
		{
			Superview.BringSubviewToFront(this);

			var doubleTap = DoubleTap;
			if (doubleTap != null)
				doubleTap(this, new ViewEvents.TouchEvent(tapGestureRecognizer.LocationInView(null).X, tapGestureRecognizer.LocationInView(null).Y));
		}

		private void HandlePan(UIPanGestureRecognizer panGestureRecognizer)
		{
			Superview.BringSubviewToFront(this);

			var translation = panGestureRecognizer.TranslationInView(Superview);
			Center = new PointF(lastLocation.X + translation.X, lastLocation.Y + translation.Y);
		}

		#region ITapAwareView implementation

		public event ViewEvents.SingleTapEventHandler SingleTap;
		public event ViewEvents.DoubleTapEventHandler DoubleTap;

		#endregion
	}
}

