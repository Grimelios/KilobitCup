using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using KilobitCup.Entities;
using Microsoft.Xna.Framework;

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
		/// Creates an empty static body.
		/// </summary>
		public static Body CreateBody()
		{
			return BodyFactory.CreateBody(world);
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

		/// <summary>
		/// Attaches an edge to the given body.
		/// </summary>
		public static void AttachEdge(Body body, Vector2 start, Vector2 end, Units units)
		{
			if (units == Units.Pixels)
			{
				start = PhysicsConvert.ToMeters(start);
				end = PhysicsConvert.ToMeters(end);
			}

			FixtureFactory.AttachEdge(start, end, body);
		}
	}
}
