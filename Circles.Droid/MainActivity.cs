using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace Circles.Droid
{
	[Activity(Label = "Circles.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private RelativeLayout _layout;
		private IRandomColourGenerator _randomColourGenerator;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			_layout = (RelativeLayout)LayoutInflater.Inflate(Resource.Layout.Main, null);
			SetContentView(_layout);

			_randomColourGenerator = new ColourLoversRandomColourGenerator(new FastLocalRandomColorGenerator());

			//_layout.Touch += (s, e) => AddCircle();

			var size = new Point();
			WindowManager.DefaultDisplay.GetSize(size);

			AddCircle(size.X / 2, size.Y / 2);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			if (e.Action == MotionEventActions.Up)
				AddCircle((int)e.RawX, (int)e.RawY);

			return base.OnTouchEvent(e);
		}

		private void AddCircle(int xPosition, int yPosition)
		{
			var circle = new DraggableCircleView(this, 200, xPosition, yPosition);

			circle.Click += (s, e) =>
			{
				var clickedCircle = (DraggableCircleView)s;
				var toast = Toast.MakeText(this, "Finding new colour", ToastLength.Short);
				toast.Show();

				_randomColourGenerator.GetNextAsync(c => {
					clickedCircle.ChangeColourAnimated(c);
					toast.Cancel();
				});
			};

			circle.DoubleTap += (v, e) =>
				((DraggableCircleView)v).ToggleTitle();

			_layout.AddView(circle);

			_randomColourGenerator.GetNextAsync(circle.ChangeColourAnimated);
		}
	}
}
