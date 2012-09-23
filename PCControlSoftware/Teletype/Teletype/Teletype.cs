using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using Teletype.Utility;
using Teletype.Properties;

namespace Teletype
{
	public class Teletype : ITeletype
	{
		ITeletypeConnectPort port;

		public void Connect()
		{
			Logger.Instance.Write("Teletype connecting");

			port = new SerialTeletypeConnectPort();
			port.Init(Settings.Default.ComPort, 110, Parity.Even, 8, StopBits.Two, Handshake.None);
			port.Open();
		}

		public void Disconnect()
		{
			Logger.Instance.Write("Teletype disconnecting");

			port.Close();
			port = null;
		}

		public bool Connected
		{
			get
			{
				return port != null && port.IsOpen;
			}
		}

		public byte[] EncodeBytes(string message)
		{
			byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

			for (int i = 0; i < messageBytes.Length; i++)
				messageBytes[i] |= 1 << 7; // Set bit 7 to 1

			return messageBytes;
		}

		#region Print stuff to Teletype

		public void Print(string message)
		{
			var bytes = EncodeBytes(message);
			Print(bytes);
		}

		public void Print(byte b)
		{
			Print(new byte[] { b });
		}

		private void Print(byte[] bytes)
		{
			if (!Connected)
				throw new ApplicationException("Not connected to Teletype");

			for (int i = 0; i < bytes.Length; i++)
			{
				port.Write(new byte[] { bytes[i] }, 0, 1);
			}
		}

		#endregion

		#region Control Functions
		/*
		 * BELL = 135 = 11100001
		 * CR   = 141 = 10110001
		 * LF   = 138 = 01010001
		 * BS   = 136 = 00010001
		 */

		public void Bell()
		{
			Print(135);
		}

		public void Backspace()
		{
			Print(136);
		}

		public void CR()
		{
			Print(141);
		}

		public void LF()
		{
			Print(138);
		}

		public void CRLF()
		{
			CR();
			LF();
		}

		#endregion

		public void SwitchOff()
		{
			// Do nothing
		}

		public void SwitchOn()
		{
			// Do nothing
		}
	}
}
