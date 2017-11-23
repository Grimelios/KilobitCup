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
		private float revealRate;
		private bool active;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public DonatorDisplay()
		{
			int charactersPerSecond = int.Parse(PropertyLoader.LoadMap("Properties.txt")["Donator.Reveal.Rate"]);

			font = ContentLoader.LoadFont("Donator");
			revealRate = 1000f / charactersPerSecond;

			MessageSystem.Subscribe(MessageTypes.Bits, this);
		}

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

					characters[index] = new DonatorCharacter(character, basePosition + new Vector2(substringLength, 0));
					index++;
				}
			}

			timer = new Timer(revealRate, time =>
			{
				characters[activationIndex].Activate(time);
				activationIndex++;

				if (activationIndex == characters.Length)
				{
					timer = null;
				}
			});

			active = true;
		}

		/// <summary>
		/// Updates the display.
		/// </summary>
		public void Update(float dt)
		{
			if (!active)
			{
				return;
			}

			timer?.Update(dt);

			foreach (DonatorCharacter character in characters)
			{
				character.Update(dt);
			}
		}

		/// <summary>
		/// Draws the display.
		/// </summary>
		public void Draw(SpriteBatch sb)
		{
			foreach (DonatorCharacter character in characters)
			{
				character.Draw(sb);
			}
		}
	}
}
