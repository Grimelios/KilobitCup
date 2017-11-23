using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KilobitCup
{
	/// <summary>
	/// Enumeration storing ease types.
	/// </summary>
	public enum EaseTypes
	{
		EaseInBack,
		EaseOutBack,
	}

	/// <summary>
	/// Utility class storing ease functions.
	/// </summary>
	public static class EaseFunctions
	{
		/// <summary>
		/// Computes easing for the given ease type.
		/// </summary>
		public static float Ease(float amount, EaseTypes easeType)
		{
			switch (easeType)
			{
				case EaseTypes.EaseInBack:
					return amount * amount - amount * (float)Math.Sin(amount * MathHelper.Pi);

				case EaseTypes.EaseOutBack:
					float f = 1 - amount;

					return 1 - (f * f - f * (float)Math.Sin(f * MathHelper.Pi));
			}

			return amount;
		}
	}
}
