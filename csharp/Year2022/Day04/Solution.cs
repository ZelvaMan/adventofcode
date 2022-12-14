using System.Security.Cryptography;

namespace Csharp.Year2022.Day04;

public class Solution
{
	public static string Part1(string[] input)
	{
		var parsed = input.Select(Range.ParseTwo).ToList();
		
		var fullyContainerCount = parsed.Where(Range.FullyOverlap).Count();

		return fullyContainerCount.ToString();
	}

	public static string Part2(string[] input)
	{
		var parsed = input.Select(Range.ParseTwo).ToList();

		var overlap = parsed.Where(Range.Overlap).Count();

		return overlap.ToString();
	}
}

public class Range
{
	private readonly long start;
	private readonly long end;

	public Range(string line)
	{
		var split = line.Split("-");
		start = int.Parse(split[0]);
		end = int.Parse(split[1]);
	}

	public static (Range, Range) ParseTwo(string line)
	{
		var split = line.Split(",");

		return (
			new Range(split[0]),
			new Range(split[1])
		);
	}

	public static bool FullyOverlap((Range r1, Range r2) rangesPair)
	{
		var (r1, r2) = rangesPair;
		
		return r1.FullyContains(r2) || r2.FullyContains(r1);
	}

	public static bool Overlap((Range r1, Range r2) rangesPair)
	{
		var (r1, r2) = rangesPair;
		
		return r1.end >= r2.start && r1.start <= r2.end;
	}

	public bool FullyContains(Range r)
	{
		return (start <= r.start && end >= r.end);
	}
}