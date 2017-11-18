using KilobitCup.Core;
using KilobitCup.Entities;
using KilobitCup.Twitch;
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
		private BitListener bitListener;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = DefaultWidth,
				PreferredBackBufferHeight = DefaultHeight
			};

			Content.RootDirectory = "Content";
			Window.Title = "Kilobit Cup";
			Window.Position = new Point(200);
		}

		/// <summary>
		/// Initializes the game.
		/// </summary>
		protected override void Initialize()
		{
			ContentLoader.Initialize(Content);

			bitListener = new BitListener();

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
			GraphicsDevice.Clear(Color.Black);

			MouseState mouseState = Mouse.GetState();
			ScrollingMessage message = new ScrollingMessage("hello cheer1 one cheer000001 two")
			{
				Position = new Vector2(mouseState.X, mouseState.Y)
			};

			spriteBatch.Begin();
			message.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
