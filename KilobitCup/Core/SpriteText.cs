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
	/// Represents renderable text. Text is assumed to use the default origin.
	/// </summary>
	public class SpriteText : Component2D
	{
		private SpriteFont font;

		private string value;

		/// <summary>
		/// Constructs the text.
		/// </summary>
		public SpriteText(string font, string value) :
			this(ContentLoader.LoadFont(font), value)
		{
		}

		/// <summary>
		/// Constructs the text.
		/// </summary>
		public SpriteText(SpriteFont font, string value)
		{
			this.font = font;
			this.value = value;
		}

		/// <summary>
		/// Draws the text.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			sb.DrawString(font, value, Position, Color, Rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
		}
	}
}
