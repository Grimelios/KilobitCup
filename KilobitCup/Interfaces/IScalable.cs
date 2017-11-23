using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be scaled.
	/// </summary>
	public interface IScalable
	{
		/// <summary>
		/// Object scale.
		/// </summary>
		float Scale { get; set; }
	}
}
