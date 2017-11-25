using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Data
{
	/// <summary>
	/// Data class storing properties for the donator display.
	/// </summary>
	public class DonatorData
	{
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public DonatorData()
		{
			object[] values = PropertyLoader.Load("Properties.txt", new []
			{
				new FieldData("Donator.Offset.1", FieldTypes.Integer),
				new FieldData("Donator.Offset.2", FieldTypes.Integer),
				new FieldData("Donator.Reveal.Time", FieldTypes.Integer),
				new FieldData("Donator.Reveal.Rate", FieldTypes.Integer),
				new FieldData("Donator.Badge.Delay", FieldTypes.Integer),
				new FieldData("Donator.Badge.Spacing", FieldTypes.Integer),
				new FieldData("Donator.Badge.Spin.Time", FieldTypes.Integer),
				new FieldData("Donator.Minimum.Hold.Time", FieldTypes.Integer),
			});

			Offset1 = (int)values[0];
			Offset2 = (int)values[1];
			RevealTime = (int)values[2];
			RevealRate = 1000f / (int)values[3];
			BadgeDelay = (int)values[4];
			BadgeSpacing = (int)values[5];
			BadgeSpinTime = (int)values[6];
			MinimumHoldTime = (int)values[7];
		}

		/// <summary>
		/// Top-line offset.
		/// </summary>
		public int Offset1 { get; }

		/// <summary>
		/// Bottom-line offset.
		/// </summary>
		public int Offset2 { get; }

		/// <summary>
		/// Character reveal rate.
		/// </summary>
		public float RevealRate { get; }

		/// <summary>
		/// Character reveal time.
		/// </summary>
		public int RevealTime { get; }

		/// <summary>
		/// Delay on the badge appearing or disappearing.
		/// </summary>
		public int BadgeDelay { get; }

		/// <summary>
		/// Badge animation duration.
		/// </summary>
		public int BadgeSpinTime { get; }

		/// <summary>
		/// Spacing between the badge the the text.
		/// </summary>
		public int BadgeSpacing { get; }

		/// <summary>
		/// Minimum holding time.
		/// </summary>
		public int MinimumHoldTime { get; }
	}
}
