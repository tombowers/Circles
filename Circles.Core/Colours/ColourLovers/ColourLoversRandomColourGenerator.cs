using System;

namespace Circles
{
	public class ColourLoversRandomColourGenerator : IRandomColourGenerator
	{
		private const string ApiUrl = "http://www.colourlovers.com/api/colors/random";
		private readonly IRandomColourGenerator _backupRandomColourGenerator;

		public ColourLoversRandomColourGenerator(IRandomColourGenerator backupRandomColourGenerator)
		{
			if (backupRandomColourGenerator == null)
				throw new ArgumentNullException("backupRandomColourGenerator");

			_backupRandomColourGenerator = backupRandomColourGenerator;
		}

		public CustomColor GetNext()
		{
			try
			{
				var response = Serialization.DeserializeFromUrl<RandomColourResponse>(ApiUrl, 5).Result;

				return response.Color;
			}
			catch (Serialization.SerializationException)
			{
				return _backupRandomColourGenerator.GetNext();
			}
		}
			
		public async void GetNextAsync(Action<CustomColor> callback)
		{
			try
			{
				var response = await Serialization.DeserializeFromUrl<RandomColourResponse>(ApiUrl, 5);
				callback(response.Color);
			}
			catch (Serialization.SerializationException)
			{
				callback(_backupRandomColourGenerator.GetNext());
			}
		}
	}
}
