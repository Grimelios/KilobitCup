using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KilobitCup;
using KilobitCup.Entities;
using KilobitCup.Twitch;
using Newtonsoft.Json.Linq;

namespace GifRetriever
{
	/// <summary>
	/// Main program class for the project.
	/// </summary>
	public class Program
	{
		private const int ThresholdCount = 5;

		private const string GifDirectory = @"C:\Users\Mark\Documents\visual studio 2015\Projects\KilobitCup\KilobitCup\Content\Gifs\";

		private static CheerData[] dataArray;

		/// <summary>
		/// Main function for the project.
		/// </summary>
		public static void Main(string[] args)
		{
			Console.WriteLine("Retrieving cheermotes from Twitch...\n");

			GetCheermotes();
			DownloadGifs();

			Console.WriteLine($"\nDone ({dataArray.Length} cheermotes).");
			Console.ReadLine();
		}

		/// <summary>
		/// Pulls cheermote data from Twitch and loads images.
		/// </summary>
		private static async void GetCheermotes()
		{
			string json = await TwitchAPI.GetWebResponse("https://api.twitch.tv/kraken/bits/actions", true);

			// The last entry in CheerTypes is an invalid marker.
			int cheerCount = CoreFunctions.EnumCount<CheerTypes>() - 1;

			dataArray = new CheerData[cheerCount];

			JObject root = JObject.Parse(json);
			JArray cheerArray = root["actions"].Value<JArray>();

			for (int i = 0; i < cheerCount; i++)
			{
				JObject cheerObject = cheerArray[i].Value<JObject>();
				JArray tiers = cheerObject["tiers"].Value<JArray>();

				CheerTypes prefix;

				string rawPrefix = cheerObject["prefix"].ToString();

				if (!Enum.TryParse(rawPrefix, out prefix))
				{
					prefix = ParseUnmatchingPrefix(rawPrefix);
				}

				string[] urlArray = new string[ThresholdCount];

				for (int j = 0; j < ThresholdCount; j++)
				{
					urlArray[j] = tiers[j]["images"]["dark"]["animated"]["1"].ToString();
				}

				dataArray[i] = new CheerData(prefix, urlArray);
			}
		}

		/// <summary>
		/// Parses a cheer prefix that doesn't exactly match with its enumeration value.
		/// </summary>
		private static CheerTypes ParseUnmatchingPrefix(string rawPrefix)
		{
			switch (rawPrefix)
			{
				case "4Head": return CheerTypes.FourHead;
				case "bday": return CheerTypes.BDay;
			}

			return CheerTypes.Invalid;
		}

		/// <summary>
		/// Downloads and saves gifs using cheer data retrieved from Twitch.
		/// </summary>
		private static void DownloadGifs()
		{
			foreach (CheerData data in dataArray)
			{
				string prefix = data.Prefix.ToString();

				for (int i = 0; i < ThresholdCount; i++)
				{
					// See https://stackoverflow.com/questions/15712872/download-animated-gif.
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(data.UrlArray[i]);
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();

					string filename = prefix + i + ".xgif";

					using (Stream stream = response.GetResponseStream())
					using (FileStream fileStream = File.Create(GifDirectory + filename))
					{
						stream.CopyTo(fileStream);

						Console.WriteLine($"Saved {filename}.");
					}
				}
			}
		}
	}
}
