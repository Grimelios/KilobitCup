using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup
{
	/// <summary>
	/// Utility class storing common functions used in the program.
	/// </summary>
	public static class CoreFunctions
	{
		/// <summary>
		/// Counts the items in the given enumeration.
		/// </summary>
		public static int EnumCount<T>()
		{
			return Enum.GetValues(typeof(T)).Length;
		}
	}
}
