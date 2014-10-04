using System;
using System.Globalization;

namespace Circles
{
	public static class ColourExtensions
	{
		public static CustomColor ContrastingColour(this CustomColor colour)
		{
			return new CustomColor
			{
				Rgb = new Rgb
				{
					Red = colour.Rgb.Red ^ 0x80,
					Green = colour.Rgb.Green ^ 0x80,
					Blue = colour.Rgb.Blue ^ 0x80
				}
			};
		}
	}
}
