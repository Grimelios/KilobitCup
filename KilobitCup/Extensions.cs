using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup
{
	/// <summary>
	/// Static class storing extension methods for the project.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Checks whether the string contains the given value at the given index.
		/// </summary>
		public static bool ContainsAt(this string source, string value, int index)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (source[index + i] != value[i])
				{
					return false;
				}
			}

			return true;
		}
	}
}
