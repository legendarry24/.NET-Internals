using System;
using System.IO;
using System.Text;

namespace Task1_2.ResourceManagement
{
	public sealed class ManagedToFileWriter : IDisposable
	{
		private bool disposed = false;

		private Stream SomeFile { get; }

		public string FilePath { get; }

		public ManagedToFileWriter()
		{
			this.FilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");

			this.SomeFile = File.Create(this.FilePath);
		}

		public void WriteToFile()
		{
			var bytes = Encoding.UTF8.GetBytes("Text written by managed writer...");

			this.SomeFile.Write(
				buffer: bytes,
				offset: 0,
				count: bytes.Length);
		}

		public void Dispose()
		{
			this.Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}

			if (disposing)
			{
				this.SomeFile?.Dispose();
			}

			this.disposed = true;
		}
	}
}