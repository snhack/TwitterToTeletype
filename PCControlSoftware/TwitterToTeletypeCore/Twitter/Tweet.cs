using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace TTT.Twitter
{
	public class Tweet
	{
		public string Id { get; set; }
		public string NumericId { get; set; }
		public DateTime Published { get; set; }
		public string Text { get; set; }
		public string Html { get; set; }
		public string ImageUri { get; set; }
		public string Author { get; set; }
		public string ScreenName { get; set; }
		public bool IsRetweet { get; set; }

		/// <summary>
		/// Twitter Id's appear to come back as "tag:search.twitter.com,2005:235812528511406080" 
		/// or as "tag:twitter.com,2007:http://twitter.com/snhack/statuses/248786105875447808"
		/// The since_id paramter of the search api requires the int at the end so we split it out here
		/// </summary>
		private static string GetNumericId(string fullId)
		{
			var spl = fullId.Split(',');
			var t5 = spl.FirstOrDefault(i => i.StartsWith("2005:"));
			if (null != t5)
			{
				return t5.Split(':')[1];
			}
			else
			{
				var t7 = spl.FirstOrDefault(i => i.StartsWith("2007:"));
				if (null != t7)
				{
					var tweetUrl = t7.Split(':')[2];
					var items = tweetUrl.Split('/');
					return items[items.Length - 1];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		public string OriginalAuthor { get; set; }
	}
}
