using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO.Ports;
using TTT.Utility;
using TTT.Properties;

namespace TTT.Twitter
{
	/// <summary>
	/// Polls the Twitter api for tweets matching the give searchTerm and fires and event
	/// when new tweets are found
	/// </summary>
	public class Tweeter
	{
		const string SEARCH_URL = "http://search.twitter.com/search.atom?q={0}&{1}";

        public string CurrentSearchTerm { get; private set; }

		public double PollingIntervalMs { get; set; }

		public event EventHandler<NewTweetEventArgs> NewTweet;

        private Timer timer;

		private string LastId = string.Empty;

		public Tweeter()
		{
			// Defaults
			PollingIntervalMs = Settings.Default.TwitterPollingIntervalMs;
		}

        public void StartSearch(string searchTerm)
        {
			Logger.Instance.Write("Starting tweeter for {0}, interval {1}ms", searchTerm, PollingIntervalMs);

			if (timer != null)
			{
				timer.Stop();
				timer = null;
			}

            CurrentSearchTerm = searchTerm;

            timer = new Timer(PollingIntervalMs);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = false;
			timer_Elapsed(this, null);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
			try
			{
				string filter;

				Logger.Instance.Write("Tweeter polling for {0}", CurrentSearchTerm);

				if (string.IsNullOrEmpty(LastId))
					filter = "rpp=1"; // First time just get the latest tweet
				else
					filter = "since_id=" + LastId; // Else use the Id of the last tweet received

				string url = string.Format(SEARCH_URL, HttpUtility.UrlEncode(CurrentSearchTerm), filter);

				Logger.Instance.Write("Fetching {0}", url);

				XmlDocument dom = new XmlDocument();
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(dom.NameTable);
				nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");
				nsmgr.AddNamespace("twitter", "http://api.twitter.com/");
				dom.Load(url);

				var nodes = dom.DocumentElement.SelectNodes("atom:entry", nsmgr);

				Logger.Instance.Write("{0} tweets to print out", nodes.Count);

				foreach (XmlNode node in nodes)
				{
					Tweet tweet = Tweet.FromNode(node, nsmgr);

					LastId = tweet.Id2005;

					Logger.Instance.Write("Received Tweet : {0}", tweet.Text);

					OnNewTweet(tweet);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.Write("Tweeter poll failed", ex);
			}
			finally
			{
				timer.Start();
			}
        }

		protected void OnNewTweet(Tweet t)
		{
			var handler = NewTweet;
			if (handler != null)
			{
				var e = new NewTweetEventArgs(t);
				handler(this, e);
			}
		}
	}
}
