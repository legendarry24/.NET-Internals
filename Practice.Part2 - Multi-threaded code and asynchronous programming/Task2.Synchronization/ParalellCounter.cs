using System;
using System.Threading;

namespace Task2.Synchronization
{
	public class ParallelCounter
	{
		//private static readonly Mutex LocalMutex = new Mutex();
		private static readonly AutoResetEvent AutoResetEvent = new AutoResetEvent(false);

		public void CountInParallel()
		{
			var threads = new Thread[10];

			for (int i = 0; i < 10; i++)
			{
				threads[i] = new Thread(this.Count)
				{
					Name = i.ToString(),
					IsBackground = false
				};

				threads[i].Start();

				AutoResetEvent.WaitOne();
			}
		}

		public void Count()
		{
			try
			{
				//LocalMutex.WaitOne();

				Console.Write("<T_id:{0}>", Thread.CurrentThread.Name);

				for (int i = 0; i < 20; i++)
				{
					Console.Write(i);
				}

				Console.Write("</T_id:{0}>\n", Thread.CurrentThread.Name);
			}
			finally
			{
				//LocalMutex.ReleaseMutex();
				AutoResetEvent.Set();
			}
		}
	}
}