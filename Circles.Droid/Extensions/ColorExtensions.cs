using Android.Graphics;

namespace Circles.Droid
{		
	public static class ColorExtensions
	{
		public static Color ToAndroid(this CustomColor colour)
		{
			return Color.Argb(255, colour.Rgb.Red, colour.Rgb.Green, colour.Rgb.Blue);
		}
	}
}
