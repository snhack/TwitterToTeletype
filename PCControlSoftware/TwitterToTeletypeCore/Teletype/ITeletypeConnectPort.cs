using System.IO.Ports;

namespace TTT.Teletype
{
	/// <summary>
	/// Interface wrapper around a serial port to allow for testing with no Teletype connected
	/// </summary>
	public interface ITeletypeConnectPort
	{
		event SerialDataReceivedEventHandler DataReceived;

		void Init(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handShake);
		void Open();
		void Close();

		bool IsOpen { get; }

		void Write(byte[] buffer, int offset, int count);
        void WriteLine(string line);

		string ReadExisting();
	}
}
