using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace Csharp.Year2022.Day05;

public class Solution
{
	public static string Part1(string[] input)
	{
		var craneInputs = input.TakeWhile(x => x != "");

		var crain = new CargeCrain(craneInputs);

		foreach (var command in input.SkipWhile(x => !string.IsNullOrEmpty(x)).Skip(1))
		{
			crain.MoveOneByOne(command);
		}

		return crain.GetTopCrates();
	}

	public static string Part2(string[] input)
	{
		var craneInputs = input.TakeWhile(x => x != "");

		var crain = new CargeCrain(craneInputs);

		foreach (var command in input.SkipWhile(x => !string.IsNullOrEmpty(x)).Skip(1))
		{
			crain.MoveMultiple(command);
		}

		return crain.GetTopCrates();
	}
}

public class CargeCrain
{
	public int StacksCount { get; private set; }
	public List<List<char>> Stacks { get; set; }

	public CargeCrain(IEnumerable<string> inputs)
	{
		UpdateNumberOfStacks(inputs.First());
		Stacks = new List<List<char>>();
		Stacks.AddRange(Enumerable.Range(0, StacksCount).Select(x => new List<char>()));

		//go from bottom up
		foreach (var line in inputs.Reverse().Skip(1))
		{
			AddToStack(line);
		}
	}

	private void UpdateNumberOfStacks(string firstLine)
	{
		StacksCount = (firstLine.Length + 1) / 4;
	}

	private void AddToStack(string line)
	{
		for (var i = 0; i < StacksCount; i++)
		{
			var stackCharacter = line[i * 4 + 1];
			if (stackCharacter != ' ')
			{
				Stacks[i].Add(stackCharacter);
			}
		}
	}

	public void MoveOneByOne(string command)
	{
		var splitCommand = command.Split(" ");
		var count = int.Parse(splitCommand[1]);
		var fromIndex = int.Parse(splitCommand[3]) - 1;
		var toIndex = int.Parse(splitCommand[5]) - 1;

		for (var i = 0; i < count; i++)
		{
			DoMove(fromIndex, toIndex);
		}
	}

	public void MoveMultiple(string command)
	{
		var splitCommand = command.Split(" ");
		var count = int.Parse(splitCommand[1]);
		var fromIndex = int.Parse(splitCommand[3]) - 1;
		var toIndex = int.Parse(splitCommand[5]) - 1;

		DoMoveMultiple(fromIndex, toIndex, count);
	}

	private void DoMoveMultiple(int from, int to, int amount)
	{
		var fromStartIndex = Stacks[from].Count - amount;

		//if they arent enough crates in stack, prevent negative index
		if (fromStartIndex < 0)
		{
			fromStartIndex = 0;
		}

		var movedStack = Stacks[from].GetRange(fromStartIndex, amount);

		// Console.WriteLine(
		// 	$"({Strings.Join(movedStack.Select(x => x.ToString()).ToArray(), " ")}) FROM {from + 1} TO {to + 1}");

		Stacks[from].RemoveRange(fromStartIndex, amount);
		Stacks[to].AddRange(movedStack);


		// PrintStacks();
	}

	private void DoMove(int from, int to)
	{
		var moved = Stacks[from].Last();
		
		// Console.WriteLine($"MOVED {moved} FROM {from + 1} TO {to + 1}");
		
		Stacks[from].RemoveAt(Stacks[from].Count - 1);
		Stacks[to].Add(moved);
	}

	public string GetTopCrates()
	{
		return Stacks.Aggregate("", (current, stack) => current + stack.Last());
	}

	public void PrintStacks()
	{
		Console.WriteLine("PRINTING STACK");
		foreach (var stack in Stacks)
		{
			var stackString = Strings.Join(stack.Select(x => x.ToString()).ToArray(), " ");

			Console.WriteLine($" {stackString}");
		}

		Console.WriteLine();
	}
}