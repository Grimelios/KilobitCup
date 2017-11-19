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
		public SurfaceFormat SurfaceFormat { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Levels { get; set; }
		public byte[] Data { get; set; }
	}
}
