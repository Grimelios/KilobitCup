using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KilobitCup
{
	/// <summary>
	/// Main game class.
	/// </summary>
	public class MainGame : Game
	{
		private const int DefaultWidth = 400;
		private const int DefaultHeight = 300;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = DefaultWidth,
				PreferredBackBufferHeight = DefaultHeight
			};

			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		/// <summary>
		/// Initializes the game.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// Loads game content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		/// <summary>
		/// Updates the game.
		/// </summary>
		protected override void Update(GameTime gameTime)
		{
		}

		/// <summary>
		/// Draws the game.
		/// </summary>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
		}
	}
}
