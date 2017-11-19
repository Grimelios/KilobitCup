using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup
{
	/// <summary>
	/// Utility class containing graphics functions.
	/// </summary>
	public static class GraphicsUtilities
	{
		/// <summary>
		/// Reference to the graphics device.
		/// </summary>
		public static GraphicsDevice Device { get; set; }

		/// <summary>
		/// Creates a render target with the given dimensions.
		/// </summary>
		public static RenderTarget2D CreateRenderTarget(int width, int height)
		{
			return new RenderTarget2D(Device, width, height);
		}
	}
}
