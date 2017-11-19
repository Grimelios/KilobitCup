using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GifProcessing
{
	/// <summary>
	/// Data class used for individual frames of a gif.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public class TextureData
	{
		/// <summary>
		/// Texture surface format.
		/// </summary>
		public SurfaceFormat SurfaceFormat { get; set; }

		/// <summary>
		/// Texture width.
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Texture height.
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Texture levels.
		/// </summary>
		public int Levels { get; set; }

		/// <summary>
		/// Texture data.
		/// </summary>
		public byte[] Data { get; set; }
	}
}
