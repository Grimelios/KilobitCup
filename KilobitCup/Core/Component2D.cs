using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Core
{
	/// <summary>
	/// Represents an abstract 2D component. Used for images and text.
	/// </summary>
	public abstract class Component2D : IPositionable, IRotatable, IColorable, IDynamic, IRenderable
	{
		/// <summary>
		/// Component position.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Component rotation.
		/// </summary>
		public float Rotation { get; set; }

		/// <summary>
		/// Component color.
		/// </summary>
		public Color Color { get; set; } = Color.White;

		/// <summary>
		/// Updates the component.
		/// </summary>
		public virtual void Update(float dt)
		{
		}

		/// <summary>
		/// Draws the component.
		/// </summary>
		public abstract void Draw(SpriteBatch sb);
	}
}
