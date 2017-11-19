using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GifProcessing
{
	/// <summary>
	/// Data class used for building gif content.
	/// </summary>
	public class GifData
	{
		/// <summary>
		/// Array of frame data.
		/// </summary>
		public TextureData[] Frames { get; set; }
	}
}
