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
		Cheer,
		RIPCheer,
		Kappa,
		Kreygasm,
		SwiftRage,
		FourHead,
		PJSalt,
		MrDestructoid,
		TriHard,
		NotLikeThis,
		FailFish,
		VoHiYo,
		Streamlabs,
		Muxy,
		BDay,
		BitBoss,
		DoodleCheer,
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
	}
}
