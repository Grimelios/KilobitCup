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
				new FieldData("Message.Top.Offset", FieldTypes.Integer),
				new FieldData("Message.Spacing", FieldTypes.Integer),
				new FieldData("Message.Zoom.Time", FieldTypes.Integer),
				new FieldData("Message.Scroll.Speed", FieldTypes.Integer),
			};

			object[] values = PropertyLoader.Load("Properties.txt", dataArray);

			TopOffset = (int)values[0];
			Spacing = (int)values[1];
			ZoomTime = (int)values[2];
			ScrollSpeed = (int)values[3];
		}

		/// <summary>
		/// Offset from the top edge of the screen.
		/// </summary>
		public int TopOffset { get; }

		/// <summary>
		/// Vertical spacing between messages.
		/// </summary>
		public int Spacing { get; }

		/// <summary>
		/// Duration for the message entering or leaving the screen (when not scrolling at a fixed rate).
		/// </summary>
		public int ZoomTime { get; }

		/// <summary>
		/// Scrolling speed (in pixels per second).
		/// </summary>
		public int ScrollSpeed { get; }
	}
}
