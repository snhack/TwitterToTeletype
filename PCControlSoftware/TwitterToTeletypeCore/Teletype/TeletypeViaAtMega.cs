using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using TTT.Utility;
using TTT.Properties;

namespace TTT.Teletype
{
	/// <summary>
	/// Represents a Teletype connected via the hackspaces AtMega interface that allows the Teletype
	/// to be turned on and off as well as translating messages from 9600 baud to the 110 baud
	/// required for the teletype.
	/// The Serial conversion has a limited buffer, so this class deals with that by breaking longer
	/// messages into short segments and allowing time for the buffer to be forwarded to the Teletype.
	/// 
	/// </summary>
	public class TeletypeViaAtmega : ITeletype
	{
		const int SwitchDelay = 2000; // Time to wait after switching power on or off to allow TT to warm up/down
		const int PrintDelay = 4000;  // Delay time for AtMega to empty it's serial buffer

		ITeletypeConnectPort port;

		public void Connect(ITeletypeConnectPort port)
		{
			Logger.Instance.Write("TeletypeViaAtmega connecting");

			this.port = port;
			port.Init(Settings.Default.ComPort, 9600, Parity.None, 8, StopBits.One, Handshake.None);
			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
			port.Open();
		}

		public void Disconnect()
		{
			Logger.Instance.Write("TeletypeViaAtmega disconnecting");

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

		public void SwitchOn()
		{
			Print(System.Text.Encoding.UTF8.GetBytes("<<\n"));
			Thread.Sleep(SwitchDelay);
		}

		public void SwitchOff()
		{
			Print(System.Text.Encoding.UTF8.GetBytes(">>\n"));
			Thread.Sleep(SwitchDelay);
		}

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			Logger.Instance.Write(port.ReadExisting());
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
			SegmentEnd();
		}

		public void Print(byte b)
		{
			Print(new byte[] { b });
			SegmentEnd();
		}

		private void Print(byte[] bytes)
		{
			if (!Connected)
				throw new ApplicationException("Not connected to Teletype");

			int counter = 0;

			for (int i = 0; i < bytes.Length; i++)
			{
				port.Write(new byte[] { bytes[i] }, 0, 1);

				// AtMega buffer is only 50 bytes, we need to send \n to flush buffer to teletype
				if (++counter % 50 == 0)
				{
					SegmentEnd();
					Thread.Sleep(PrintDelay);
				}
			}
		}

		private void SegmentEnd()
		{
			Print(System.Text.Encoding.UTF8.GetBytes("\n"));
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
	}
}
