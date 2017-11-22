using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using KilobitCup.Entities;

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

		/// <summary>
		/// Creates a rectangular body.
		/// </summary>
		public static Body CreateRectangle(float width, float height, Units units, BodyType bodyType, Entity entity)
		{
			if (units == Units.Pixels)
			{
				width = PhysicsConvert.ToMeters(width);
				height = PhysicsConvert.ToMeters(height);
			}

			Body body = BodyFactory.CreateRectangle(world, width, height, 1);
			body.BodyType = bodyType;
			body.UserData = entity;

			return body;
		}
	}
}
