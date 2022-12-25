using System.Text.Json.Serialization.Metadata;
using Microsoft.VisualBasic;

namespace Csharp.Year2022.Day21;

public class Solution
{
	public static string Part1(string[] input)
	{
		Monkey.Monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.Name, x => x);

		return Monkey.Monkeys["root"].Evaluate().ToString();
	}

	public static string Part2(string[] input)
	{
		Monkey.Monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.Name, x => x);

		var root = Monkey.Monkeys["root"];
		root.Evaluate();
		root.CalculateHumanBranch();

		var res = root.RightMonkey.IsHumanBranch
			? root.RightMonkey.CalculateHuman(root.LeftMonkey.Number)
			: root.LeftMonkey.CalculateHuman(root.RightMonkey.Number);

		return res.ToString();
	}
}

public class Monkey
{
	private const string HumanName = "humn";

	public static Dictionary<string, Monkey> Monkeys;


	private string leftMonkeyName;
	private string rightMonkeyName;
	public Monkey LeftMonkey => Monkeys[leftMonkeyName];
	public Monkey RightMonkey => Monkeys[rightMonkeyName];


	private readonly string opearation;
	private readonly bool hasNumber;
	public readonly string Name;

	public long Number;
	public bool IsHumanBranch;

	public Monkey(string input)
	{
		Name = input[..4];

		var expression = input[6..];
		if (long.TryParse(expression, out Number))
		{
			hasNumber = true;
		}
		else
		{
			var split = expression.Split(" ");
			leftMonkeyName = split[0];
			opearation = split[1];
			rightMonkeyName = split[2];
		}
	}


	public long Evaluate()
	{
		if (hasNumber)
		{
			return Number;
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

		Number = res;
		return res;
	}

	public void CalculateHumanBranch()
	{
		if (Name == HumanName)
		{
			IsHumanBranch = true;
		}

		if (hasNumber) return;

		LeftMonkey.CalculateHumanBranch();
		RightMonkey.CalculateHumanBranch();
		//if one of children is human branch, parent also is
		if (LeftMonkey.IsHumanBranch || RightMonkey.IsHumanBranch)
		{
			IsHumanBranch = true;
		}
	}

	public long CalculateHuman(long required)
	{
		//return number human should yell

		if (Name == HumanName)
		{
			return required;
		}

		return LeftMonkey.IsHumanBranch
			? LeftMonkey.CalculateHuman(GetRequired(RightMonkey, required, this, false))
			: RightMonkey.CalculateHuman(GetRequired(LeftMonkey, required, this, true));
	}

	private static long GetRequired(Monkey given, long target, Monkey parent, bool leftGiven)
	{
		var operation = parent.opearation;
		var number = given.Number;


		Console.WriteLine(leftGiven ? $"{number} {operation} X = {target}" : $"X {operation} {number} = {target}");

		return operation switch
		{
			"+" => target - number,
			"*" => target / number,
			"/" when leftGiven => number / target,
			"/" => number * target,
			"-" when leftGiven => number - target,
			"-" => number + target,
		};
	}

	public override string ToString()
	{
		if (Name == HumanName)
		{
			return "HUMAN";
		}

		if (hasNumber)
		{
			return $" {Number} ";
		}

		if (Name == "root")
		{
			return $"{LeftMonkey}	=	{RightMonkey}";
		}

		return $"({LeftMonkey} {opearation} {RightMonkey})";
	}
}