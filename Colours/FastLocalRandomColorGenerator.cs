using System;

namespace Circles
{
	public class FastLocalRandomColorGenerator : IRandomColourGenerator
	{
		private readonly Random _random;

		public FastLocalRandomColorGenerator()
		{
			_random	= new Random();
		}

		public CustomColor GetNext()
		{
			var red = _random.Next(255);
			var green = _random.Next(255);
			var blue = _random.Next(255);

			return new CustomColor
			{
				Rgb = new Rgb
				{
					Red = red,
					Green = green,
					Blue = blue
				},
				Title = string.Format("rgb({0},{1},{2})", red, green, blue)
			};
		}

		public void GetNextAsync(Action<CustomColor> callback)
		{
			callback(GetNext());
		}
	}
}

