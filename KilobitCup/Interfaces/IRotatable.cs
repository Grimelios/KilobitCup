using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be rotated.
	/// </summary>
	public interface IRotatable
	{
		/// <summary>
		/// Object rotation.
		/// </summary>
		float Rotation { get; set; }
	}
}
