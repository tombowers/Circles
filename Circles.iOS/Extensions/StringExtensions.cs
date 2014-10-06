using MonoTouch.UIKit;
using System;

namespace Circles.iOS
{
	public static class StringExtensions
	{
		public static string Ellipsize(this string text, UIView view, UIFont font, int pixelWidth)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException("text must not be null, empty, or whitespace");
			if (view == null)
				throw new ArgumentNullException("view");
			if (font == null)
				throw new ArgumentNullException("font");
			if (pixelWidth < 1)
				throw new ArgumentOutOfRangeException("pixelWidth", "pixelWidth must be greater than zero");

			const string ellipsis = "...";

			view.StringSize(text, font);

			if (view.StringSize(text, font).Width > pixelWidth)
			{
				while (view.StringSize(text + ellipsis, font).Width > pixelWidth)
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
