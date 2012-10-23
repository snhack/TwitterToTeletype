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
		interface IQueryUris
		{
			// {0} = Twitter User Id
			// {1} = Additional querystring items
			string InitialQuery { get; }
			string SubsequentQuery { get; }
		}

		class SearchUris : IQueryUris
		{
			public string InitialQuery { get { return "http://search.twitter.com/search.atom?q={0}&rpp=1"; } }
			public string SubsequentQuery { get { return "http://search.twitter.com/search.atom?q={0}&since_id={1}"; } }
		}

		class RetweetUris : IQueryUris
		{
			public string InitialQuery { get { return "http://api.twitter.com/1/statuses/retweeted_by_user.atom?id={0}&count=1"; } }
			public string SubsequentQuery { get { return "http://api.twitter.com/1/statuses/retweeted_by_user.atom?id={0}&since_id={1}"; } }
		}

        public string CurrentSearchTerm { get; private set; }

		public double PollingIntervalMs { get; set; }

		public event EventHandler<NewTweetEventArgs> NewTweet;

        private Timer timer;

		private string LastId = string.Empty;

		private IQueryUris QueryUris { get; set; }

		public Tweeter()
		{
			// Defaults
			PollingIntervalMs = Settings.Default.TwitterPollingIntervalMs;

			switch (Settings.Default.TwitterMode)
			{
				case "Search":
					Logger.Instance.Write("Setting up Tweeter in Search mode");
					QueryUris = new SearchUris();
					break;

				case "Retweet":
					Logger.Instance.Write("Setting up Tweeter in Retweet mode");
					QueryUris = new RetweetUris();
					break;

				default:
					throw new ArgumentOutOfRangeException("TwitterMode", "Should be one of Search, Retweet");
			}
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
				string url;

				Logger.Instance.Write("\nTweeter polling for {0}", CurrentSearchTerm);

				if (string.IsNullOrEmpty(LastId))
					url = string.Format(QueryUris.InitialQuery, HttpUtility.UrlEncode(CurrentSearchTerm));
				else
					url = string.Format(QueryUris.SubsequentQuery, HttpUtility.UrlEncode(CurrentSearchTerm), LastId);

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

					LastId = tweet.NumericId;

					Logger.Instance.Write("\nReceived Tweet : {0}", tweet.Text);

					OnNewTweet(tweet);
				}
			}
			catch (Exception ex)
			{
				Logger.Instance.Write("Tweeter poll failed : {0}", ex);
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
