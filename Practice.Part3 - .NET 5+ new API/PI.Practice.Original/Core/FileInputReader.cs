using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PI.Practice.Original.Core
{
	public class FileInputReader
	{
		private readonly string filePath;

		public FileInputReader(string filePath)
		{
			this.filePath = filePath;
		}

		public IEnumerable<string> Read()
		{
			var res = new List<string>();
			using (var s = File.OpenRead(this.filePath))
			{
				using (var r = new StreamReader(s))
				{
					while (!r.EndOfStream)
					{
						res.Add(r.ReadLine());
					}
				}
			}

			return res;
		}

		// TASK 2
		public async Task<IEnumerable<string>> ReadAsync()
		{
			var lines = new List<string>();

			// This importantly uses Windows I/O ports to await this without ANY CPU threads,
			// while the Task.Factory.StartNew/Task.Run approach would waste a CPU thread.
			await using (var fileStream =
				new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
			{
				using var reader = new StreamReader(fileStream);
				string line;
				while ((line = await reader.ReadLineAsync()) != null)
				{
					lines.Add(line);
				}
			}

			return lines;
		}

		// TASK 4
		public async IAsyncEnumerable<string> ReadIteratorAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			await using var fileStream =
				new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
			using var reader = new StreamReader(fileStream);

			string line;
			while (!cancellationToken.IsCancellationRequested && (line = await reader.ReadLineAsync()) != null)
			{
				yield return line;
			}
		}

		// TASK 4
		public IAsyncEnumerable<string> ReadEnumerableAsync()
		{
			return new FileInputAsyncEnumerable(this.filePath);
		}
	}
}