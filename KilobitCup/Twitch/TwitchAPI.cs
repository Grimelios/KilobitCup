using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Twitch
{
	/// <summary>
	/// Utility class for communicating with the Twitch API.
	/// </summary>
	public static class TwitchAPI
	{
		private const string ClientID = "zulz2i7hm8u5vofu2095940hrq81nx";

		/// <summary>
		/// Sends a web request to Twitch using the given URL. The response Json is returned.
		/// </summary>
		public static async Task<string> GetWebResponse(string url, bool setAccept = false)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";

			// This allows cheermote data to be retrieved properly.
			if (setAccept)
			{
				request.Accept = "application/vnd.twitchtv.v5+json";
			}

			request.Headers.Add("Client-ID", ClientID);
			request.Headers.Add("Authorization", "OAuth " + Environment.GetEnvironmentVariable("Kilobit_Secret"));

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
