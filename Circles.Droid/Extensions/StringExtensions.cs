using Android.Graphics;

namespace Circles.Droid
{
	public static class StringExtensions
	{
		public static string Ellipsize(this string text, Paint paint, int pixelWidth)
		{
			const string ellipsis = "...";

			if (paint.MeasureText(text) > pixelWidth)
			{
				while (paint.MeasureText(text + ellipsis) > pixelWidth)
				{
					text = text.Remove(text.Length - 1);

					if (text == "")
						break;
				}

				if (text != "")
					text += ellipsis;
			}

			return text;
		}
	}
}
