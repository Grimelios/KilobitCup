using FarseerPhysics.Dynamics;
using KilobitCup.Core;
using KilobitCup.Entities;
using KilobitCup.Physics;
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
		private const int Gravity = 25;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private BitListener bitListener;
		private PhysicsAccumulator accumulator;

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
			World world = new World(new Vector2(0, Gravity));

			ContentLoader.Initialize(Content);
			PhysicsFactory.Initialize(world);

			bitListener = new BitListener();
			accumulator = new PhysicsAccumulator(world);

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
			float dt = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

			accumulator.Update(dt);
		}

		/// <summary>
		/// Draws the game.
		/// </summary>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			MouseState mouseState = Mouse.GetState();
			ScrollingMessage message = new ScrollingMessage("My name is Lucidus16 cheer100 and I'm super rich kappa1000")
			{
				Position = new Vector2(mouseState.X, mouseState.Y)
			};

			spriteBatch.Begin();
			message.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
