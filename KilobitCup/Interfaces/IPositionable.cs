using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be positioned in 2D space.
	/// </summary>
	public interface IPositionable
	{
		/// <summary>
		/// Object position.
		/// </summary>
		Vector2 Position { get; set; }
	}
}
