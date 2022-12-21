using Microsoft.VisualBasic;

namespace Csharp.Year2022.Day21;

public class Solution
{
	public static string Part1(string[] input)
	{
		Dictionary<string, Monkey> monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.name, x => x);

		var res = monkeys["root"].Evaluate(monkeys);
		return res.ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}
}

public class Monkey
{
	public string name;
	public string leftMonkey;
	public string rightMonkey;
	public string opearation;
	public bool HasNumber;
	public long number;

	public Monkey(string input)
	{
		name = input[..4];
		if (long.TryParse(input[6..], out number))
		{
			HasNumber = true;
		}
		else
		{
			var splitted = input[6..].Split(" ");
			leftMonkey = splitted[0];
			opearation = splitted[1];
			rightMonkey = splitted[2];
		}
	}

	public long Evaluate(Dictionary<string, Monkey> monkeys)
	{
		if (HasNumber)
		{
			return number;
		}

		var leftNumber = monkeys[leftMonkey].Evaluate(monkeys);
		var rightNumber = monkeys[rightMonkey].Evaluate(monkeys);

		return opearation switch
		{
			"+" => leftNumber + rightNumber,
			"-" => leftNumber - rightNumber,
			"*" => leftNumber * rightNumber,
			"/" => leftNumber / rightNumber
		};
	}
}