using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Entities;

namespace GifRetriever
{
	/// <summary>
	/// Data class used when pulling cheer data from Twitch.
	/// </summary>
	public class CheerData
	{
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public CheerData(CheerTypes prefix, string[] urlArray)
		{
			Prefix = prefix;
			UrlArray = urlArray;
		}

		/// <summary>
		/// Prefix of the cheer (from the CheerTypes enumeration).
		/// </summary>
		public CheerTypes Prefix { get; }

		/// <summary>
		/// Array of URLs pointing to the dark, animated versions of the cheermote (one entry per bit threshold).
		/// </summary>
		public string[] UrlArray { get; }
	}
}
