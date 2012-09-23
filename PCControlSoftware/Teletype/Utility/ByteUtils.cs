using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teletype.Utility
{
	static class ByteUtils
	{
		/// <summary>
		/// Print byte out in format "124 : 01101101"
		/// </summary>
		public static string DebugText(this byte b)
		{
			var text = new StringBuilder();

			text.Append((char)b);
			text.Append(" (");
			text.Append(b);
			text.Append(") : ");
			text.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

			return text.ToString();
		}
	}
}
