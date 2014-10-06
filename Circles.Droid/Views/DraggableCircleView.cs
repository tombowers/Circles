using Android.Content;
using Android.Util;
using Android.Views;
using System;
using Android.Widget;
using Android.Animation;

namespace Circles.Droid
{
	public sealed class DraggableCircleView : CircleView, ViewEvents.ITapAwareView
	{
		private int _radius;
		private ViewEvents.ITouchHandler _touchHandler;

		private int _xDelta, _yDelta;
		private bool _dragging;

		public DraggableCircleView(Context context, int radius, int xPosition, int yPosition)
			: base(context, radius, xPosition, yPosition)
		{
			SharedConstruction(context, radius);
		}

		public DraggableCircleView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			SharedConstruction(context, 100);
		}

		private void SharedConstruction(Context context, int radius)
		{
			_radius = radius;

			// Create custom touch handler to support taps, double taps and drags
			_touchHandler = new TouchHandler(context, this);

			// Allow other consumers to use single and double click.
			_touchHandler.SingleTap += (v, e) =>
			{
				var singleTap = SingleTap;
				if (singleTap != null)
					singleTap(v, e);
			};

			_touchHandler.DoubleTap += (v, e) =>
			{
				var doubleTap = DoubleTap;
				if (doubleTap != null)
					doubleTap(v, e);
			};

			MakeDraggable();
		}

		#region ITapAwareView implementation

		public event ViewEvents.SingleTapEventHandler SingleTap;
		public event ViewEvents.DoubleTapEventHandler DoubleTap;

		#endregion

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			// Pass touch event to custom touch handler
			_touchHandler.ReceiveNativeTouchEvent(e);

			// The OnTouchEvent call above returns false even when returning true from all the overridden methods.
			// There's no occasion in which we wan't the parent to respond to events handled by this view, so returning true here.
			return true;
		}

		private void MakeDraggable()
		{
			_touchHandler.Down += (v, e) =>
			{
				BringToFront();

				var lParams = (RelativeLayout.LayoutParams)LayoutParameters;
				_xDelta = (int)e.TouchX - lParams.LeftMargin;
				_yDelta = (int)e.TouchY - lParams.TopMargin;
			};

			_touchHandler.Drag += (v, e) =>
			{
				if (!_dragging)
				{
					//Enlarge();
					_dragging = true;
				}

				var layoutParams = (RelativeLayout.LayoutParams)LayoutParameters;
				layoutParams.LeftMargin = (int)e.TouchX - _xDelta;
				layoutParams.TopMargin = (int)e.TouchY - _yDelta;
				LayoutParameters = layoutParams;
			};
		}

		private void Enlarge()
		{
			var fromVal = _radius;
			var toVal = (int)(_radius * 1.5);

			var scaleAnimation = ValueAnimator.OfInt(fromVal, toVal);
			scaleAnimation.Update += (s, e) =>
			{
				var result = (int)e.Animation.AnimatedValue;

				Console.WriteLine(result);

				_radius = result;
				Invalidate();
			};

			scaleAnimation.Start();
		}

		private void DeEnlarge()
		{
			var fromVal = (int)(_radius * 1.5);
			var toVal = _radius;

			var scaleAnimation = ValueAnimator.OfInt(fromVal, toVal);
			scaleAnimation.Update += (s, e) =>
			{
				var result = (int)e.Animation.AnimatedValue;

				_radius = result;
				Invalidate();
			};

			scaleAnimation.Start();
		}
	}
}
