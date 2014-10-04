using System;
using System.Net.Http;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace Circles
{
	public static class Serialization
	{
		/// <summary>
		/// Deserializes xml from a remote URL into an instance of the supplied type.
		/// </summary>
		public static async Task<T> DeserializeFromUrl<T>(string url, int timeoutSeconds)
		{
			if (string.IsNullOrWhiteSpace(url))
				throw new ArgumentException("url must not be null, empty, or whitespace");

			Uri endPoint;
			if (!Uri.TryCreate(url, UriKind.Absolute, out endPoint))
				throw new ArgumentException("Invalid url specified");

			var serialiser = new XmlSerializer(typeof(T));

			var client = new HttpClient();
			client.BaseAddress = endPoint;
			client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

			try
			{
				var response = await client.GetAsync("");

				return (T)serialiser.Deserialize(response.Content.ReadAsStreamAsync().Result);
			} 
			catch (Exception e)
			{
				throw new SerializationException("An error has occurred. See inner exception for details.", e);
			}
		}

		public class SerializationException : Exception
		{
			public SerializationException() {}
			public SerializationException(string message) : base(message) {}
			public SerializationException(string message, Exception inner) : base(message, inner) {}
		}
	}
}

