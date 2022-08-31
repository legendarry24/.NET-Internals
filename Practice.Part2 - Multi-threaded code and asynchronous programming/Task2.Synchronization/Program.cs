using System;

namespace Task2.Synchronization
{
	class Program
	{
		static void Main()
		{
			new ParallelCounter().CountInParallel();
			Console.ReadKey();
		}
	}
}