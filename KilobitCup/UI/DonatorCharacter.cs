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
		private DonatorDisplay parent;
		
		private bool entering;

		/// <summary>
		/// Constructs the element.
		/// </summary>
		public DonatorCharacter(char character, Vector2 position, DonatorDisplay parent)
		{
			this.parent = parent;

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
			entering = !entering;

			// The timer is paused while holding, so it doesn't need to be recreated a second time.
			if (!entering)
			{
				timer.Paused = false;

				return;
			}

			timer = new Timer(revealTime, progress =>
			{
				Position = entering
					? Vector2.Lerp(basePosition, targetPosition, EaseOutBack(progress)) 
					: Vector2.Lerp(targetPosition, basePosition, EaseInBack(progress));
			}, time =>
			{
				if (entering)
				{
					timer.Reset();
					timer.Paused = true;
				}
				else
				{
					timer = null;
					parent.SignalHidden();
				}
			}, initialElapsed);
		}

		/// <summary>
		/// Computes in-back easing.
		/// </summary>
		private float EaseInBack(float amount)
		{
			// See https://github.com/warrenm/AHEasing/blob/master/AHEasing/easing.c.
			return amount * amount - amount * (float)Math.Sin(amount * MathHelper.Pi);
		}

		/// <summary>
		/// Computes out-back easing.
		/// </summary>
		private float EaseOutBack(float amount)
		{
			// See https://github.com/warrenm/AHEasing/blob/master/AHEasing/easing.c.
			float f = 1 - amount;

			return 1 - (f * f - f * (float)Math.Sin(f * MathHelper.Pi));
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
