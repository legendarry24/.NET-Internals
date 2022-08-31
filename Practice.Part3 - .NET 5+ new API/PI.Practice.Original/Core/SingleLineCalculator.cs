using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI.Practice.Original.Core
{
	public class SingleLineCalculator
	{
		private readonly IntegersParser parser;
		// TASK 3
		private readonly IDictionary<int, int> cache;

		public SingleLineCalculator(IntegersParser parser)
		{
			this.parser = parser;
			this.cache = new Dictionary<int, int>();
		}

		public int Sum(string line)
		{
			return this.parser.Parse(line).Sum();
		}

		public int SumSpan(ReadOnlySpan<char> line)
		{
			return this.parser.ParseSpan(line).Sum();
		}

		public async Task<int> SumAsync(string line)
		{
			return (await this.parser.ParseAsync(line)).Sum();
		}

		public async Task<int> SumMemoryAsync(ReadOnlyMemory<char> line)
		{
			return (await this.parser.ParseMemoryAsync(line)).Sum();
		}

		// TASK 3
		public async Task<int> SumMemoryWithCacheAsync(ReadOnlyMemory<char> line)
		{
			int hash = CalculateHashCode(line);
			if (this.cache.ContainsKey(hash))
			{
				return this.cache[hash];
			}

			int sum = await this.SumMemoryAsync(line);
			this.cache.Add(hash, sum);

			return sum;
		}

		// TASK 3
		public async ValueTask<int> SumMemoryWithCacheValueAsync(ReadOnlyMemory<char> line)
		{
			int hash = CalculateHashCode(line);
			if (this.cache.ContainsKey(hash))
			{
				return this.cache[hash];
			}

			int sum = await this.SumMemoryAsync(line);
			this.cache.Add(hash, sum);

			return sum;
		}

		// TASK 3
		public static int CalculateHashCode(ReadOnlyMemory<char> input)
		{
			var span = input.Span;

			int hashCode = span[0];
			for (int i = 1; i < span.Length; i++)
			{
				hashCode ^= span[i].GetHashCode();
			}

			return hashCode;
		}
	}
}