using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PI.Practice.Original.Core
{
	public class IntegersCalculator
	{
		private readonly FileInputReader reader;
		private readonly SingleLineCalculator lineCalculator;

		public IntegersCalculator(string integersFilePath, string integersSeparator)
		{
			this.reader = new FileInputReader(integersFilePath);
			this.lineCalculator = new SingleLineCalculator(new IntegersParser(integersSeparator));
		}

		public int Sum()
		{
			int res = 0;
			IEnumerable<string> lines = this.reader.Read();
			foreach (string line in lines)
			{
				res += this.lineCalculator.Sum(line);
			}

			return res;
		}

		public int SumSpan()
		{
			int res = 0;
			IEnumerable<string> lines = this.reader.Read();
			foreach (string line in lines)
			{
				res += this.lineCalculator.SumSpan(line);
			}

			return res;
		}

		public async Task<int> SumAsync()
		{
			int res = 0;
			IEnumerable<string> lines = await this.reader.ReadAsync();
			foreach (string line in lines)
			{
				res += await this.lineCalculator.SumAsync(line);
			}

			return res;
		}

		public async Task<int> SumMemoryAsync()
		{
			int res = 0;
			IEnumerable<string> lines = await this.reader.ReadAsync();
			foreach (string line in lines)
			{
				res += await this.lineCalculator.SumMemoryAsync(line.AsMemory());
			}

			return res;
		}

		public async Task<int> SumMemoryWithCacheAsync()
		{
			int res = 0;
			IEnumerable<string> lines = await this.reader.ReadAsync();
			foreach (string line in lines)
			{
				res += await this.lineCalculator.SumMemoryWithCacheAsync(line.AsMemory());
			}

			return res;
		}

		public async Task<int> SumMemoryWithCacheValueAsync()
		{
			int res = 0;
			IEnumerable<string> lines = await this.reader.ReadAsync();
			foreach (string line in lines)
			{
				res += await this.lineCalculator.SumMemoryWithCacheValueAsync(line.AsMemory());
			}

			return res;
		}

		public async Task<int> SumMemoryWithCacheValueIteratorAsync(CancellationToken token = default)
		{
			int res = 0;
			ConfiguredCancelableAsyncEnumerable<string> enumerable = this.reader.ReadIteratorAsync().WithCancellation(token);
			await foreach (string line in enumerable)
			{
				res += await this.lineCalculator.SumMemoryWithCacheValueAsync(line.AsMemory());
			}

			return res;
		}

		public async Task<int> SumMemoryWithCacheValueEnumerableAsync(CancellationToken token = default)
		{
			int res = 0;
			ConfiguredCancelableAsyncEnumerable<string> enumerable = this.reader.ReadEnumerableAsync().WithCancellation(token);
			await foreach (string line in enumerable)
			{
				res += await this.lineCalculator.SumMemoryWithCacheValueAsync(line.AsMemory());
			}

			return res;
		}
	}
}