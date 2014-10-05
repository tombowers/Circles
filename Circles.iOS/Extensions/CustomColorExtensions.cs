using MonoTouch.UIKit;

namespace Circles.iOS
{
	public static class CustomColorExtensions
	{
		public static UIColor ToUIColor(this CustomColor colour)
		{
			return new UIColor(colour.Rgb.Red, colour.Rgb.Green, colour.Rgb.Blue, 255);
		}
	}
}
