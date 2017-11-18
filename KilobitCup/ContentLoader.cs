using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup
{
	/// <summary>
	/// Utility class used to load content.
	/// </summary>
	public static class ContentLoader
	{
		private static ContentManager content;

		/// <summary>
		/// Initializes the class.
		/// </summary>
		public static void Initialize(ContentManager c)
		{
			content = c;
		}

		/// <summary>
		/// Loads the given effect.
		/// </summary>
		public static Effect LoadEffect(string filename)
		{
			return content.Load<Effect>("Effects/" + filename);
		}

		/// <summary>
		/// Loads the given font.
		/// </summary>
		public static SpriteFont LoadFont(string filename)
		{
			return content.Load<SpriteFont>("Fonts/" + filename);
		}

		/// <summary>
		/// Loads the given texture.
		/// </summary>
		public static Texture2D LoadTexture(string filename)
		{
			return content.Load<Texture2D>("Textures/" + filename);
		}
	}
}
