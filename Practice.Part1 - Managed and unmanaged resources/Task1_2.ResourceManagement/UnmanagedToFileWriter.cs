using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Task1_2.ResourceManagement
{
	public class UnmanagedToFileWriter : IDisposable
	{
		private readonly MySafeFileHandle handle;
		private bool disposed;
		private IntPtr bytesPointer;

		public string FilePath { get; }

		public UnmanagedToFileWriter()
		{
			this.FilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");

			this.handle = NativeMethods.CreateFile(
				filename: this.FilePath,
				access: FileAccess.Write,
				share: FileShare.Write,
				securityAttributes: IntPtr.Zero,
				creationDisposition: FileMode.CreateNew,
				flagsAndAttributes: FileAttributes.Normal,
				templateFile: IntPtr.Zero);

			if (this.handle.IsInvalid)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		public void WriteToFile()
		{
			var bytes = Encoding.UTF8.GetBytes("Text written by unmanaged writer...");

			this.bytesPointer = Marshal.AllocHGlobal(bytes.Length);
			Marshal.Copy(bytes, 0, this.bytesPointer, bytes.Length);

			NativeMethods.WriteFile(
				fileHandle: this.handle,
				buffer: this.bytesPointer,
				count: bytes.Length,
				resultCount: out int bytesWritten,
				lpOverlapped: IntPtr.Zero);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}

			if (disposing)
			{
				if (this.handle != null && !this.handle.IsInvalid)
				{
					// Free the handle
					this.handle.Dispose();
				}
			}

			Marshal.FreeHGlobal(this.bytesPointer);
			this.bytesPointer = IntPtr.Zero;

			this.disposed = true;
		}

		~UnmanagedToFileWriter()
		{
			this.Dispose(false);
		}
	}
}