﻿using System;
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
			string json = await TwitchAPI.GetWebResponse("https://api.twitch.tv/kraken/channels/grimelios");
			string userID = ParseChannelID(json);

			TwitchAPI.GetAuthorizationToken();

			return;

			//channelID = await GetID("https://api.twitch.tv/kraken/channel", "Twitch_OAuth");

			JObject dataObject = new JObject(
				new JProperty("topics", "channel-bits-events-v1." + userID),
				new JProperty("auth_token", null)
			);

			JObject fullObject = new JObject(
				new JProperty("type", "LISTEN"),
				new JProperty("data", dataObject)
			);

			socket.Send(fullObject.ToString());
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
