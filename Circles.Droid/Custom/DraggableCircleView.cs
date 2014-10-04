using Android.Content;
using Android.Util;
using Android.Views;
using System;
using Android.Widget;

namespace Circles.Droid
{
	public delegate void DoubleTapEventHandler(IDoubleTapAwareView view);
	public interface IDoubleTapAwareView
	{
		event DoubleTapEventHandler DoubleTap;
		void OnDoubleTap(IDoubleTapAwareView view);
	}

	public sealed class DraggableCircleView : CircleView, IDoubleTapAwareView
	{
		private GestureDetector _touchDetector;

		public DraggableCircleView(Context context, int radius, int xPosition, int yPosition)
			: base(context, radius, xPosition, yPosition)
		{
			SharedConstruction(context);
		}

		public DraggableCircleView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			SharedConstruction(context);
		}

		private void SharedConstruction(Context context)
		{
			_touchDetector = new GestureDetector(context, new GestureListener(this));
		}

		public event DoubleTapEventHandler DoubleTap;

		public void OnDoubleTap(IDoubleTapAwareView view)
		{
			var doubleTap = DoubleTap;
			if (doubleTap != null)
				DoubleTap(view);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			BringToFront();

			_touchDetector.OnTouchEvent(e);

			// The OnTouchEvent call above returns false even when returning true from all the overridden methods.
			// There's no occasion in which we wan't the parent to respond to events handled by this view, so returning true here.
			return true;
		}

		// Should be moved of here and changed to fire events instead of handling view position directly.
		private class GestureListener : GestureDetector.SimpleOnGestureListener
		{
			private readonly View _view;

			private int _xDelta, _yDelta;

			public GestureListener(View view)
			{
				if (view == null)
					throw new ArgumentNullException("view");

				_view = view;
			}

			public override bool OnDown(MotionEvent e)
			{
				var lParams = (RelativeLayout.LayoutParams)_view.LayoutParameters;
				_xDelta = (int)e.RawX - lParams.LeftMargin;
				_yDelta = (int)e.RawY - lParams.TopMargin;

				return true;
			}

			public override bool OnSingleTapConfirmed(MotionEvent e)
			{
				_view.PerformClick();

				return true;
			}

			public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
			{
				var layoutParams = (RelativeLayout.LayoutParams)_view.LayoutParameters;
				layoutParams.LeftMargin = (int)e2.RawX - _xDelta;
				layoutParams.TopMargin = (int)e2.RawY - _yDelta;
				_view.LayoutParameters = layoutParams;

				return true;
			}

			public override bool OnDoubleTap(MotionEvent e)
			{
				var view = _view as IDoubleTapAwareView;
				if (view != null)
					view.OnDoubleTap(view);

				return true;
			}	
		}
	}
}
