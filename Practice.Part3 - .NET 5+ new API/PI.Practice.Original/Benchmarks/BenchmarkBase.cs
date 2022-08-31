using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System;
using System.IO;
using System.Reflection;

namespace PI.Practice.Original.Benchmarks
{
	[MemoryDiagnoser]
	//[NativeMemoryProfiler]

	[ThreadingDiagnoser]
	//[HardwareCounters(
	//	HardwareCounter.BranchMispredictions,
	//	HardwareCounter.BranchInstructions)]

	[PlainExporter]
	[AsciiDocExporter]
	[HtmlExporter]
	public class BenchmarkBase
	{
		public static readonly string testDataFolder = Path.Combine(
			Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath),
			"AppData");

		//[Params("Input.txt", "Input_10.txt", "Input_100.txt", "Input_1000.txt", "Input_10000.txt")]
		public string TestFileName = "Input_10.txt";
	}
}
