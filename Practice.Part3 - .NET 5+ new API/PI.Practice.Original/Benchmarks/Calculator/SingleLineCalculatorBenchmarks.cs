using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using PI.Practice.Original.Core;

namespace PI.Practice.Original.Benchmarks.Calculator
{
	public class SingleLineCalculatorBenchmarks : BenchmarkBase
	{
		private string[] lines;
		private SingleLineCalculator calculator;

		[GlobalSetup]
		public void GlobalSetup()
		{
			// executed once per each testFileName value. Execution time of this method does not affect benchmarks
			this.lines = new FileInputReader($"{testDataFolder}\\{this.TestFileName}").Read().ToArray();
			this.calculator = new SingleLineCalculator(new IntegersParser(","));
		}

		[Benchmark(Baseline = true)]
		public int SingleLineCalculator_Sum()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += this.calculator.Sum(l);
			}
			return sum;
		}

		// Benchmark for TASK 1
		[Benchmark]
		public int IntegersCalculater_SumSpan()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += this.calculator.SumSpan(l);
			}
			return sum;
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task<int> IntegersCalculater_SumAsync()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += await this.calculator.SumAsync(l);
			}
			return sum;
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryAsync()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += await this.calculator.SumMemoryAsync(l.AsMemory());
			}
			return sum;
		}

		// Benchmark for TASK 3
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheAsync()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += await this.calculator.SumMemoryWithCacheAsync(l.AsMemory());
			}
			return sum;
		}

		// Benchmark for TASK 3
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheValueAsync()
		{
			int sum = 0;
			foreach (var l in this.lines)
			{
				sum += await this.calculator.SumMemoryWithCacheValueAsync(l.AsMemory());
			}
			return sum;
		}

		[GlobalCleanup]
		public void GlobalCleanup()
		{
			// Disposing logic
		}
	}
}