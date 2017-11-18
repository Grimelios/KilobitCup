using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be updated.
	/// </summary>
	public interface IDynamic
	{
		/// <summary>
		/// Updates the object.
		/// </summary>
		void Update(float dt);
	}
}
