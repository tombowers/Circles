using MonoTouch.UIKit;

namespace Circles.iOS
{
	public static class CustomColorExtensions
	{
		public static UIColor ToUIColor(this CustomColor colour)
		{
			return new UIColor(colour.Rgb.Red / 255f, colour.Rgb.Green / 255f, colour.Rgb.Blue / 255f, 1);
		}
	}
}
