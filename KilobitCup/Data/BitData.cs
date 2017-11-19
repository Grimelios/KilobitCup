using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Data
{
	/// <summary>
	/// Data class sent in bit messages.
	/// </summary>
	public class BitData
	{
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public BitData(string message, string username, int totalBits)
		{
			Message = message;
			Username = username;
			TotalBits = totalBits;
		}

		/// <summary>
		/// Raw message (including cheers).
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// Username of the person who donated.
		/// </summary>
		public string Username { get; }

		/// <summary>
		/// Total number of bits donated by the user.
		/// </summary>
		public int TotalBits { get; }
	}
}
