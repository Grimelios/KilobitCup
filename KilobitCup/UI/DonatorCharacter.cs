using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using KilobitCup.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.UI
{
	/// <summary>
	/// Represents a single character in the donator string.
	/// </summary>
	public class DonatorCharacter : UIElement
	{
		private static int offset;
		private static int revealTime;

		/// <summary>
		/// Static initialize for the class.
		/// </summary>
		static DonatorCharacter()
		{
			object[] values = PropertyLoader.Load("Properties.txt", new []
			{
				new FieldData("Donator.Offset", FieldTypes.Integer),
				new FieldData("Donator.Reveal.Time", FieldTypes.Integer),
			});

			offset = (int)values[0];
			revealTime = (int)values[1];
		}

		private Timer timer;
		private Vector2 basePosition;
		private Vector2 targetPosition;
		private SpriteText spriteText;

		private bool entering;

		/// <summary>
		/// Constructs the element.
		/// </summary>
		public DonatorCharacter(char character, Vector2 position)
		{
			spriteText = new SpriteText("Donator", character.ToString())
			{
				Position = position
			};

			basePosition = position;
			targetPosition = new Vector2(position.X, Resolution.Height - offset);
		}

		/// <summary>
		/// Element position.
		/// </summary>
		public override Vector2 Position
		{
			set
			{
				spriteText.Position = value;

				base.Position = value;
			}
		}

		/// <summary>
		/// Activates the character to pop up from below the screen.
		/// </summary>
		public void Activate(float initialElapsed)
		{
			timer = new Timer(revealTime, progress =>
			{
				Position = Vector2.Lerp(basePosition, targetPosition, progress);
			}, time =>
			{
				timer.Reset();
				timer.Paused = true;
			}, initialElapsed);
		}

		/// <summary>
		/// Updates the element.
		/// </summary>
		public override void Update(float dt)
		{
			if (timer != null && !timer.Paused)
			{
				timer.Update(dt);
			}
		}

		/// <summary>
		/// Draws the element.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			spriteText.Draw(sb);
		}
	}
}
