using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Task1_2.ResourceManagement
{
	public static class NativeMethods
	{
		#region Import kernel32.dll functions

		// Allocate a file object in the kernel, then return a handle to it.
		// as a return type can be used SafeFileHandle or IntPtr types
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern MySafeFileHandle CreateFile(
			[MarshalAs(UnmanagedType.LPTStr)] string filename,
			[MarshalAs(UnmanagedType.U4)] FileAccess access,
			[MarshalAs(UnmanagedType.U4)] FileShare share,
			IntPtr securityAttributes,
			[MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
			[MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
			IntPtr templateFile);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool WriteFile(
			MySafeFileHandle fileHandle,
			IntPtr buffer,
			int count,
			out int resultCount,
			IntPtr lpOverlapped);

		// Free the kernel's file object (close the file)
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

		#endregion
	}
}