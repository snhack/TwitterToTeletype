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
		public string Id2005 { get; set; }
		public DateTime Published { get; set; }
		public string Text { get; set; }
		public string Html { get; set; }
		public string ImageUri { get; set; }
		public string Author { get; set; }
		public string AuthorUri { get; set; }

		public static Tweet FromNode(XmlNode node, XmlNamespaceManager nsmgr)
		{
			Tweet t = new Tweet();

			t.Id = node.SelectSingleNode("atom:id", nsmgr).InnerText;
			t.Id2005 = GetId2005(t.Id);
			t.Published = DateTime.Parse(node.SelectSingleNode("atom:published", nsmgr).InnerText);
			t.Text = node.SelectSingleNode("atom:title", nsmgr).InnerText;
			t.Html = node.SelectSingleNode("atom:content[@type=\"html\"]", nsmgr).InnerText;
			t.ImageUri = node.SelectSingleNode("atom:link[@rel=\"image\"]", nsmgr).Attributes["href"].Value;
			t.Author = node.SelectSingleNode("atom:author", nsmgr).SelectSingleNode("atom:name", nsmgr).InnerText;
			t.AuthorUri = node.SelectSingleNode("atom:author", nsmgr).SelectSingleNode("atom:uri", nsmgr).InnerText;

			return t;
		}

		/// <summary>
		/// Twitter Id's appear to come back as "tag:search.twitter.com,2005:235812528511406080" 
		/// The since_id paramter of the search api requires the int after 2005: so we split it out here
		/// </summary>
		private static string GetId2005(string fullId)
		{
			var t5 = fullId.Split(',').FirstOrDefault(i => i.StartsWith("2005:"));
			if (null != t5)
			{
				return t5.Split(':')[1];
			}
			else
			{
				return string.Empty;
			}
		}
	}
}
