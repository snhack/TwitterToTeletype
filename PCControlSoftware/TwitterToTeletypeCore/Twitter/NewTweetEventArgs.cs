using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTT.Twitter
{
	public class NewTweetEventArgs : EventArgs
	{
		public Tweet Tweet { get; private set; }

		public NewTweetEventArgs(Tweet t)
		{
			Tweet = t;
		}
	}
}
