using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Represents any object that can be drawn.
	/// </summary>
	public interface IRenderable
	{
		/// <summary>
		/// Draws the object.
		/// </summary>
		void Draw(SpriteBatch sb);
	}
}
