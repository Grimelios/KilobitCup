using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GifProcessing
{
	/// <summary>
	/// Static helper class used to pull bit information from a gif.
	/// </summary>
	public static class Quantizer
	{
		/// <summary>
		/// Gets byte data for a gif.
		/// </summary>
		public static unsafe byte[] Quantize(Image source)
		{
			int height = source.Height;
			int width = source.Width;

			Rectangle rect = new Rectangle(0, 0, width, height);
			Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb);

			using (Graphics graphics = Graphics.FromImage(image))
			{
				graphics.PageUnit = GraphicsUnit.Pixel;
				graphics.DrawImageUnscaled(source, rect);
			}

			BitmapData bitmapdata = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			byte* numPtr = (byte*)bitmapdata.Scan0.ToPointer();
			int index = 0;
			byte[] buffer = new byte[source.Width * source.Height * 4];

			for (int i = 0; i < source.Width; i++)
			{
				for (int j = 0; j < source.Height; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						buffer[index] = numPtr[index];
						index++;
					}
				}
			}
			// I'm not sure why swapping these colors works, but swapping colors in this way works. Swapping colors here avoids having to
			// do it when constructing gifs.
			for (int k = 0; k < buffer.Length; k += 4)
			{
				byte temp = buffer[k];

				buffer[k] = buffer[k + 2];
				buffer[k + 2] = temp;
			}

			image.UnlockBits(bitmapdata);

			return buffer;
		}
	}
}
