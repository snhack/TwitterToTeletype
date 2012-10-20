using System;

namespace TTT.Teletype
{
	/// <summary>
	/// Teletype interface.
	/// Largely pointless abstraction, but may be helpful for testing
	/// </summary>
	public interface ITeletype
	{
		void SwitchOn();
		void SwitchOff();

		void Connect(ITeletypeConnectPort port);
		void Disconnect();
		bool Connected { get; }
	
		bool SimulateWrite { get; }
		bool SimulateShowsBytes { get; }

		void Backspace();
		void Bell();
		void CR();
		void CRLF();
		void LF();

		byte[] EncodeBytes(string message);

		void Print(byte b);
		void Print(string message);
	}
}
