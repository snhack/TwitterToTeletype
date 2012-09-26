using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTT.Twitter;
using System.Threading;

namespace TTT.Teletype
{
	/// <summary>
	/// TweetPrinter links a Tweet to a Teletype, dealing with turning the Teletype on / off 
	/// and formatting the tweet for printing.
	/// Just in case someone doesn't hear the Teletype start up, we also ring the bell!
	/// </summary>
	public class TweetPrinter
	{
		const int CONSOLE_WIDTH = 70;

		private ITeletype teletype;

		public TweetPrinter(ITeletype teletype)
		{
			this.teletype = teletype;
		}

		public void PrintTweet(Tweet t)
		{
			teletype.SwitchOn();

			Thread.Sleep(2000);

			teletype.Bell();
			teletype.Bell();

			PrintTweetText(t.Text);	
			teletype.CRLF();
			teletype.Print(string.Format("Tweeted by : {0} at {1}", t.Author, t.Published.ToString("dd MMM yyyy HH:mm:ss")));
			teletype.CRLF();
			teletype.CRLF();

			Thread.Sleep(2000);

			teletype.SwitchOff();

			Thread.Sleep(2000);
		}

		private void PrintTweetText(string message)
		{
			var formattedMessage = FormatMessage(message);

			while (formattedMessage.Length > 0)
			{
				string line;
				if (formattedMessage.Length <= CONSOLE_WIDTH)
				{
					line = formattedMessage;
					formattedMessage = string.Empty;
				}
				else
				{
					line = formattedMessage.Substring(0, CONSOLE_WIDTH);
					formattedMessage = formattedMessage.Substring(CONSOLE_WIDTH);
				}
				
				teletype.Print(line);
				teletype.CRLF();
			}
		}

		private string FormatMessage(string message)
		{
			// TODO: tidy up message
			return message;
		}
	}
}
