using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Task1_2.ResourceManagement
{
	// .NET class Microsoft.Win32.SafeHandles.SafeFileHandle can be used instead of this custom implementation
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public sealed class MySafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Create a SafeHandle, informing the base class that this SafeHandle instance "owns" the handle,
		// and therefore SafeHandle should call our ReleaseHandle method when the SafeHandle is no longer in use.
		private MySafeFileHandle()
			: base(true)
		{
		}

		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseHandle(this.handle);
		}
	}
}