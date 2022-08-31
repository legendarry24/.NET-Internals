using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Task1.AsynchronousProgramming
{
	public class WebsiteLoader
	{
		public void LoadMany(params string[] urls)
		{
			Stopwatch sw = Stopwatch.StartNew();

			foreach (string url in urls)
			{
				this.Load(url);
			}

			sw.Stop();
			Console.WriteLine($"Total Time: {sw.ElapsedMilliseconds}ms.");
		}

		public void Load(string url)
		{
			Stopwatch sw = Stopwatch.StartNew();
			string response;

			using (var client = new WebClient())
			{
				response = client.DownloadString(url);
			}

			sw.Stop();
			Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms. | Length: {response.Length} | Url: {url} ");
		}

		public void DownloadMany(params string[] urls)
		{
			Stopwatch sw = Stopwatch.StartNew();
			var tasks = new List<Task>();

			foreach (string url in urls)
			{
				tasks.Add(this.DownloadAsync(url));
			}

			Console.WriteLine("\nAsync implementation:");

			Task.WaitAll(tasks.ToArray()); // blocking wait

			// could use await here and make this method async:
			// await Task.WhenAll(tasks.ToArray());

			sw.Stop();
			Console.WriteLine($"Total Time: {sw.ElapsedMilliseconds}ms.");
		}

		// non-blocking async method
		public async Task DownloadAsync(string url)
		{
			Stopwatch sw = Stopwatch.StartNew();
			string response;

			// HttpClient can be used for this purpose and it might improve performance
			using (var client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(url);
			}

			sw.Stop();
			Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms. | Length: {response.Length} | Url: {url} ");
		}
	}
}