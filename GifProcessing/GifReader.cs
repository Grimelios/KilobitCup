using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GifProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GifProcessing
{
	/// <summary>
	/// Content class for reading gif data.
	/// </summary>
	public class GifReader : ContentTypeReader<GifData>
	{
		/// <summary>
		/// Reads gif data.
		/// </summary>
		protected override GifData Read(ContentReader input, GifData existingInstance)
		{
			int frameCount = input.ReadInt32();

			IGraphicsDeviceService service =
				(IGraphicsDeviceService)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService));

			if (service == null)
			{
				throw new ContentLoadException();
			}

			GraphicsDevice graphicsDevice = service.GraphicsDevice;

			if (graphicsDevice == null)
			{
				throw new ContentLoadException();
			}

			Texture2D[] textures = new Texture2D[frameCount];

			for (int i = 0; i < frameCount; i++)
			{
				SurfaceFormat format = (SurfaceFormat)input.ReadInt32();

				int width = input.ReadInt32();
				int height = input.ReadInt32();
				int numberLevels = input.ReadInt32();

				textures[i] = new Texture2D(graphicsDevice, width, height, false, format);

				for (int j = 0; j < numberLevels; j++)
				{
					int count = input.ReadInt32();
					byte[] data = input.ReadBytes(count);

					textures[i].SetData(j, null, data, 0, data.Length);
				}
			}

			input.Close();

			return new GifData
			{
				Textures = textures
			};
		}
	}
}
