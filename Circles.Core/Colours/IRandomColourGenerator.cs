using Circles;
using System;

namespace Circles
{
	public interface IRandomColourGenerator
	{
		CustomColor GetNext();
		void GetNextAsync(Action<CustomColor> callback);
	}
}
