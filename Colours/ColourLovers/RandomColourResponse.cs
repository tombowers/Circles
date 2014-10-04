using System;
using System.Xml.Serialization;

namespace Circles
{
	[XmlRoot(ElementName="rgb")]
	public class Rgb
	{
		[XmlElement(ElementName="red")]
		public int Red { get; set; }

		[XmlElement(ElementName="green")]
		public int Green { get; set; }

		[XmlElement(ElementName="blue")]
		public int Blue { get; set; }
	}

	[XmlRoot(ElementName="hsv")]
	public class Hsv
	{
		[XmlElement(ElementName="hue")]
		public int Hue { get; set; }

		[XmlElement(ElementName="saturation")]
		public int Saturation { get; set; }

		[XmlElement(ElementName="value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName="color")]
	public class CustomColor {
		[XmlElement(ElementName="id")]
		public String Id { get; set; }

		[XmlElement(ElementName="title")]
		public String Title { get; set; }

		[XmlElement(ElementName="userName")]
		public String UserName { get; set; }

		[XmlElement(ElementName="numViews")]
		public int NumViews { get; set; }

		[XmlElement(ElementName="numVotes")]
		public int NumVotes { get; set; }

		[XmlElement(ElementName="numComments")]
		public int NumComments { get; set; }

		[XmlElement(ElementName="numHearts")]
		public int NumHearts { get; set; }

		[XmlElement(ElementName="rank")]
		public int Rank { get; set; }

		[XmlElement(ElementName="dateCreated")]
		public string DateCreated { get; set; }

		[XmlElement(ElementName="hex")]
		public String Hex { get; set; }

		[XmlElement(ElementName="rgb")]
		public Rgb Rgb { get; set; }

		[XmlElement(ElementName="hsv")]
		public Hsv Hsv { get; set; }

		[XmlElement(ElementName="description")]
		public String Description { get; set; }

		[XmlElement(ElementName="url")]
		public String Url { get; set; }

		[XmlElement(ElementName="imageUrl")]
		public String ImageUrl { get; set; }

		[XmlElement(ElementName="badgeUrl")]
		public String BadgeUrl { get; set; }

		[XmlElement(ElementName="apiUrl")]
		public String ApiUrl { get; set; }
	}

	[XmlRoot(ElementName="colors")]
	public class RandomColourResponse
	{
		[XmlElement(ElementName="color")]
		public CustomColor Color { get; set; }

		[XmlAttribute(AttributeName="numResults")]
		public int NumResults { get; set; }

		[XmlAttribute(AttributeName="totalResults")]
		public int TotalResults { get; set; }
	}

}
