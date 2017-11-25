using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KilobitCup.Forms;

namespace KilobitCup.Twitch
{
	/// <summary>
	/// Utility class for communicating with the Twitch API.
	/// </summary>
	public static class TwitchAPI
	{
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

			request.Headers.Add("Client-ID", Program.ClientID);
			request.Headers.Add("Authorization", "OAuth " + Environment.GetEnvironmentVariable("Kilobit_Secret"));

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				return reader.ReadToEnd();
			}
		}

		/// <summary>
		/// Gets an authorization token by displaying a temporary form allowing users to log in.
		/// </summary>
		public static string GetAuthorizationToken()
		{
			AuthorizationForm form = new AuthorizationForm();
			form.ShowDialog();

			// This indicates that the user authorized the program.
			if (form.DialogResult == DialogResult.OK)
			{
				return form.AccessToken;
			}

			return null;
		}
	}
}
