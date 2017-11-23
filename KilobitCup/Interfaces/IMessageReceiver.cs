using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Interfaces
{
	/// <summary>
	/// Enumeration storing message types.
	/// </summary>
	public enum MessageTypes
	{
		Bits,
		MessageComplete
	}

	/// <summary>
	/// Represents any object that can receive messages.
	/// </summary>
	public interface IMessageReceiver
	{
		/// <summary>
		/// Receives messages.
		/// </summary>
		void Receive(MessageTypes messageType, object data);
	}
}
