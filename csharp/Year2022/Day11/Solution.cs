using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Csharp.Year2022.Day11;

public class Solution
{
	public static string Part1(string[] input)
	{
		var monkeys = ParseMonkeys(input);

		Simulate(monkeys, 20, false);
		var ordered = monkeys.OrderByDescending(x => x.InspectionsCount).Take(2).ToArray();
		return (ordered[0].InspectionsCount * ordered[1].InspectionsCount).ToString();
	}

	public static string Part2(string[] input)
	{
		var monkeys = ParseMonkeys(input);

		Simulate(monkeys, 10000, true);
		var ordered = monkeys.OrderByDescending(x => x.InspectionsCount).Take(2).ToArray();
		var m1 = ordered[0].InspectionsCount;
		var m2 = ordered[1].InspectionsCount;

		BigInteger n = m1 * m2;
		return (n).ToString();
	}

	private static List<Monkey> ParseMonkeys(string[] input)
	{
		return input.Chunk(7).Select(x => new Monkey(x)).ToList();
	}

	private static void Simulate(List<Monkey> monkeys, int rounds, bool part2)
	{
		for (int i = 0; i < rounds; i++)
		{
			foreach (var monkey in monkeys)
			{
				monkey.InspectItems(monkeys, part2);
			}
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

	public void InspectItems(List<Monkey> monkeys, bool part2)
	{
		var dividor = MathHelper.Lcm(monkeys.Select(x => (long) x.dividant).ToArray());

		foreach (var item in items)
		{
			long value = operation.DoOperation(item);

			if (part2)
			{
				value %= dividor;
			}
			else
			{
				value /= 3;
			}

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
		return (operand, number) switch
		{
			("*", "old") => (old * old),
			("+", "old") => (old + old),
			("*", _) => (old * int.Parse(number)),
			("+", _) => (old + int.Parse(number))
		};
	}


	public Operation(string input)
	{
		var rightPart = input.Split("=")[1].Trim().Split(" ");

		operand = rightPart[1];
		number = rightPart[2];
	}
}

public static class MathHelper
{
	private static long Gcd(long a, long b)
	{
		while (true)
		{
			if (a == 0) return b;
			var a1 = a;
			a = b % a;
			b = a1;
		}
	}

	//recursive implementation
	private static long LcmOfArray(long[] arr, long idx)
	{
		// lcm(a,b) = (a*b/gcd(a,b))
		if (idx == arr.Length - 1)
		{
			return arr[idx];
		}

		var a = arr[idx];
		var b = LcmOfArray(arr, idx + 1);
		return (a * b / Gcd(a, b));
	}

	public static long Lcm(long[] array)
	{
		return LcmOfArray(array, 0);
	}
}