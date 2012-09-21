using System;
namespace Teletype
{
	interface ITeletype
	{
		void Backspace();
		void Bell();
		void Connect();
		bool Connected { get; }
		void CR();
		void CRLF();
		void Disconnect();
		byte[] EncodeBytes(string message);
		void LF();
		void Print(byte b);
		void Print(string message);
		void SwitchOff();
		void SwitchOn();
	}
}
