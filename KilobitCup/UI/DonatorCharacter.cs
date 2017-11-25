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
		private Timer timer;
		private Vector2 basePosition;
		private Vector2 targetPosition;
		private SpriteText spriteText;
		private DonatorDisplay parent;
		
		private bool entering;

		/// <summary>
		/// Constructs the element.
		/// </summary>
		public DonatorCharacter(char character, Vector2 position, int offset, DonatorDisplay parent)
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
		public void Activate(int revealTime, float initialElapsed)
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
					? Vector2.Lerp(basePosition, targetPosition, EaseFunctions.Ease(progress, EaseTypes.EaseOutBack)) 
					: Vector2.Lerp(targetPosition, basePosition, EaseFunctions.Ease(progress, EaseTypes.EaseInBack));
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
