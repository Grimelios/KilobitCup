using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.UI
{
	/// <summary>
	/// Displays the name of a donator along with associated bit information.
	/// </summary>
	public class DonatorName : UIElement
	{
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public DonatorName(string name)
		{
			SpriteFont font = ContentLoader.LoadFont("Donator");
		}

		/// <summary>
		/// Element position.
		/// </summary>
		public override Vector2 Position { get; set; }
	}
}
