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
			"muxy",
			"streamlabs",
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

		private bool textFirst;

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

				for (int i = 0; i < cheerList.Count; i++)
				{
					cheerList[i].Position = value + offsetList[i * 2 + indexCorrection];
				}
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

			for (int i = 0; i < textList.Count + cheerList.Count - 1; i++)
			{
				bool isTextItem = textFirst ? i % 2 == 0 : i % 2 == 1;

				offset.X += isTextItem ? textList[i / 2].Width : cheerList[i / 2].Width;
				offsetList.Add(offset);
			}
		}

		/// <summary>
		/// Updates the message.
		/// </summary>
		public override void Update(float dt)
		{
			cheerList.ForEach(c => c.Update(dt));
		}

		/// <summary>
		/// Draws the message.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			textList.ForEach(t => t.Draw(sb));
			cheerList.ForEach(s => s.Draw(sb));
		}
	}
}
