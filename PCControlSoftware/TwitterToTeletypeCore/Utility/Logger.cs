using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTT.Utility
{
	public class Logger
	{
		static readonly object locker = new object();

		static Logger instance;

		public static Logger Instance
		{
			get
			{
				if (instance == null)
					lock (locker)
						if (instance == null)
							instance = new Logger();

				return instance;
			}
		}

		public void Write(string message, params object[] args)
		{
			//System.Diagnostics.Debug.WriteLine(string.Format(message, args));
			Console.WriteLine(string.Format(message, args));
		}

		public void Error(string message, Exception ex)
		{
			//System.Diagnostics.Debug.WriteLine(string.Format("Error {0}, {1}", message,ex.ToString()));
			Console.Error.WriteLine(string.Format("Error {0}, {1}", message, ex.ToString()));
		}
	}
}
