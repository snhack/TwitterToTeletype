using System.IO.Ports;

namespace TTT.Teletype
{
	/// <summary>
	/// Connection to a teletype via a serial port
	/// </summary>
	public class SerialTeletypeConnectPort : ITeletypeConnectPort
	{
		private SerialPort port;

		public event SerialDataReceivedEventHandler DataReceived;

		public void Init(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handShake)
		{
			port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
			port.Handshake = handShake;
			port.DataReceived += port_DataReceived;
		}

		public void Open()
		{
			port.Open();
		}

		public void Close()
		{
			port.Close();
		}

		public bool IsOpen
		{
			get { return port.IsOpen; }
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			port.Write(buffer, offset, count);
		}

        public void WriteLine(string line)
        {
            port.WriteLine(line);
        }

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			var handler = DataReceived;
			if (handler != null)
				handler(this, e);
		}

		public string ReadExisting()
		{
			return port.ReadExisting();
		}
	}
}
