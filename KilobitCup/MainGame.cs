using System;
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

		private static readonly Color DarkBackground = new Color(30, 30, 30);

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private BitListener bitListener;
		private PhysicsAccumulator accumulator;
		private Gif gif;

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
			GraphicsUtilities.Device = GraphicsDevice;
			PhysicsFactory.Initialize(world);

			bitListener = new BitListener();
			accumulator = new PhysicsAccumulator(world);
			gif = new Gif("Kappa4");

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

			long value = gameTime.ElapsedGameTime.Ticks;
			int test = (int)(dt * TimeSpan.TicksPerSecond);

			accumulator.Update(dt);
			gif.Update(dt);
		}

		/// <summary>
		/// Draws the game.
		/// </summary>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(DarkBackground);

			MouseState mouseState = Mouse.GetState();

			spriteBatch.Begin();
			//message.Draw(spriteBatch);
			gif.Position = new Vector2(mouseState.X, mouseState.Y);
			gif.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
