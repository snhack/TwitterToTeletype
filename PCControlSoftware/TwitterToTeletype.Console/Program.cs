using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTT.Twitter;
using TTT.Teletype;
using TTT.Utility;

namespace TTT.Console
{
	class Program
	{
		static Tweeter tweeter;
		static TweetPrinter printer;
		static ITeletype teletype;

		static void Main(string[] args)
		{
			Logger.Instance.Write("Starting...");

			teletype = new TeletypeViaAtmega();
			teletype.Connect(new SerialTeletypeConnectPort());

			Logger.Instance.Write("Printing welcome message");
			teletype.SwitchOn();
			teletype.CRLF();
			teletype.Print("Swindon Hackspace - Twitter to Teletype project - 2012");
			teletype.CRLF();
			teletype.SwitchOff();

			printer = new TweetPrinter(teletype);

			Logger.Instance.Write("Starting Twitter polling");
			tweeter = new Tweeter();
			tweeter.NewTweet += new EventHandler<NewTweetEventArgs>(tweeter_NewTweet);
			tweeter.StartSearch("hakmoc");

			Logger.Instance.Write("Startup complete.  Waiting for tweets");
			Logger.Instance.Write("Press any key to exit");

			System.Console.ReadLine();

			Logger.Instance.Write("Exiting...");

			teletype.SwitchOff();
			teletype.Disconnect();
		}

		static void tweeter_NewTweet(object sender, NewTweetEventArgs e)
		{
			try
			{
				Logger.Instance.Write("{0} Sending tweet to teletype : {1}", Environment.NewLine, e.Tweet.Text);
				printer.PrintTweet(e.Tweet);
			}
			catch (Exception ex)
			{
				Logger.Instance.Write("Failed to send tweet to teletype : {0}", ex.ToString());
			}
		}
	}
}
