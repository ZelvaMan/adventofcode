using System.Text.Json.Serialization.Metadata;
using Microsoft.VisualBasic;

namespace Csharp.Year2022.Day21;

public class Solution
{
	public static string Part1(string[] input)
	{
		Dictionary<string, Monkey> monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.name, x => x);
		Monkey.Monkeys = monkeys;

		var res = monkeys["root"].Evaluate();
		return res.ToString();
	}

	public static string Part2(string[] input)
	{
		Dictionary<string, Monkey> monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.name, x => x);
		Monkey.Monkeys = monkeys;

		var playerMonkey = monkeys["humn"];
		var root = monkeys["root"];
		monkeys["root"].Evaluate();
		root.CalculateHumanBranch();
		long res;

		if (root.RightMonkey.isHumanBranch)
		{
			res = root.RightMonkey.CalculateHuman(root.LeftMonkey.number);
		}
		else
		{
			res = root.LeftMonkey.CalculateHuman(root.RightMonkey.number);
		}

		Console.WriteLine(res);
		return res.ToString();
	}
}

public class Monkey
{
	public static Dictionary<string, Monkey> Monkeys;
	public string name;

	private string leftMonkeyName;
	private string rightMonkeyName;

	public Monkey LeftMonkey => Monkeys[leftMonkeyName];

	public Monkey RightMonkey => Monkeys[rightMonkeyName];
	public string opearation;
	public bool HasNumber;
	public long number;
	public bool isHumanBranch;

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
			leftMonkeyName = splitted[0];
			opearation = splitted[1];
			rightMonkeyName = splitted[2];
		}
	}


	public long Evaluate()
	{
		if (HasNumber)
		{
			return number;
		}

		var leftNumber = LeftMonkey.Evaluate();
		var rightNumber = RightMonkey.Evaluate();

		var res = opearation switch
		{
			"+" => leftNumber + rightNumber,
			"-" => leftNumber - rightNumber,
			"*" => leftNumber * rightNumber,
			"/" => leftNumber / rightNumber
		};

		number = res;
		return res;
	}

	public void CalculateHumanBranch()
	{
		if (name == "humn")
		{
			isHumanBranch = true;
		}

		if (HasNumber) return;

		LeftMonkey.CalculateHumanBranch();
		RightMonkey.CalculateHumanBranch();
		//if one of children is human branch, parent also is
		if (LeftMonkey.isHumanBranch || RightMonkey.isHumanBranch)
		{
			isHumanBranch = true;
		}
	}

	public long CalculateHuman(long required)
	{
		//return number human should yell

		if (name == "humn")
		{
			return required;
		}

		return LeftMonkey.isHumanBranch
			? LeftMonkey.CalculateHuman(GetRequired(RightMonkey, required, this, false))
			: RightMonkey.CalculateHuman(GetRequired(RightMonkey, required, this, true));
	}

	private static long GetRequired(Monkey known, long required, Monkey parent, bool firstKnown)
	{
		var operation = parent.opearation;
		var number = known.number;


		if (firstKnown)
		{
			Console.WriteLine($"{number} {operation} X = {required}");
		}
		else
		{
			Console.WriteLine($"X {operation} {number} = {required}");
		}

		if (operation == "+")
		{
			return required - number;
		}

		if (operation == "*")
		{
			return required / number;
		}

		if (operation == "/")
		{
			if (firstKnown)
			{
				return number / required;
			}

			return number * required;
		}

		if (operation == "-")
		{
			if (firstKnown)
			{
				return number - required;
			}

			return number + required;
		}

		throw new Exception("oh no");
	}

	public override string ToString()
	{
		if (name == "humn")
		{
			return "HUMAN";
		}

		if (HasNumber)
		{
			return $" {number} ";
		}

		var left = LeftMonkey;
		var right = RightMonkey;


		if (name == "root")
		{
			return $"{left}	=	{right}";
		}

		return $"({left} {opearation} {right})";
	}
}