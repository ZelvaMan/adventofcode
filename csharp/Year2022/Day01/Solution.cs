namespace Csharp.Year2022.Day01;

public class Solution
{
	public static string Part1(string[] input)
	{
		return GetSum(input, 1).ToString();
	}

	public static string Part2(string[] input)
	{
		return GetSum(input, 3).ToString();
	}

	private static int GetSum(string[] input, int topElvesCount)
	{
		var elves = new List<int>();
		var sum = 0;

		foreach (var line in input)
			if (string.IsNullOrEmpty(line))
			{
				elves.Add(sum);
				sum = 0;
			}
			else
			{
				sum += int.Parse(line);
			}

		var ordered = elves.OrderByDescending(x => x);

		return ordered.Take(topElvesCount).Sum();
	}
}