using System.Security.Cryptography.X509Certificates;

namespace Csharp.Year2022.Day02;

public class Solution
{
	public static string Part1(string[] input)
	{
		var s = ParseInput(input);
		var results = s.Select((pair => dogame(pair) + pair.us));
		var sum = results.Sum();
		return sum.ToString();
	}

	public static string Part2(string[] input)
	{
		var s = ParseInput(input);
		var results = s
			.Select(x => (x.they, matchGame(x)))
			.Select((pair => dogame(pair) + pair.Item2));
		var sum = results.Sum();
		return sum.ToString();

	}

	//ROCK = 1 = AX
	//PAPER = 2 = BY
	//SCISORS = 3 = CZ
	private static List<(int they, int us)> ParseInput(string[] input)
	{
		return input
			.Select(raw => raw.Split(" "))
			.Select(splitted =>
				(CharToNum(splitted[0]), CharToNum(splitted[1]))
			).ToList();
	}

	private static int dogame((int they, int us) pair)
	{
		if (pair.they == pair.us) return 3;

		switch (pair.they)
		{
			case 1 when pair.us == 2:
			case 2 when pair.us == 3:
			case 3 when pair.us == 1:
				return 6;
			default:
				return 0;
		}
	}


	private static int matchGame((int they, int result) pair)
	{
		//x => lose
		//y => same
		//z => win

		if (pair.result == 2)
		{
			return pair.they;
		}

		if (pair.result == 1)
		{
			return pair.they switch
			{
				1 => 3,
				2 => 1,
				3 => 2
			};
		}
		if (pair.result == 3)
		{
			return pair.they switch
			{
				1 => 2,
				2 => 3,
				3 => 1
			};

		}

		throw new Exception("oh nou");

	}

	private static int CharToNum(string s)
	{
		return s switch
		{
			"A" => 1,
			"X" => 1,
			"B" => 2,
			"Y" => 2,
			"C" => 3,
			"Z" => 3,
		};
	}
}