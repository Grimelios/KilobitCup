using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be colored or tinted.
	/// </summary>
	public interface IColorable
	{
		/// <summary>
		/// Object color.
		/// </summary>
		Color Color { get; set; }
	}
}
