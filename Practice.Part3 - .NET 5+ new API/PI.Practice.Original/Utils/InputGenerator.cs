using System;
using System.Collections.Generic;
using System.Linq;

namespace PI.Practice.Original.Utils
{
	public static class InputGenerator
	{
		public static IEnumerable<string> GenerateLinesOfNumbers(int numbersInRow, int rows, string separator, int minValue = 0, int maxValue = 1000, int duplicatesPercentage = 0)
		{
			int uniqueCount = Convert.ToInt32(rows - rows * (double)duplicatesPercentage / 100);
			var unique = new HashSet<string>();

			int duplicatesCount = rows - uniqueCount;
			var rnd = new Random();

			while (true)
			{
				var line = string.Join(separator, Enumerable.Range(0, numbersInRow).Select(i => rnd.Next(minValue, maxValue)));
				unique.Add(line);
				if (unique.Count < uniqueCount)
				{
					continue;
				}

				int maxUniqueIdx = unique.Count - 1;
				var duplicates = Enumerable.Range(0, duplicatesCount).Select(i => unique.ElementAt(rnd.Next(0, maxUniqueIdx)));

				return unique.Concat(duplicates).OrderBy(i => rnd.Next());
			}
		}
	}
}
