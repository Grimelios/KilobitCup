using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Core;
using KilobitCup.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Represents text that scroll across the screen when a bit event is received.
	/// </summary>
	public class ScrollingMessage : Entity
	{
		private static readonly string[] CheerKeywords =
		{
			"cheer",
			"ripcheer",
			"kappa",
			"kreygasm",
			"swiftrage",
			"4head",
			"pjsalt",
			"mrdestructoid",
			"trihard",
			"notlikethis",
			"failfish",
			"vohiyo",
			"streamlabs",
			"muxy",
			"bday",
			"bitboss",
			"doodlecheer"
		};

		// Every valid cheer must contain at least one digit.
		private static int minimumKeywordLength = CheerKeywords.Min(k => k.Length) + 1;

		private static ScrollingMessageData data = new ScrollingMessageData();

		private List<SpriteText> textList;
		private List<Cheer> cheerList;
		private List<Vector2> offsetList;
		private Vector2 velocity;

		private bool textFirst;
		private bool accelerating;
		private float totalWidth;

		private int releaseIndex;

		/// <summary>
		/// Constructs the message. Message tokens are parsed on creation.
		/// </summary>
		public ScrollingMessage(string message, int messageCount) : base(EntityTypes.Message)
		{
			textList = new List<SpriteText>();
			cheerList = new List<Cheer>();
			offsetList = new List<Vector2>();

			ComputeTokens(message);
			ComputeOffsets();

			Position = new Vector2(Resolution.Width, data.TopOffset + data.Spacing * messageCount);
			velocity.X = -data.ScrollSpeed;
		}

		/// <summary>
		/// Message position.
		/// </summary>
		public override Vector2 Position
		{
			set
			{
				int indexCorrection = textFirst ? 1 : 0;

				for (int i = 0; i < textList.Count; i++)
				{
					textList[i].Position = value + offsetList[i * 2 + (1 - indexCorrection)];
				}

				for (int i = releaseIndex; i < cheerList.Count; i++)
				{
					cheerList[i].Position = value + offsetList[i * 2 + indexCorrection];
				}

				base.Position = value;
			}
		}

		/// <summary>
		/// Computes text and image tokens from the raw message.
		/// </summary>
		private void ComputeTokens(string message)
		{
			int index = 0;
			int cheerIndex;
			int cheerLength;

			SpriteFont font = ContentLoader.LoadFont("Default");

			while (ParseNextCheer(message, index, out cheerIndex, out cheerLength))
			{
				if (index != cheerIndex)
				{
					textList.Add(new SpriteText(font, message.Substring(index, cheerIndex - index)));
				}
				
				index = cheerIndex + cheerLength;
			}

			if (index < message.Length)
			{
				textList.Add(new SpriteText(font, message.Substring(index, message.Length - index)));
			}

			// Text-first is false by default.
			if (CheerKeywords.Any(k => message.ContainsAt(k, 0)))
			{
				return;
			}

			textFirst = true;
		}

		/// <summary>
		/// Finds the next cheer text in the message (if any) starting at the current index.
		/// </summary>
		private bool ParseNextCheer(string message, int index, out int cheerIndex, out int cheerLength)
		{
			for (int i = index; i < message.Length - minimumKeywordLength; i++)
			{
				int bitValue;

				if (TryParseCheer(message, i, out cheerLength, out bitValue) && bitValue > 0)
				{
					cheerIndex = i;

					return true;
				}
			}

			cheerIndex = -1;
			cheerLength = -1;

			return false;
		}

		/// <summary>
		/// Attempts to parse a cheer at the given index using all possible keywords. If successful, bit value is set.
		/// </summary>
		private bool TryParseCheer(string message, int index, out int cheerLength, out int bitValue)
		{
			for (int i = 0; i < CheerKeywords.Length; i++)
			{
				string keyword = CheerKeywords[i];

				if (!message.ContainsAt(keyword, index))
				{
					continue;
				}

				int endIndex = index + keyword.Length;
				int whitespaceIndex = message.IndexOf(' ', endIndex);

				if (whitespaceIndex == -1)
				{
					whitespaceIndex = message.Length;
				}

				if (int.TryParse(message.Substring(endIndex, whitespaceIndex - endIndex), out bitValue))
				{
					cheerLength = whitespaceIndex - index;
					cheerList.Add(new Cheer((CheerTypes)i, bitValue));

					return true;
				}
			}

			bitValue = -1;
			cheerLength = -1;

			return false;
		}

		/// <summary>
		/// Computes local offset relative to the message's base position.
		/// </summary>
		private void ComputeOffsets()
		{
			Vector2 offset = Vector2.Zero;
			offsetList.Add(offset);

			int offsetCount = textList.Count + cheerList.Count - 1;

			for (int i = 0; i < offsetCount; i++)
			{
				bool isTextItem = textFirst ? i % 2 == 0 : i % 2 == 1;

				offset.X += isTextItem ? textList[i / 2].Width : cheerList[i / 2].Width;
				offsetList.Add(offset);
			}

			bool textLast = textList.Count == cheerList.Count ? !textFirst : textFirst;

			totalWidth = offset.X + (textLast ? textList.Last().Width : cheerList.Last().Width);
		}

		/// <summary>
		/// Updates the message.
		/// </summary>
		public override void Update(float dt)
		{
			if (accelerating)
			{
				velocity.X -= data.Acceleration * dt;
				Position += velocity * dt;

				if (Position.X <= -totalWidth)
				{
					Scene.Remove(this);
				}
			}
			else
			{
				Position -= new Vector2(data.ScrollSpeed * dt, 0);

				if (releaseIndex < cheerList.Count)
				{
					CheckCheerRelease();
				}

				if (Position.X <= Resolution.Width / 2 - totalWidth)
				{
					accelerating = true;
				}
			}

			cheerList.ForEach(c => c.Update(dt));
		}

		/// <summary>
		/// Checks if the next cheer should be released and, if so, releases it. Only called if at least one cheer is left in the list.
		/// </summary>
		private void CheckCheerRelease()
		{
			Cheer nextCheer = cheerList[releaseIndex];

			if (nextCheer.Position.X <= Resolution.Width / 2)
			{
				nextCheer.EnablePhysics();
				Scene.Add(nextCheer);
				releaseIndex++;

				if (releaseIndex == cheerList.Count)
				{
					accelerating = true;
				}
			}
		}

		/// <summary>
		/// Draws the message.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			textList.ForEach(t => t.Draw(sb));

			for (int i = releaseIndex; i < cheerList.Count; i++)
			{
				cheerList[i].Draw(sb);
			}
		}
	}
}
