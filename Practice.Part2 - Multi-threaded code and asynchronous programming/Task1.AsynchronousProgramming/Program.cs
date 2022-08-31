using System;

namespace Task1.AsynchronousProgramming
{
	class Program
	{
		static void Main(string[] args)
		{
			var urls = new[]
			{
				@"https://marksheet.io/",
				@"http://javascript.info/",
				@"https://learn.jquery.com/",
				@"https://webconfigtransformationtester.apphb.com/"
			};

			var websiteLoader = new WebsiteLoader();
			websiteLoader.LoadMany(urls);
			websiteLoader.DownloadMany(urls);

			Console.ReadKey();
		}
	}
}