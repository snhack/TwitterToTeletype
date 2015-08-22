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
	/// Represents a "logobot" connecting via serial.
	/// </summary>
	public class LogobotViaSerial : ITeletype
	{
		ITeletypeConnectPort port;

        public void Init()
        {
            if (SimulateWrite)
            {
                Logger.Instance.Write("  || CS");
                return;
            }
            port.WriteLine("CS");
       }

		public void Connect(ITeletypeConnectPort port)
		{
            Logger.Instance.Write("LogobotViaSerial connecting");

			this.port = port;
			port.Init(Settings.Default.ComPort, 9600, Parity.None, 8, StopBits.One, Handshake.None);
			try
			{
				port.Open();
			}
			catch(System.IO.IOException)
			{
				if (!SimulateWrite) throw;
				// Log use of SimulateWrite mode
				Logger.Instance.Write("Port could not be opened, continuing in SimulateWrite mode");
			}
			catch(Exception ex) 
			{
				Logger.Instance.Error("Failed to open com port", ex);
			}

			if (!SimulateWrite)
				port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
		}

		public void Disconnect()
		{
            Logger.Instance.Write("LogobotViaSerial disconnecting");

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
            // Nothing to do
		}

		public void SwitchOff()
		{
            // Nothing to do
		}

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			//Logger.Instance.Write(port.ReadExisting());
		}

		#region Send stuff to Logobot

		public void Print(string message)
		{
			if (SimulateWrite)
			{
				// Simulating, so print to console
				Logger.Instance.Write (" || WT {0}", message.ToUpper());
				return;
			}

			if (!Connected && !SimulateWrite)
				throw new ApplicationException("Not connected to Logobot");

            port.WriteLine("WT " + message.ToUpper());
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
            if (SimulateWrite)
            {
                Logger.Instance.Write(" || BZ 500");
                return;
            }

            port.WriteLine("BZ 500");
		}

		#endregion



        // Allows directing output to console when no device connected
        // FIXME: Do something more useful with this placeholder
        public bool SimulateWrite { get { return !Connected || false; } }

        // (Dis)allows teletype delay when no device connected
        // FIXME: Do something more useful with this placeholder
        public bool SimulateDelay { get { return SimulateWrite && false; } }

        // Show control characters etc. when simulating
        // FIXME: Do something more useful with this placeholder
        public bool SimulateShowsBytes { get { return SimulateWrite && false; } }

        public void WaitForTT(int wait)
        {
            // No need to wait for logobot
        }

        public void Backspace()
        {
            throw new NotImplementedException();
        }

        public void CR()
        {
            throw new NotImplementedException();
        }

        public void CRLF()
        {
            var cmd = "NL";
            if (SimulateWrite)
            {
                Logger.Instance.Write(" || " + cmd);
                return;
            }

            port.WriteLine(cmd);
        }

        public void LF()
        {
            throw new NotImplementedException();
        }

        public byte[] EncodeBytes(string message)
        {
            throw new NotImplementedException();
        }

        public void Print(byte b)
        {
            throw new NotImplementedException();
        }
    }
}
