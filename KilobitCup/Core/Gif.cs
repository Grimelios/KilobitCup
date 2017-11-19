using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GifProcessing;
//using GifProcessing;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Core
{
	/// <summary>
	/// Represents a renderable gif.
	/// </summary>
	public class Gif : Component2D
	{
		private long currentTick;
		private bool stopped;

		private Texture2D[] textures;
		
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public Gif(string filename)
		{
			GifData data = ContentLoader.LoadCustom<GifData>(filename);

			textures = ContentLoader.LoadCustom<GifData>(filename).Textures;
		}

		/// <summary>
		/// Gets a texture for the current frame.
		/// </summary>
		public Texture2D CurrentTexture => textures[CurrentFrame];

		/// <summary>
		/// Gets the frame at the given index.
		/// </summary>
		public Texture2D this[int index] => textures[index];

		/// <summary>
		/// Whether the gif is paused.
		/// </summary>
		public bool Paused { get; set; }

		/// <summary>
		/// Current frame.
		/// </summary>
		public int CurrentFrame { get; private set; }

		/// <summary>
		/// Frame count.
		/// </summary>
		public int FrameCount => textures.Length;

		/// <summary>
		/// Gif width.
		/// </summary>
		public int Width => textures[0].Width;

		/// <summary>
		/// Gif height.
		/// </summary>
		public int Height => textures[0].Height;

		/// <summary>
		/// Plays the gif.
		/// </summary>
		public void Play()
		{
			CurrentFrame = 0;
			Paused = false;
			stopped = false;
		}

		/// <summary>
		/// Stops the gif.
		/// </summary>
		public void Stop()
		{
			CurrentFrame = 0;
			Paused = false;
			stopped = true;
		}

		/// <summary>
		/// Updates the gif.
		/// </summary>
		public override void Update(float dt)
		{
			if (Paused || stopped)
			{
				return;
			}

			currentTick += (long)(dt * TimeSpan.TicksPerSecond);

			if (currentTick >= 0xf4240L)
			{
				currentTick = 0;
				CurrentFrame = CurrentFrame == textures.Length - 1 ? 0 : ++CurrentFrame;
			}
		}

		/// <summary>
		/// Draws the gif.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
		}
	}
}
