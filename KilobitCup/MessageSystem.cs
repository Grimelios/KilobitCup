using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;

namespace KilobitCup
{
	/// <summary>
	/// Utility class used to allow classes to communicate in a decoupled way.
	/// </summary>
	public static class MessageSystem
	{
		private static List<IMessageReceiver>[] receivers = new List<IMessageReceiver>[CoreFunctions.EnumCount<MessageTypes>()];

		/// <summary>
		/// Subscribes the given receiver to the given message type.
		/// </summary>
		public static void Subscribe(MessageTypes messageType, IMessageReceiver receiver)
		{
			VerifyList(messageType).Add(receiver);
		}

		/// <summary>
		/// Unsubscribes the given receiver from the given message type.
		/// </summary>
		public static void Unsubscribe(MessageTypes messageType, IMessageReceiver receiver)
		{
			receivers[(int)messageType].Remove(receiver);
		}

		/// <summary>
		/// Sends the given message.
		/// </summary>
		public static void Send(MessageTypes messageType, object data)
		{
			VerifyList(messageType).ForEach(r => r.Receive(messageType, data));
		}

		/// <summary>
		/// Verifies whether a receiver list for the given message type exist and, if not, creates one.
		/// </summary>
		private static List<IMessageReceiver> VerifyList(MessageTypes messageType)
		{
			int index = (int)messageType;

			return receivers[index] ?? (receivers[index] = new List<IMessageReceiver>());
		}
	}
}
