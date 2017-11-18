using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Represents an animated cheermote with physics data.
	/// </summary>
	public class Cheer : Entity
	{
		private const int VerticalOffset = 8;

		private Sprite sprite;
		private Vector2 positionOffset;

		/// <summary>
		/// Constructs the cheer. Bit value is used to determine which image to use.
		/// </summary>
		public Cheer(int bitValue)
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
