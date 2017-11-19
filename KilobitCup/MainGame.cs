using System;
using FarseerPhysics.Dynamics;
using KilobitCup.Core;
using KilobitCup.Data;
using KilobitCup.Entities;
using KilobitCup.Interfaces;
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
		private const int Gravity = 25;

		private static readonly Color DarkBackground = new Color(20, 20, 20);

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private BitListener bitListener;
		private PhysicsAccumulator accumulator;
		private Scene scene;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = Resolution.Width,
				PreferredBackBufferHeight = Resolution.Height
			};

			Content.RootDirectory = "Content";
			Window.Title = "Kilobit Cup";
			Window.Position = new Point(200);
			IsMouseVisible = true;
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
			scene = new Scene();

			MessageSystem.Send(MessageTypes.Bits, new BitData("cheer10000 Hello vohiyo5000", "Terra21", 5100));

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
			scene.Update(dt);
		}

		/// <summary>
		/// Draws the game.
		/// </summary>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(DarkBackground);
			
			spriteBatch.Begin();
			scene.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}
