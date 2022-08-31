using BenchmarkDotNet.Attributes;
using PI.Practice.Original.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PI.Practice.Original.Benchmarks.Parser
{
	public class ParserBenchmarks : BenchmarkBase
	{
		private string[] lines;
		private IntegersParser parser;

		[GlobalSetup]
		public void GlobalSetup()
		{
			// executed once per each TestFileName value. Execution time of this method does not affect benchmarks
			this.lines = new FileInputReader($"{testDataFolder}\\{this.TestFileName}").Read().ToArray();
			this.parser = new IntegersParser(",");
		}

		[Benchmark(Baseline = true)]
		public void IntegersParser_Parse()
		{
			foreach (var line in this.lines)
			{
				this.parser.Parse(line);
			}
		}

		// Benchmark for TASK 1
		[Benchmark]
		public void IntegersParser_ParseSpan()
		{
			foreach (var line in this.lines)
			{
				this.parser.ParseSpan(line);
			}
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task IntegersParser_ParseAsync()
		{
			foreach (var line in this.lines)
			{
				await this.parser.ParseAsync(line);
			}
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task IntegersParser_ParseMemoryAsync()
		{
			foreach (var line in this.lines)
			{
				await this.parser.ParseMemoryAsync(line.AsMemory());
			}
		}

		[GlobalCleanup]
		public void GlobalCleanup()
		{
			// Disposing logic
		}
	}
}
