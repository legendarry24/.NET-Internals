using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI.Practice.Original.Core
{
	public class IntegersParser
	{
		private readonly string separator;

		public IntegersParser(string separator)
		{
			this.separator = separator;
		}

		public IEnumerable<int> Parse(string line)
		{
			return line.Split(this.separator).Select(s => int.Parse(s));
		}

		// TASK 1
		public IEnumerable<int> ParseSpan(ReadOnlySpan<char> inputSpan)
		{
			// must use List because of the following error:
			// Error CS4013 Instance of type 'ReadOnlySpan<char>' cannot be used inside a nested function, query expression, iterator block or async method
			// so yield return doesn't work here
			var integers = new List<int>();
			int wordStart = 0;
			int wordLength = 0;

			for (int i = 0; i <= inputSpan.Length;)
			{
				if (i == inputSpan.Length || this.separator.Contains(inputSpan[i]))
				{
					integers.Add(int.Parse(inputSpan.Slice(wordStart, wordLength)));

					wordStart = i + this.separator.Length;
					wordLength = 0;
					i += this.separator.Length;
				}
				else
				{
					i++;
					wordLength++;
				}
			}

			return integers;
		}

		// TASK 2
		public async Task<IEnumerable<int>> ParseAsync(string line)
		{
			await Task.Delay(1);
			return line.Split(this.separator).Select(s => int.Parse(s));
		}

		// TASK 2
		public async Task<IEnumerable<int>> ParseMemoryAsync(ReadOnlyMemory<char> inputMemory)
		{
			await Task.Delay(1);
			return this.ParseSpan(inputMemory.Span);
		}
	}
}