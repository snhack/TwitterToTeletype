using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using Teletype.Utility;

namespace Teletype
{
	/// <summary>
	/// Debug connection, just dumps all data sent to the log
	/// </summary>
	class DebugConnectPort : ITeletypeConnectPort
	{
		public event SerialDataReceivedEventHandler DataReceived;

		private void LogMethodCall(string message)
		{
			Logger.Instance.Write("DebugConnectPort : {0}", message);
		}

		public void Init(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits, Handshake handShake)
		{
			LogMethodCall("Init");
		}

		public void Open()
		{
			LogMethodCall("Open");
		}

		public void Close()
		{
			LogMethodCall("Close");
		}

		public bool IsOpen
		{
			get { return true; }
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			LogMethodCall("Write");
			buffer.ToList().ForEach(b => Logger.Instance.Write(b.DebugText()));
		}

		public string ReadExisting()
		{
			return string.Empty;
		}
	}
}
