using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Represents an abstract entity. As entity is anything that can be updated and drawn on the screen.
	/// </summary>
	public abstract class Entity : IPositionable, IDynamic, IRenderable
	{
		/// <summary>
		/// Entity position.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Updates the entity.
		/// </summary>
		public virtual void Update(float dt)
		{
		}

		/// <summary>
		/// Draws the entity.
		/// </summary>
		public virtual void Draw(SpriteBatch sb)
		{
		}
	}
}
