using System.Diagnostics.CodeAnalysis;

namespace Csharp.Year2022.Day11;

public class Solution
{
	public static string Part1(string[] input)
	{
		var monkeys = ParseMonkeys(input);

		for (int i = 0; i < 20; i++)
		{
			DoRound(monkeys);
		}

		var ordered = monkeys.OrderByDescending(x => x.InspectionsCount).Take(2).ToArray();

		return (ordered[0].InspectionsCount * ordered[1].InspectionsCount).ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENT";
	}

	private static List<Monkey> ParseMonkeys(string[] input)
	{
		return input.Chunk(7).Select(x => new Monkey(x)).ToList();
	}

	private static void DoRound(List<Monkey> monkeys)
	{
		foreach (var monkey in monkeys)
		{
			monkey.InspectItems(monkeys);
		}
	}
}

public class Monkey
{
	public int InspectionsCount { get; private set; } = 0;

	private int dividant;
	private List<long> items;
	private Operation operation;
	private (int ifTrue, int ifFalse) throwTo;

	public Monkey(string[] monkeyInput)
	{
		items = monkeyInput[1][18..].Split(", ").Select(long.Parse).ToList();
		operation = new Operation(monkeyInput[2][12..].Trim());
		dividant = int.Parse(monkeyInput[3][21..].Trim());
		throwTo.ifTrue = int.Parse(monkeyInput[4][29..].Trim());
		throwTo.ifFalse = int.Parse(monkeyInput[5][30..].Trim());
	}

	public void InspectItems(List<Monkey> monkeys)
	{
		foreach (var item in items)
		{
			long value = operation.DoOperation(item);

			if (value % dividant == 0)
			{
				monkeys[throwTo.ifTrue].items.Add(value);
			}
			else
			{
				monkeys[throwTo.ifFalse].items.Add(value);
			}

			InspectionsCount++;
		}

		items.Clear();
	}
}

public class Operation
{
	private string operand, number;

	public long DoOperation(long old)
	{
		var newValue = (operand, number) switch
		{
			("*", "old") => (old * old),
			("+", "old") => (old + old),
			("*", _) => (old * int.Parse(number)),
			("+", _) => (old + int.Parse(number))
		};

		return newValue / 3;
	}


	public Operation(string input)
	{
		var rightPart = input.Split("=")[1].Trim().Split(" ");

		operand = rightPart[1];
		number = rightPart[2];
	}
}