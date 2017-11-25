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
		public BitData(string username, string message, int bits, int totalBits)
		{
			Username = username;
			Message = message;
			Bits = bits;
			TotalBits = totalBits;
		}

		/// <summary>
		/// Username of the person who donated.
		/// </summary>
		public string Username { get; }

		/// <summary>
		/// Raw message (including cheers).
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// Total bits donated in the current message.
		/// </summary>
		public int Bits { get; }

		/// <summary>
		/// Total number of bits donated by the user.
		/// </summary>
		public int TotalBits { get; }
	}
}
