using System;

namespace Circles
{
	public class ViewEvents
	{
		public delegate void DownEventHandler(object view, TouchEvent e);
		public delegate void UpEventHandler(object view, TouchEvent e);
		public delegate void SingleTapEventHandler(object view, TouchEvent e);
		public delegate void DoubleTapEventHandler(object view, TouchEvent e);
		public delegate void DragEventHandler(object view, TouchEvent e);

		public interface ITouchHandler
		{
			void ReceiveNativeTouchEvent(object e);

			event DownEventHandler Down;
			event UpEventHandler Up;
			event SingleTapEventHandler SingleTap;
			event DoubleTapEventHandler DoubleTap;
			event DragEventHandler Drag;
		}

		public class TouchEvent
		{
			public TouchEvent(float touchX, float touchY)
			{
				TouchX = touchX;
				TouchY = touchY;
			}

			public float TouchX { get; private set; }
			public float TouchY { get; private set; }
		}
	}
}
