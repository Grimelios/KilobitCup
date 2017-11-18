using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace KilobitCup.Twitch
{
	/// <summary>
	/// Helper class used to listen for bit messages from Twitch.
	/// </summary>
	public class BitListener
	{
		private const string ClientID = "zulz2i7hm8u5vofu2095940hrq81nx";

		private WebSocket socket;

		private string channelID;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public BitListener()
		{
			socket = new WebSocket("wss://pubsub-edge.twitch.tv");
			socket.OnOpen += OnOpen;
			socket.OnMessage += OnMessage;
			socket.EmitOnPing = true;
			socket.Connect();
		}

		/// <summary>
		/// Called when the socket is opened.
		/// </summary>
		private async void OnOpen(object sender, EventArgs e)
		{
			string userID = await GetID("https://api.twitch.tv/kraken/channels/grimelios", "Kilobit_Secret");

			return;

			channelID = await GetID("https://api.twitch.tv/kraken/channel", "Twitch_OAuth");

			JObject dataObject = new JObject
			{
				{ "topics", new JArray("channel-bits-events-v1." + channelID) },
				{ "auth_token", channelID }
			};

			JObject jObject = new JObject
			{
				{ "type", "LISTEN" },
				{ "data", dataObject }
			};

			socket.Send(jObject.ToString());
		}

		/// <summary>
		/// Gets an ID from Twitch based on the given data.
		/// </summary>
		private async Task<string> GetID(string url, string environmentVariable)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.Headers.Add("Client-ID", ClientID);
			request.Headers.Add("Authorization", "OAuth " + Environment.GetEnvironmentVariable(environmentVariable));

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				return ParseChannelID(reader.ReadToEnd());
			}
		}

		/// <summary>
		/// Parses a channel ID from the given Json block.
		/// </summary>
		private string ParseChannelID(string json)
		{
			using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
			{
				while (reader.Read())
				{
					if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "_id")
					{
						reader.Read();

						return reader.Value.ToString();
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Called when a message is received.
		/// </summary>
		private void OnMessage(object sender, MessageEventArgs e)
		{
		}
	}
}
