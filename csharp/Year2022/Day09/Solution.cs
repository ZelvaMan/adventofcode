using System.Numerics;

namespace Csharp.Year2022.Day09;

public class Solution
{
	public static string Part1(string[] input)
	{
		var movements = RiverSimulator.parseInstructions(input);

		var simulation = new RiverSimulator();

		foreach (var motion in movements)
		{
			simulation.SimulateMotion(motion);
		}

		return simulation.visitedByTail.Count.ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}
}

public class RiverSimulator
{
	private Vector2 head;
	private Vector2 tail;
	public HashSet<Vector2> visitedByTail { get; private set; }

	public RiverSimulator()
	{
		head = Vector2.Zero;
		tail = Vector2.Zero;
		visitedByTail = new HashSet<Vector2>() {tail};
	}

	public void SimulateMotion((Vector2 direction, int count) motion)
	{
		Console.WriteLine($"SIMULATE MOTION: {motion}");
		for (int i = 0; i < motion.count; i++)
		{
			SimulateMove(motion.direction);
			PrintState();
		}
	}


	public void SimulateMove(Vector2 direction)
	{
		//move head
		head = head + direction;

		if (tailTouching())
		{
			return;
		}


		var diferenceVector = Vector2.Subtract(head, tail);

		var tailMove = ChangeToOnes(diferenceVector);

		tail = Vector2.Add(tail, tailMove);

		visitedByTail.Add(tail);
	}

	private Vector2 ChangeToOnes(Vector2 v)
	{
		if (v.X > 0)
		{
			v.X = 1;
		}

		if (v.X < 0)
		{
			v.X = -1;
		}

		if (v.Y > 0)
		{
			v.Y = 1;
		}

		if (v.Y < 0)
		{
			v.Y = -1;
		}

		return v;
	}

	private bool tailTouching()
	{
		// Console.WriteLine("Distance: " + Vector2.Distance(head, tail));
		//their distance is less than sqrt 2
		return Vector2.Distance(head, tail) < 2;
	}


	public void PrintState()
	{
		Console.WriteLine($"tail={tail.ToString()} head ={head.ToString()}");
	}

	public static List<(Vector2, int)> parseInstructions(string[] instructions)
	{
		return instructions.Select(x => (parseVec(x[0]), int.Parse(x[2..]))).ToList();
	}

	private static Vector2 parseVec(char v)
	{
		switch (v)
		{
			case 'R':
				return new Vector2(1, 0);
			case 'U':
				return new Vector2(0, 1);
			case 'L':
				return new Vector2(-1, 0);
			case 'D':
				return new Vector2(0, -1);
		}

		return Vector2.Zero;
	}
}