using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using PI.Practice.Original.Core;

namespace PI.Practice.Original.Benchmarks.Calculator
{
	public class CalculatorBenchmarks : BenchmarkBase
	{
		private IntegersCalculator calculator;

		[GlobalSetup]
		public void GlobalSetup()
		{
			this.calculator = new IntegersCalculator($"{testDataFolder}\\{this.TestFileName}", ","); // executed once per each testFileName value
		}

		[Benchmark(Baseline = true)]
		public int IntegersCalculater_Sum()
		{
			int sum = this.calculator.Sum();
			return sum;
		}

		// Benchmark for TASK 1
		[Benchmark]
		public int IntegersCalculater_SumSpan()
		{
			int sum = this.calculator.SumSpan();
			return sum;
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task<int> IntegersCalculater_SumAsync()
		{
			int sum = await this.calculator.SumAsync();
			return sum;
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryAsync()
		{
			int sum = await this.calculator.SumMemoryAsync();
			return sum;
		}

		// Benchmark for TASK 3
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheAsync()
		{
			int sum = await this.calculator.SumMemoryWithCacheAsync();
			return sum;
		}

		// Benchmark for TASK 3
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheValueAsync()
		{
			int sum = await this.calculator.SumMemoryWithCacheValueAsync();
			return sum;
		}

		// Benchmark for TASK 4
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheValueIteratorAsync()
		{
			int sum = await this.calculator.SumMemoryWithCacheValueIteratorAsync();
			return sum;
		}

		// Benchmark for TASK 4
		[Benchmark]
		public async Task<int> IntegersCalculater_SumMemoryWithCacheValueEnumerableAsync()
		{
			int sum = await this.calculator.SumMemoryWithCacheValueEnumerableAsync();
			return sum;
		}

		[GlobalCleanup]
		public void GlobalCleanup()
		{
			// Disposing logic
		}
	}
}