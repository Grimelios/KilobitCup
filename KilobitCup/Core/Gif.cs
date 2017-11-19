using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GifProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Core
{
	/// <summary>
	/// Represents a renderable gif.
	/// </summary>
	public class Gif : Component2D
	{
		private const float Framerate = 1f / 25;

		private int currentFrame;
		private float elapsed;

		private Texture2D[] textures;
		private Vector2 origin;
		
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public Gif(string filename)
		{
			CreateTextures(ContentLoader.LoadCustom<GifData>("Gifs/" + filename).Frames);

			// This assumes all frames are the same size (which I think has to be true anyway).
			Texture2D frame = textures[0];
			origin = new Vector2(frame.Width, frame.Height) / 2;
		}

		/// <summary>
		/// Gif width.
		/// </summary>
		public int Width => textures[0].Width;

		/// <summary>
		/// Gif height.
		/// </summary>
		public int Height => textures[0].Height;

		/// <summary>
		/// Creates textures from the given frame array.
		/// </summary>
		private void CreateTextures(TextureData[] frames)
		{
			textures = new Texture2D[frames.Length];

			for (int i = 0; i < frames.Length; i++)
			{
				TextureData textureData = frames[i];

				textures[i] = new Texture2D(GraphicsUtilities.Device, textureData.Width, textureData.Height, false,
					textureData.SurfaceFormat);

				for (int j = 0; j < textureData.Levels; j++)
				{
					byte[] data = textureData.Data;
					
					// I'm not sure why swapping these colors works, but swapping colors in this way works.
					for (int k = 0; k < data.Length; k += 4)
					{
						byte temp = data[k];

						data[k] = data[k + 2];
						data[k + 2] = temp;
					}

					textures[i].SetData(j, null, data, 0, data.Length);
				}
			}
		}

		/// <summary>
		/// Updates the gif.
		/// </summary>
		public override void Update(float dt)
		{
			elapsed += dt;
			
			while (elapsed >= Framerate)
			{
				elapsed -= Framerate;
				currentFrame = currentFrame == textures.Length - 1 ? 0 : ++currentFrame;
			}
		}

		/// <summary>
		/// Draws the gif.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			sb.Draw(textures[currentFrame], Position, null, Color, Rotation, origin, 1, SpriteEffects.None, 0);
		}
	}
}
