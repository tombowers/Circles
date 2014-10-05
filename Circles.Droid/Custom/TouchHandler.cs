using System;
using Android.Views;
using Android.Content;

namespace Circles.Droid
{
	public class TouchHandler : GestureDetector.SimpleOnGestureListener, ViewEvents.ITouchHandler
	{
		private readonly View _view;
		private readonly GestureDetector _gestureDetector;

		public TouchHandler(Context context, View view)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			if (view == null)
				throw new ArgumentNullException("view");

			_view = view;

			_gestureDetector = new GestureDetector(context, this);

			view.Touch += (sender, e) =>
			{
				var up = Up;
				if (up != null)
					up(_view, new ViewEvents.TouchEvent(e.Event.RawX, e.Event.RawY));
			};
		}

		#region ITouchHandler implementation

		public void ReceiveNativeTouchEvent(object e)
		{
			var motionEvent = (MotionEvent)e;

			_gestureDetector.OnTouchEvent(motionEvent);
		}

		public event ViewEvents.DownEventHandler Down;
		public event ViewEvents.UpEventHandler Up;
		public event ViewEvents.SingleTapEventHandler SingleTap;
		public event ViewEvents.DoubleTapEventHandler DoubleTap;
		public event ViewEvents.DragEventHandler Drag;

		#endregion


		#region SimpleOnGestureListener overrides

		public override bool OnDown(MotionEvent e)
		{
			var down = Down;
			if (Down != null)
				down(_view, new ViewEvents.TouchEvent(e.RawX, e.RawY));

			return true;
		}

		public override bool OnSingleTapConfirmed(MotionEvent e)
		{
			var singleTap = SingleTap;
			if (singleTap != null)
				singleTap(_view, new ViewEvents.TouchEvent(e.RawX, e.RawY));

			return true;
		}

		public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			var drag = Drag;
			if (drag != null)
				drag(_view, new ViewEvents.TouchEvent(e2.RawX, e2.RawY));

			return false;
		}

		public override bool OnDoubleTap(MotionEvent e)
		{
			var doubleTap = DoubleTap;
			if (doubleTap != null)
				doubleTap(_view, new ViewEvents.TouchEvent(e.RawX, e.RawY));

			return true;
		}

		#endregion
	}
}