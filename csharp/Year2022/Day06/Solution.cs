namespace Csharp.Year2022.Day06;

public class Solution
{
	public static string Part1(string[] input)
	{
		return IndexOfFirstUniqueSequence(input[0], 4);
	}

	public static string Part2(string[] input)
	{
		return IndexOfFirstUniqueSequence(input[0], 14);
	}

	private static string IndexOfFirstUniqueSequence(string line, int length)
	{
		for (var i = 0; i < line.Length; i++)
		{
			var sequence = line.Substring(i, length);
			
			//no repeating characters found in sequence 
			if (sequence.Distinct().Count() == length)
			{
				return (i + length).ToString();
			}
		}
		return "NO MARKER FOUND";
	}
}