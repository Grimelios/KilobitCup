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
		private static readonly int[] BitBadgeThresholds =
		{
			1,
			100,
			1000,
			5000,
			10000,
			25000,
			50000,
			75000,
			100000,
			200000,
			300000,
			400000,
			500000,
			600000,
			700000,
			800000,
			900000,
			1000000
		};

		private Sprite badge;
		private SpriteFont font;
		private DonatorData data;
		private DonatorCharacter[] characters;
		private Timer characterTimer;
		private Timer badgeTimer;

		private int activationIndex;
		private int hiddenCount;
		private int fontSize;

		private bool revealing;
		private bool messageComplete;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public DonatorDisplay()
		{
			data = new DonatorData();
			font = ContentLoader.LoadFont("Donator");
			fontSize = (int)font.MeasureString("A").Y;

			MessageSystem.Subscribe(MessageTypes.Bits, this);
			MessageSystem.Subscribe(MessageTypes.MessageComplete, this);
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
			switch (messageType)
			{
				case MessageTypes.Bits:
					HandleBits((BitData)data);
					break;

				case MessageTypes.MessageComplete:
					messageComplete = true;
					
					// The timer NOT being null indicates that the minimum holding time has not yet passed.
					if (characterTimer == null)
					{
						CreateActivationTimer();
					}

					break;
			}
		}

		/// <summary>
		/// Handles bit events.
		/// </summary>
		private void HandleBits(BitData bitData)
		{
			CreateBadge(bitData.TotalBits);

			string bitSuffix = bitData.Bits > 1 ? "bits" : "bit";
			string fullString = $"{bitData.Username} donated {bitData.Bits} {bitSuffix}!";

			// The full string contains three spaces.
			characters = new DonatorCharacter[fullString.Length - 3];

			int index = 0;
			int fullWidth = (int)font.MeasureString(fullString).X + badge.Width + data.BadgeSpacing;

			Vector2 basePosition = new Vector2((Resolution.Width - fullWidth) / 2, Resolution.Height);
			Vector2 textPosition = basePosition + new Vector2(badge.Width + data.BadgeSpacing, 0);

			badge.Position = basePosition + new Vector2(badge.Width / 2, fontSize / 2 - data.Offset1);

			for (int i = 0; i < fullString.Length; i++)
			{
				char character = fullString[i];

				if (character != ' ')
				{
					float substringLength = font.MeasureString(fullString.Substring(0, i)).X;

					characters[index] = new DonatorCharacter(character, textPosition + new Vector2(substringLength, 0), data.Offset1,
						this);
					index++;
				}
			}

			revealing = true;
			CreateActivationTimer();
			Active = true;
		}

		/// <summary>
		/// Creates a sprite for the badge. Badge type is determined from the total number of bits donated by the user. Also creates the
		/// badge timer.
		/// </summary>
		private void CreateBadge(int totalBits)
		{
			int badgeLevel = 0;

			while (badgeLevel < BitBadgeThresholds.Length - 1 && totalBits >= BitBadgeThresholds[badgeLevel + 1])
			{
				badgeLevel++;
			}

			badge = new Sprite("Badges/BitBadge" + badgeLevel)
			{
				Scale = 0
			};

			badgeTimer = new Timer(data.BadgeDelay, time =>
			{
				CreateBadgeTimer();
			});
		}

		/// <summary>
		/// Creates a timer for expanding or shrinking the badge.
		/// </summary>
		private void CreateBadgeTimer()
		{
			badgeTimer = new Timer(data.BadgeSpinTime, progress =>
			{
				float amount = revealing
					? MathHelper.Lerp(0, 1, EaseFunctions.Ease(progress, EaseTypes.EaseOutBack))
					: MathHelper.Lerp(1, 0, EaseFunctions.Ease(progress, EaseTypes.EaseInBack));

				badge.Scale = amount;
				badge.Rotation = amount * MathHelper.TwoPi;
			}, time =>
			{
				badgeTimer = null;
			});
		}

		/// <summary>
		/// Creates a repeating timer to activate characters.
		/// </summary>
		private void CreateActivationTimer()
		{
			activationIndex = 0;
			characterTimer = new Timer(data.RevealRate, time =>
			{
				// The Activate function is overloaded. It can either trigger the character to reveal itself or hide.
				characters[activationIndex].Activate(data.RevealTime, time);
				activationIndex++;

				if (activationIndex == characters.Length)
				{
					if (revealing)
					{
						CreateHoldTimer();
					}
					else
					{
						characterTimer = null;
					}
				}

				return true;
			});

			if (!revealing)
			{
				CreateBadgeTimer();
			}
		}

		/// <summary>
		/// Creates the holding timer.
		/// </summary>
		private void CreateHoldTimer()
		{
			characterTimer = new Timer(data.MinimumHoldTime, time =>
			{
				revealing = false;

				// Donator messages stay on screen until their message begins to accelerate off-screen (unless the message finishes before
				// the minimum holding time has elapsed).
				if (messageComplete)
				{
					CreateActivationTimer();
				}
				else
				{
					characterTimer = null;
				}
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
			characterTimer?.Update(dt);
			badgeTimer?.Update(dt);

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
			badge?.Draw(sb);

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
