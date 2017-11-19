using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using KilobitCup.Twitch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Enumeration storing default cheer types (custom cheers not included).
	/// </summary>
	public enum CheerTypes
	{
		Cheer,//
		RIPCheer,//
		Kappa,//
		Kreygasm,//
		SwiftRage,//
		FourHead,//
		PJSalt,//
		MrDestructoid,//
		TriHard,//
		NotLikeThis,//
		FailFish,//
		VoHiYo,//
		Streamlabs,//
		Muxy,//
		BDay,//
		BitBoss,//
		DoodleCheer,//
		Invalid
	}

	/// <summary>
	/// Represents an animated cheermote with physics data.
	/// </summary>
	public class Cheer : Entity
	{
		private const int VerticalOffset = 8;

		private static readonly int[] BitThresholds =
		{
			1,
			100,
			1000,
			5000,
			10000
		};

		/// <summary>
		/// Static initializer for the class.
		/// </summary>
		static Cheer()
		{
			GetCheermotes();
		}

		/// <summary>
		/// Pulls cheermote data from Twitch and loads images.
		/// </summary>
		private static async void GetCheermotes()
		{
			string json = await TwitchAPI.GetWebResponse("https://api.twitch.tv/kraken/bits/actions", true);

			Type cheerType = typeof(CheerTypes);

			// The last entry in CheerTypes is an invalid marker.
			int cheerCount = Enum.GetValues(cheerType).Length - 1;

			CheerData[] dataArray = new CheerData[cheerCount];
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

				string[] urlArray = new string[BitThresholds.Length];

				for (int j = 0; j < BitThresholds.Length; j++)
				{
					urlArray[j] = tiers[j]["images"]["dark"]["animated"].First.ToString();
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

		private Sprite sprite;
		private Vector2 positionOffset;

		/// <summary>
		/// Constructs the cheer. Bit value is used to determine which image to use.
		/// </summary>
		public Cheer(CheerTypes type, int bitValue)
		{
			sprite = new Sprite("Kappa");
			positionOffset = new Vector2(Width / 2, VerticalOffset);
		}

		/// <summary>
		/// Cheer width (based on the active cheermote).
		/// </summary>
		public int Width => 18;

		/// <summary>
		/// Cheer position.
		/// </summary>
		public override Vector2 Position
		{
			set { sprite.Position = value + positionOffset; }
		}

		/// <summary>
		/// Draws the sprite.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			sprite.Draw(sb);
		}

		/// <summary>
		/// Data class used when pulling cheer data from Twitch.
		/// </summary>
		private class CheerData
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
}
