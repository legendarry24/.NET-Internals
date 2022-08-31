using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using PI.Practice.Original.Core;

namespace PI.Practice.Original.Benchmarks.Reader
{
	public class ReaderBenchmarks : BenchmarkBase
	{
		private FileInputReader reader;

		[GlobalSetup]
		public void GlobalSetup()
		{
			// executed once per each TestFileName value. Execution time of this method does not affect benchmarks
			this.reader = new FileInputReader($"{testDataFolder}\\{this.TestFileName}");
		}

		[Benchmark(Baseline = true)]
		public int FileInputReader_Read()
		{
			IEnumerable<string> lines = this.reader.Read();
			int count = 0;
			foreach (var line in lines)
			{
				count++;
			}

			return count;
		}

		// Benchmark for TASK 2
		[Benchmark]
		public async Task<int> FileInputReader_ReadAsync()
		{
			IEnumerable<string> lines = await this.reader.ReadAsync();
			int count = 0;
			foreach (var line in lines)
			{
				count++;
			}

			return count;
		}

		// Benchmark for TASK 4
		[Benchmark]
		public async Task<int> FileInputReader_ReadIteratorAsync()
		{
			IAsyncEnumerable<string> lines = this.reader.ReadIteratorAsync();
			int count = 0;
			await foreach (var line in lines)
			{
				count++;
			}

			return count;
		}

		// Benchmark for TASK 4
		[Benchmark]
		public async Task<int> FileInputReader_ReadEnumerableAsync()
		{
			IAsyncEnumerable<string> lines = this.reader.ReadEnumerableAsync();
			int count = 0;
			await foreach (var line in lines)
			{
				count++;
			}

			return count;
		}
	}
}