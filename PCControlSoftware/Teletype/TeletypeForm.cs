using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using Teletype.Twitter;
using Teletype.Utility;
using Teletype.Properties;

namespace Teletype
{
	public partial class TeletypeForm : Form
	{
		ITeletype teletype;
		Tweeter tweeter;
		TweetPrinter printer;

		public TeletypeForm()
		{
			InitializeComponent();
		}

		private void btnConnectToTeletype_Click(object sender, EventArgs e)
		{
			try
			{
				//teletype = new Teletype();
				teletype = new TeletypeViaAtmega();
				teletype.Connect();
				printer = new TweetPrinter(teletype);

				grpManualTransmission.Enabled = true;
				grpTwitter.Enabled = true;
				btnConnectToTeletype.Enabled = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnTeletypeOn_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.SwitchOn();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnTeletypeOff_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.SwitchOff();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnSendMessage_Click(object sender, EventArgs e)
		{
			try
			{
				var message = txtMessage.Text.ToUpper();
				txtMessage.Text = message;

				var messageBytes = teletype.EncodeBytes(message);
				ShowBytes(messageBytes);
				teletype.Print(message);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void ShowBytes(byte[] messageBytes)
		{
			txtDebug.Text = string.Empty;

			foreach (var b in messageBytes)
			{
				txtDebug.Text += b;
				txtDebug.Text += " : ";
				txtDebug.Text += Convert.ToString(b, 2).PadLeft(8, '0');
				txtDebug.Text += Environment.NewLine;
			}
		}

		private void btnSendByte_Click(object sender, EventArgs e)
		{
			try
			{
				var b = byte.Parse(txtByte.Text);
				teletype.Print(b);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnCR_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.CR();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnLF_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.LF();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnBell_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.Bell();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}

		private void btnBs_Click(object sender, EventArgs e)
		{
			try
			{
				teletype.Backspace();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnTweet_Click(object sender, EventArgs e)
		{
			try
			{
				tweeter = new Tweeter();
				tweeter.NewTweet += new EventHandler<NewTweetEventArgs>(t_NewTweet);
				tweeter.StartSearch(txtTwitterQuery.Text);

				txtDebug.Text = "Tweeter started...  Wait.";

				grpTwitter.Enabled = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		void t_NewTweet(object sender, NewTweetEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => { t_NewTweet(sender, e); }));
				return;
			}

			try
			{
				txtDebug.Text +=
					string.Format("{0} Sending tweet to teletype : {1}", Environment.NewLine, e.Tweet.Text);

				printer.PrintTweet(e.Tweet);
			}
			catch (Exception ex)
			{
				Logger.Instance.Error("Failed to send tweet to teletype", ex);
			}
		}
	}
}
