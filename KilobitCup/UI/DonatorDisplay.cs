using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using KilobitCup.Data;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.UI
{
	/// <summary>
	/// Container class that displays names of donators as they are received.
	/// </summary>
	public class DonatorDisplay : IMessageReceiver, IDynamic, IRenderable
	{
		private SpriteFont font;
		private DonatorCharacter[] characters;
		private Timer timer;

		private int activationIndex;
		private int holdTime;
		private int hiddenCount;
		private float revealRate;
		private bool revealing;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public DonatorDisplay()
		{
			object[] values = PropertyLoader.Load("Properties.txt", new []
			{
				new FieldData("Donator.Reveal.Rate", FieldTypes.Integer),
				new FieldData("Donator.Hold.Time", FieldTypes.Integer),
			});

			int charactersPerSecond = (int)values[0];

			font = ContentLoader.LoadFont("Donator");
			revealRate = 1000f / charactersPerSecond;
			holdTime = (int)values[1];

			MessageSystem.Subscribe(MessageTypes.Bits, this);
		}

		/// <summary>
		/// Whether the display is active.
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// Receives messages.
		/// </summary>
		public void Receive(MessageTypes messageType, object data)
		{
			HandleBits((BitData)data);
		}

		/// <summary>
		/// Handles bit events.
		/// </summary>
		private void HandleBits(BitData data)
		{
			string bitSuffix = data.Bits > 1 ? "bits" : "bit";
			string fullString = $"{data.Username} donated {data.Bits} {bitSuffix}!";

			// The full string contains three spaces.
			characters = new DonatorCharacter[fullString.Length - 3];

			int index = 0;

			Vector2 basePosition = new Vector2(Resolution.Width / 2 - font.MeasureString(fullString).X / 2, Resolution.Height);

			for (int i = 0; i < fullString.Length; i++)
			{
				char character = fullString[i];

				if (character != ' ')
				{
					float substringLength = font.MeasureString(fullString.Substring(0, i)).X;

					characters[index] = new DonatorCharacter(character, basePosition + new Vector2(substringLength, 0), this);
					index++;
				}
			}

			CreateActivationTimer();
			revealing = true;
			Active = true;
		}

		/// <summary>
		/// Creates a repeating timer to active characters.
		/// </summary>
		private void CreateActivationTimer()
		{
			activationIndex = 0;

			timer = new Timer(revealRate, time =>
			{
				// The Activate function is overloaded. It can either trigger the character to reveal itself or hide.
				characters[activationIndex].Activate(time);
				activationIndex++;

				if (activationIndex == characters.Length)
				{
					if (revealing)
					{
						CreateHoldTimer();
					}
					else
					{
						timer = null;
					}
				}

				return true;
			});
		}

		/// <summary>
		/// Creates the holding timer.
		/// </summary>
		private void CreateHoldTimer()
		{
			timer = new Timer(holdTime, time =>
			{
				revealing = false;
				CreateActivationTimer();
			});
		}

		/// <summary>
		/// Signals that a character is fully hidden.
		/// </summary>
		public void SignalHidden()
		{
			hiddenCount++;

			if (hiddenCount == characters.Length)
			{
				Active = false;
				characters = null;
			}
		}

		/// <summary>
		/// Updates the display.
		/// </summary>
		public void Update(float dt)
		{
			timer?.Update(dt);

			if (revealing)
			{
				for (int i = 0; i < activationIndex; i++)
				{
					characters[i].Update(dt);
				}
			}
			else
			{
				// Donator name uses a waving effect while visible, meaning that it's possible for characters to be hidden out of order.
				// For safety, it's easiest to just update every character while in the process of hiding.
				foreach (DonatorCharacter character in characters)
				{
					character.Update(dt);
				}
			}
		}

		/// <summary>
		/// Draws the display.
		/// </summary>
		public void Draw(SpriteBatch sb)
		{
			if (revealing)
			{
				for (int i = 0; i < activationIndex; i++)
				{
					characters[i].Draw(sb);
				}
			}
			else
			{
				foreach (DonatorCharacter character in characters)
				{
					character.Draw(sb);
				}
			}
		}
	}
}
