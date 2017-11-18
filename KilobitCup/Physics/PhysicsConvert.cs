using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KilobitCup.Physics
{
	/// <summary>
	/// Utility class used to convert physics values.
	/// </summary>
	public static class PhysicsConvert
	{
		private const int PixelsPerMeter = 32;

		/// <summary>
		/// Converts the given value to meters.
		/// </summary>
		public static float ToMeters(float value)
		{
			return value / PixelsPerMeter;
		}

		/// <summary>
		/// Converts the given value to pixels.
		/// </summary>
		public static float ToPixels(float value)
		{
			return value * PixelsPerMeter;
		}

		/// <summary>
		/// Converts the given value to meters.
		/// </summary>
		public static Vector2 ToMeters(Vector2 value)
		{
			return value / PixelsPerMeter;
		}

		/// <summary>
		/// Converts the given value to pixels.
		/// </summary>
		public static Vector2 ToPixels(Vector2 value)
		{
			return value * PixelsPerMeter;
		}
	}
}
