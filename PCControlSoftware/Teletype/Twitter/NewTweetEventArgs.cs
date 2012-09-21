using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teletype.Twitter
{
	class NewTweetEventArgs : EventArgs
	{
		public Tweet Tweet { get; private set; }

		public NewTweetEventArgs(Tweet t)
		{
			Tweet = t;
		}
	}
}
