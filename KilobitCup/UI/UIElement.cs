using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.UI
{
	/// <summary>
	/// Represents an abstract item drawn on the screen. The item is assumed to not rotate or scale.
	/// </summary>
	public abstract class UIElement : IPositionable, IDynamic, IRenderable
	{
		private Vector2 position;

		/// <summary>
		/// Element position.
		/// </summary>
		public virtual Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}
		
		/// <summary>
		/// Updates the element.
		/// </summary>
		public virtual void Update(float dt)
		{
		}

		/// <summary>
		/// Draws the element.
		/// </summary>
		public virtual void Draw(SpriteBatch sb)
		{
		}
	}
}
