using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace GifProcessing
{
	/// <summary>
	/// Content importer for gifs.
	/// </summary>
	[ContentImporter(".xgif", DisplayName = "Gif Importer", DefaultProcessor = "GifProcessor")]
	public class GifImporter : ContentImporter<GifData>
	{
		/// <summary>
		/// Imports a gif.
		/// </summary>
		public override GifData Import(string filename, ContentImporterContext context)
		{
			Image source = Image.FromFile(filename);
			FrameDimension dimension = new FrameDimension(source.FrameDimensionsList[0]);

			int frameCount = source.GetFrameCount(dimension);

			TextureData[] frames = new TextureData[frameCount];

			for (int i = 0; i < frameCount; i++)
			{
				source.SelectActiveFrame(dimension, i);
				frames[i] = new TextureData
				{
					SurfaceFormat = SurfaceFormat.Color,
					Width = source.Width,
					Height = source.Height,
					Levels = 1,
					Data = Quantizer.Quantize(source)
				};
			}

			source.Dispose();

			return new GifData
			{
				Frames = frames
			};
		}

	}

}
