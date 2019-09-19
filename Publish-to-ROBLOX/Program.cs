using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Publish_to_ROBLOX
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 3) Environment.Exit(1);
			if (!UInt64.TryParse(args[1], out ulong id))
			{
				Console.WriteLine("Arguments not valid.");
				Environment.Exit(1);
			}
			var cookie = args[2];
			var url = string.Format("http://data.roblox.com/Data/Upload.ashx?assetid={0}", id);

			using (var c = new WebClient())
			{
				var h = c.Headers;
				h.Add("Accept", "*/*");
				h.Add("Cookie", ".ROBLOSECURITY=" + cookie);
				h.Add("Content-Type", "application/xml");
				h.Add("User-Agent", "Roblox/WinInet");
				using (var str = new StreamContent(new FileStream(args[0], FileMode.Open)))
				{
					try { c.UploadData(url, str.ReadAsByteArrayAsync().Result); }
					catch (WebException)
					{
						Console.WriteLine("Upload Failed.");
						Environment.Exit(2);
					}
				}
			}
		}
	}
}
