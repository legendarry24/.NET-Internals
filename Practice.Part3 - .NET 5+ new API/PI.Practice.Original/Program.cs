using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using PI.Practice.Original.Benchmarks.Calculator;
using PI.Practice.Original.Benchmarks.Parser;
using PI.Practice.Original.Benchmarks.Reader;
using PI.Practice.Original.Core;
using PI.Practice.Original.Utils;

namespace PI.Practice.Original
{
	class Program
	{
		static void Main(string[] args)
		{
			// Use this method to debug your implementation.
			//TestMethod().Wait();

			// These methods were used for test data generation / check.
			// Normally you will not use them unless you want to regenerate data.
			//PrintTestFilesLinesDuplications();
			//GenerateTestData();

			// Use this method to run all benchmarks
			// Benchmarks are executed only in RELEASE mode!!!
			FullBenchmarksRun();
		}

		private static void FullBenchmarksRun()
		{
			// Benchmarks are performed on a set of files, that contain 1, 10, 100, 1000 and 10000 lines of integers
			// Files on which benchmarks are performed are controlled by the BenchmarkBase.TestFileName property
			// By default (for development purpose) it is set to a one file with 10 lines.
			// If you uncomment [Params("Input.txt", "Input_10.txt", "Input_100.txt", "Input_1000.txt", "Input_10000.txt")], then benchmarks will be run on all these files.

			// Reults of benchmarks can be found in bin\Release\net5.0\BenchmarkDotNet.Artifacts\results
			// They are written in multiple formats (.asciidoc, .csv, .html, .md)

			// Isolated benchmarks for reading lines from file
			BenchmarkRunner.Run<ReaderBenchmarks>();

			// Isolated benchmarks for parsing string 
			BenchmarkRunner.Run<ParserBenchmarks>();

			// Benchmark for calculating sum of a single line.
			// Includes parsing the line and then performing sum of parsed integers
			BenchmarkRunner.Run<SingleLineCalculatorBenchmarks>();

			// Benchmark for calculating sum of a all lines in file
			// Includes reading lines from file, parsing each line, then performing sum of parsed integers and then performing sum of sums from all lines
			BenchmarkRunner.Run<CalculatorBenchmarks>();
		}

		private static void PrintTestFilesLinesDuplications()
		{
			string[] files = new[] { "Input.txt", "Input_10.txt", "Input_100.txt", "Input_1000.txt", "Input_10000.txt" };

			string testDataFolder = Path.Combine(
				Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath),
				"AppData");

			foreach (var f in files)
			{
				string[] lines = File.ReadAllLines(Path.Combine(testDataFolder, f));
				int uniqueCount = new HashSet<string>(lines).Count;
				int duplicatesCount = lines.Length - uniqueCount;
				double duplicatesPercentage = duplicatesCount * 100 / lines.Length;
				Console.WriteLine($"File {f}, duplicated lines: {duplicatesCount} ({duplicatesPercentage} %)");
			}
		}

		private static void GenerateTestData()
		{
			string testDataFolder = Path.Combine(
				Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath),
				"AppData");

			File.WriteAllLines(Path.Combine(testDataFolder, "Input.txt"), InputGenerator.GenerateLinesOfNumbers(6, 1, ",", duplicatesPercentage: 40));
			File.WriteAllLines(Path.Combine(testDataFolder, "Input_10.txt"), InputGenerator.GenerateLinesOfNumbers(6, 10, ",", duplicatesPercentage: 40));
			File.WriteAllLines(Path.Combine(testDataFolder, "Input_100.txt"), InputGenerator.GenerateLinesOfNumbers(6, 100, ",", duplicatesPercentage: 40));
			File.WriteAllLines(Path.Combine(testDataFolder, "Input_1000.txt"), InputGenerator.GenerateLinesOfNumbers(6, 1000, ",", duplicatesPercentage: 40));
			File.WriteAllLines(Path.Combine(testDataFolder, "Input_10000.txt"), InputGenerator.GenerateLinesOfNumbers(6, 10000, ",", duplicatesPercentage: 40));
		}

		private static async Task TestMethod()
		{
			string testDataFolder = Path.Combine(
				Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath),
				"AppData");
			int sum = await new IntegersCalculator($"{testDataFolder}\\Input_10.txt", ",").SumMemoryWithCacheValueEnumerableAsync();

			Console.WriteLine(sum);
		}
	}
}