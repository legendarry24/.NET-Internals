using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PI.Practice.Original.Core
{
	// TASK 4
	public class FileInputAsyncEnumerable : IAsyncEnumerable<string>
	{
		private readonly string filePath;

		public FileInputAsyncEnumerable(string filePath)
		{
			this.filePath = filePath;
		}

		public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
		{
			return new FileInputAsyncEnumerator(this.filePath, cancellationToken);
		}

		// TASK 4
		private class FileInputAsyncEnumerator : IAsyncEnumerator<string>, IDisposable
		{
			private readonly CancellationToken token;
			private FileStream fileStream;
			private StreamReader reader;

			public FileInputAsyncEnumerator(string filePath, CancellationToken token)
			{
				this.fileStream = File.OpenRead(filePath);
				this.reader = new StreamReader(this.fileStream);
				this.token = token;
			}

			public string Current { get; private set; }

			public async ValueTask<bool> MoveNextAsync()
			{
				return !this.token.IsCancellationRequested && (this.Current = await this.reader.ReadLineAsync()) != null;
			}

			public void Dispose()
			{
				this.Dispose(disposing: true);
			}

			public async ValueTask DisposeAsync()
			{
				await this.DisposeAsyncCore();

				this.Dispose(disposing: false);
			}

			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.reader?.Dispose();
					(this.fileStream as IDisposable)?.Dispose();

					this.reader = null;
					this.fileStream = null;
				}
			}

			private async ValueTask DisposeAsyncCore()
			{
				if (this.fileStream != null)
				{
					await this.fileStream.DisposeAsync().ConfigureAwait(false);
				}

				if (this.reader is IAsyncDisposable asyncDisposable)
				{
					await asyncDisposable.DisposeAsync().ConfigureAwait(false);
				}
				else
				{
					this.reader?.Dispose();
				}

				this.fileStream = null;
				this.reader = null;
			}
		}
	}
}