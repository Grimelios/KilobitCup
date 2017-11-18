using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using KilobitCup.Interfaces;

namespace KilobitCup.Physics
{
	/// <summary>
	/// Helper class used to advance the physics world using an accumulator.
	/// </summary>
	public class PhysicsAccumulator : IDynamic
	{
		private const float PhysicsStep = 1f / 120;

		private World world;

		private float accumulator;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public PhysicsAccumulator(World world)
		{
			this.world = world;
		}

		/// <summary>
		/// Updates the class.
		/// </summary>
		public void Update(float dt)
		{
			accumulator += dt;

			while (accumulator > PhysicsStep)
			{
				world.Step(PhysicsStep);
				accumulator -= PhysicsStep;
			}
		}
	}
}
