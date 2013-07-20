using System;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;
using Codeplex.Data;
using TTT.Properties;
using TTT.Utility;

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
			dynamic ResultSelector(dynamic json);
			bool TweetMatches(Tweet t);
		}

		class SearchUris : IQueryUris
		{
			public string InitialQuery { get { return "https://api.twitter.com/1.1/search/tweets.json?q={0}&count=1"; } }
			public string SubsequentQuery { get { return "https://api.twitter.com/1.1/search/tweets.json?q={0}&since_id={1}"; } }
			public dynamic ResultSelector(dynamic json) { return json.statuses; }
			public bool TweetMatches(Tweet t) { return true; }
		}

		class RetweetUris : IQueryUris
		{
			public string InitialQuery { get { return "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&count=1"; } }
			public string SubsequentQuery { get { return "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&since_id={1}"; } }
			public dynamic ResultSelector(dynamic json) { return json; }
			public bool TweetMatches(Tweet t) { return t.IsRetweet; }
		}

        public string CurrentSearchTerm { get; private set; }
		public double PollingIntervalMs { get; set; }

		public event EventHandler<NewTweetEventArgs> NewTweet;

        private Timer timer;
		private string lastId = string.Empty;
		private IQueryUris queryUris;
		private string authHeader;

		public Tweeter()
		{
			// Defaults
			PollingIntervalMs = Settings.Default.TwitterPollingIntervalMs;

			switch (Settings.Default.TwitterMode)
			{
				case "Search":
					Logger.Instance.Write("Setting up Tweeter in Search mode");
					queryUris = new SearchUris();
					break;

				case "Retweet":
					Logger.Instance.Write("Setting up Tweeter in Retweet mode");
					queryUris = new RetweetUris();
					break;

				default:
					throw new ArgumentOutOfRangeException("TwitterMode", "Should be one of Search, Retweet");
			}

			authHeader = GetAppAuthHeader();
		}

		private static string GetAppAuthHeader()
		{
			var ascii = new ASCIIEncoding();

			if (string.IsNullOrWhiteSpace(Settings.Default.OauthConsumerKey) || string.IsNullOrWhiteSpace(Settings.Default.OauthConsumerSecret))
				throw new ApplicationException("You need to configure the Twitter Oauth keys in the settings file");

			var bearerTokenCredentials = Settings.Default.OauthConsumerKey + ":" + Settings.Default.OauthConsumerSecret;
			var bearerTokenCredentialsBase64 = Convert.ToBase64String(ascii.GetBytes(bearerTokenCredentials));

			var tokenRequest = (HttpWebRequest)WebRequest.Create("https://api.twitter.com/oauth2/token");
			tokenRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + bearerTokenCredentialsBase64;
			tokenRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8.";
			tokenRequest.Method = "POST";
			var stream = tokenRequest.GetRequestStream();
			string postData = "grant_type=client_credentials";
			byte[] byteArray = Encoding.UTF8.GetBytes(postData);
			stream.Write(byteArray, 0, byteArray.Length);

			var tokenResponse = tokenRequest.GetResponse();
			string json;
			using (var reader = new StreamReader(tokenResponse.GetResponseStream()))
				json = reader.ReadToEnd();

			var jr = DynamicJson.Parse(json);

			if (jr.token_type != "bearer") throw new ApplicationException("Invalid Token Type : " + jr.token_type);

			return "Bearer " + jr.access_token;
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

				Logger.Instance.Write(Environment.NewLine + "Tweeter polling for {0}", CurrentSearchTerm);

				if (string.IsNullOrEmpty(lastId))
					url = string.Format(queryUris.InitialQuery, HttpUtility.UrlEncode(CurrentSearchTerm));
				else
					url = string.Format(queryUris.SubsequentQuery, HttpUtility.UrlEncode(CurrentSearchTerm), lastId);

				Logger.Instance.Write("Fetching {0}", url);

				ServicePointManager.Expect100Continue = false;
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
				webRequest.Headers[HttpRequestHeader.Authorization] = authHeader;
				webRequest.ContentType = "application/x-www-form-urlencoded";
				webRequest.Method = "GET";

				var webResponse = webRequest.GetResponse();
				var json = string.Empty;
				using (var reader = new StreamReader(webResponse.GetResponseStream()))
					json = reader.ReadToEnd();

				var dj = DynamicJson.Parse(json);
				var tweets = queryUris.ResultSelector(dj);
				
				foreach (var jt in tweets)
				{
					var tweet = new Tweet
					{
						Author = jt.user.name, //tweet.user.screen_name
						Id = jt.id.ToString(),
						ImageUri = "",
						IsRetweet = jt.retweeted_status(),
						NumericId = jt.id_str,
						Published = DateTime.ParseExact(jt.created_at, "ddd MMM dd HH:mm:ss zzz yyyy", null), //"Wed Jul 03 22:05:32 +0000 2013"
						Text = jt.text
					};
					if (tweet.IsRetweet)
						tweet.OriginalAuthor = jt.retweeted_status.user.name;

					lastId = tweet.NumericId;

					Logger.Instance.Write(Environment.NewLine + "Received Tweet : {0}", tweet.Text);

					if (queryUris.TweetMatches(tweet))
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
