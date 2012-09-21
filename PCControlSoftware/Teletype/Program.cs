using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Teletype
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();

			Application.Run(new TeletypeForm());
		}
	}
}
