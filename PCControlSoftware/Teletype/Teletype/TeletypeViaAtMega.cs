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
	public class TeletypeViaAtmega : ITeletype
	{
		SerialPort port;

		public void Connect()
		{
			Logger.Instance.Write("TeletypeViaAtmega connecting");
			
			port = new SerialPort(Properties.Settings.Default.ComPort, 9600, Parity.None, 8, StopBits.One);

			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
			port.Handshake = Handshake.None;
			port.Open();
		}

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			Console.WriteLine(port.ReadExisting());
		}

		public void Disconnect()
		{
			Logger.Instance.Write("TeletypeViaAtmega disconnecting");

			port.Close();
			port = null;
		}

		public void SwitchOn()
		{
			Print(System.Text.Encoding.UTF8.GetBytes("<<\n"));
		}

		public void SwitchOff()
		{
			Print(System.Text.Encoding.UTF8.GetBytes(">>\n"));
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

				/*
				if (counter++ % 50 == 0)
				{
					SegmentEnd();
					Thread.Sleep(5000);
				}*/
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
