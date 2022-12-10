using System.Numerics;

namespace Csharp.Year2022.Day09;

public class Solution
{
	public static string Part1(string[] input)
	{
		var movements = RiverSimulator.ParseInstructions(input);

		var simulation = new RiverSimulator(2);

		foreach (var motion in movements)
		{
			simulation.SimulateMotion(motion);
		}

		return simulation.visitedByTail.Count.ToString();
	}

	public static string Part2(string[] input)
	{
		var movements = RiverSimulator.ParseInstructions(input);

		var simulation = new RiverSimulator(10);

		foreach (var motion in movements)
		{
			simulation.SimulateMotion(motion);
		}

		return simulation.visitedByTail.Count.ToString();
	}
}

public class RiverSimulator
{
	private Vector2[] Rope;
	private int length;
	public HashSet<Vector2> visitedByTail { get; private set; }

	public RiverSimulator(int ropeLength)
	{
		length = ropeLength;
		//init Rope
		Rope = new Vector2[ropeLength];
		for (int i = 0; i < length; i++)
		{
			Rope[i] = Vector2.One;
		}

		visitedByTail = new HashSet<Vector2>() {Vector2.One};
	}

	public void SimulateMotion((Vector2 direction, int count) motion)
	{
		// Console.WriteLine($"SIMULATE MOTION: {motion}");
		for (int i = 0; i < motion.count; i++)
		{
			SimulateMove(motion.direction);
		}
	}


	public void SimulateMove(Vector2 direction)
	{
		//move head
		Rope[0] += direction;

		for (var i = 1; i < Rope.Length; i++)
		{
			Rope[i] = MoveSegment(Rope[i - 1], Rope[i]);
		}

		visitedByTail.Add(Rope.Last());
	}


	private Vector2 MoveSegment(Vector2 head, Vector2 tail)
	{
		if (IsTailTouching(head, tail))
		{
			return tail;
		}

		var differenceVector = head - tail;

		var tailMove = ChangeToOnes(differenceVector);

		return tail + tailMove;
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

	private bool IsTailTouching(Vector2 head, Vector2 tail)
	{
		//their distance is less than sqrt 2
		return Vector2.Distance(head, tail) < 2;
	}

	//PARSING
	public static List<(Vector2, int)> ParseInstructions(IEnumerable<string> instructions)
	{
		return instructions.Select(x => (ParseVec(x[0]), int.Parse(x[2..]))).ToList();
	}

	private static Vector2 ParseVec(char v)
	{
		return v switch
		{
			'R' => new Vector2(1, 0),
			'U' => new Vector2(0, 1),
			'L' => new Vector2(-1, 0),
			'D' => new Vector2(0, -1),
			_ => Vector2.Zero
		};
	}
}