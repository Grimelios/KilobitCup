using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;

namespace KilobitCup.Physics
{
	/// <summary>
	/// Enumeration storing physics units.
	/// </summary>
	public enum Units
	{
		Pixels,
		Meter
	}

	/// <summary>
	/// Utility class used to create physics objects.
	/// </summary>
	public static class PhysicsFactory
	{
		private static World world;

		/// <summary>
		/// Initializes the class.
		/// </summary>
		public static void Initialize(World w)
		{
			world = w;
		}
	}
}
