using System.Security.Cryptography;

namespace Csharp.Year2022.Day04;

public class Solution
{
	public static string Part1(string[] input)
	{
		var parsed = input.Select(Range.ParseTwo).ToList();

		int fullyContainerCount = parsed.Where(Range.FullyOverlap).Count();
		return fullyContainerCount.ToString();
	}

	public static string Part2(string[] input)
	{
		var parsed = input.Select(Range.ParseTwo).ToList();

		int overlap = parsed.Where(Range.Overlap).Count();
		return overlap.ToString();
	}
}

public class Range
{
	public long start, end;

	public Range(string line)
	{
		var split = line.Split("-");
		start = int.Parse(split[0]);
		end = int.Parse(split[1]);
	}

	public static (Range, Range) ParseTwo(string line)
	{
		var split = line.Split(",");

		return (new Range(split[0]), new Range(split[1]));
	}

	public static bool FullyOverlap((Range r1, Range r2) rangesPair)
	{
		var (r1, r2) = rangesPair;

		if (r1.IsSubset(r2))
		{
			return true;
		}

		if (r2.IsSubset(r1))
		{
			return true;
		}

		return false;
	}

	public static bool Overlap((Range r1, Range r2) rangesPair)
	{
		var (r1, r2) = rangesPair;

		if (r1.end < r2.start)
		{
			return false;
		}

		if (r1.start > r2.end)
		{
			return false;
		}

		return true;
	}

	public bool IsSubset(Range r)
	{
		return (start <= r.start && end >= r.end);
	}
}