using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Core
{
	/// <summary>
	/// Represents a renderable sprite. Sprites are assumed to use a centered origin.
	/// </summary>
	public class Sprite : Component2D
	{
		private Texture2D texture;
		private Vector2 origin;

		/// <summary>
		/// Constructs the sprite.
		/// </summary>
		public Sprite(string texture) :
			this(ContentLoader.LoadTexture(texture))
		{
		}

		/// <summary>
		/// Constructs the sprite.
		/// </summary>
		public Sprite(Texture2D texture)
		{
			this.texture = texture;

			origin = new Vector2(texture.Width, texture.Height) / 2;
		}

		/// <summary>
		/// Sprite width.
		/// </summary>
		public int Width => texture.Width;

		/// <summary>
		/// Sprite height.
		/// </summary>
		public int Height => texture.Height;

		/// <summary>
		/// Draws the sprite.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, Position, null, Color, Rotation, origin, Scale, SpriteEffects.None, 0);
		}
	}
}
