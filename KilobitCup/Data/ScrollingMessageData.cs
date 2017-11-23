using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;

namespace KilobitCup.Data
{
	/// <summary>
	/// Data class used by scrolling messages.
	/// </summary>
	public class ScrollingMessageData
	{
		private FieldData[] dataArray;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public ScrollingMessageData()
		{
			dataArray = new []
			{
				new FieldData("Message.Offset", FieldTypes.Integer),
				new FieldData("Message.Spacing", FieldTypes.Integer),
				new FieldData("Message.Acceleration", FieldTypes.Integer),
				new FieldData("Message.Scroll.Speed", FieldTypes.Integer),
			};

			object[] values = PropertyLoader.Load("Properties.txt", dataArray);

			Offset = (int)values[0];
			Spacing = (int)values[1];
			Acceleration = (int)values[2];
			ScrollSpeed = (int)values[3];
		}

		/// <summary>
		/// Offset from the top edge of the screen.
		/// </summary>
		public int Offset { get; }

		/// <summary>
		/// Vertical spacing between messages.
		/// </summary>
		public int Spacing { get; }

		/// <summary>
		/// Message acceleration.
		/// </summary>
		public int Acceleration { get; }

		/// <summary>
		/// Fixed scroll speed (in pixels per second).
		/// </summary>
		public int ScrollSpeed { get; }
	}
}
