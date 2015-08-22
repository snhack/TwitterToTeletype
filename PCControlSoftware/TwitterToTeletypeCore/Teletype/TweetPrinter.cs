﻿using System;
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
        public int ConsoleWidth { get; set; }
        public bool IncludeAccreditation { get; set; }

		private ITeletype teletype;

		public TweetPrinter(ITeletype teletype)
		{
			this.teletype = teletype;

            // defaults
            IncludeAccreditation = true;
            ConsoleWidth = 70;
		}

		public void PrintTweet(Tweet t)
		{
			teletype.SwitchOn();

			teletype.WaitForTT(2000);

			teletype.Bell();
			teletype.Bell();

			PrintTweetText(t.Text);

            if (IncludeAccreditation)
            {
                teletype.CRLF();
                teletype.Print(string.Format("Tweeted by : {0} (@{1})", t.Author, t.ScreenName));
                teletype.CRLF();
                teletype.CRLF();
            }

			teletype.WaitForTT(2000);

			teletype.SwitchOff();

			teletype.WaitForTT(2000);
		}

		private void PrintTweetText(string message)
		{
			var formattedMessage = FormatMessage(message);

			while (formattedMessage.Length > 0)
			{
				string line;
				if (formattedMessage.Length <= ConsoleWidth)
				{
					line = formattedMessage;
					formattedMessage = string.Empty;
				}
				else
				{
					line = formattedMessage.Substring(0, ConsoleWidth);
					formattedMessage = formattedMessage.Substring(ConsoleWidth);
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
