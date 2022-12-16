using System.Numerics;
using System.Threading.Channels;
using System.Threading.Tasks.Sources;

namespace Csharp.Year2022.Day14;

public class Solution
{
	public static string Part1(string[] input)
	{
		var simulation = new SandSimulation();
		simulation.AddStructures(input);

		simulation.print();

		var ans = simulation.simulateSand();

		return ans.ToString();
	}

	public static string Part2(string[] input)
	{
		var simulation = new SandSimulation();
		simulation.AddStructures(input);
		simulation.addFloor();

		// simulation.print();

		var ans = simulation.simulateSand2();

		return ans.ToString();
	}
}

public enum Material
{
	Sand,
	Rock
}

public class SandSimulation
{
	private Dictionary<Vector2, Material> grid = new Dictionary<Vector2, Material>();


	public void AddStructures(string[] structures)
	{
		foreach (var structure in structures)
		{
			AddStructure(structure);
		}
	}

	public void AddStructure(string structure)
	{
		var corners = structure.Split(" -> ").Select(values => values.Split(","))
			.Select(x => new Vector2(int.Parse(x[0]), int.Parse(x[1]))).ToList();

		var previousCorner = corners.First();
		grid.TryAdd(previousCorner, Material.Rock);

		foreach (var corner in corners.Skip(1))
		{
			var stepVector = getStepVector(previousCorner, corner);

			var tmp = corner;
			while (tmp != previousCorner)
			{
				grid.TryAdd(tmp, Material.Rock);
				tmp += stepVector;
			}

			previousCorner = corner;
		}
	}

	private static Vector2 getStepVector(Vector2 start, Vector2 target)
	{
		var diff = start - target;


		if (diff.X == 0 && diff.Y == 0)
		{
			throw new Exception("Somethings wronf");
		}

		if (diff.X != 0)
		{
			return diff.X > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
		}

		if (diff.Y != 0)
		{
			return diff.Y > 0 ? new Vector2(0, 1) : new Vector2(0, -1);
		}

		throw new Exception("oh no");
	}


	public void print()
	{
		var ((maxX, minX), (maxY, minY)) = getDimensions();

		string s = "";

		// Console.WriteLine(getDimensions());
		for (int y = minY; y <= maxY; y++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				if (grid.ContainsKey(new Vector2(x, y)))
				{
					if (grid[new Vector2(x, y)] == Material.Rock)
					{
						s += "X"; //	Console.Write("X");
					}
					else
					{
						s += "O"; //Console.Write("O");
					}
				}
				else
				{
					s += " "; //Console.Write(" ");
				}
			}

			s += "\n"; //Console.Write("\n")
		}

		Thread.Sleep(10);
		Console.Clear();
		Console.Write(s);
	}

	private ((int max, int min) x, (int max, int min) y) getDimensions()
	{
		var maxX = (int) grid.Max(item => item.Key.X);
		var minX = (int) grid.Min(item => item.Key.X);
		var maxY = (int) grid.Max(item => item.Key.Y);
		var minY = (int) grid.Min(item => item.Key.Y);
		return ((maxX, minX), (maxY, minY));
	}

	public int simulateSand()
	{
		var start = new Vector2(500, 0);
		var maxy = getDimensions().y.max;
		int c = 0;
		while (simulateSandDrop(start, maxy) != null)
		{
			// print();
			c++;
		}

		return c;
	}

	public int simulateSand2()
	{
		var start = new Vector2(500, 0);
		var maxy = getDimensions().y.max;
		int c = 0;
		do
		{
			simulateSandDrop(start, maxy);
			// print();
			c++;
		} while (!grid.ContainsKey(start));


		return c;
	}


	public void addFloor()
	{
		var bounds = getDimensions();

		AddStructure($"-1000,{bounds.y.max + 2} -> 1000,{bounds.y.max + 2}");
	}

	public Vector2? simulateSandDrop(Vector2 startCord, int yStopLevel)
	{
		Vector2 down = new Vector2(0, 1);
		Vector2 left = new Vector2(-1, 1);
		Vector2 right = new Vector2(1, 1);

		var currentCord = startCord;
		var prev = currentCord;
		while (currentCord.Y < yStopLevel)
		{
			//straigh down
			if (canMove(currentCord + down))
			{
				prev = currentCord;
				currentCord += down;
				continue;
			}

			if (canMove(currentCord + left))
			{
				prev = currentCord;
				currentCord += left;
				continue;
			}

			if (canMove(currentCord + right))
			{
				prev = currentCord;
				currentCord += right;
				continue;
			}

			grid.Add(currentCord, Material.Sand);
			// no way to move
			return prev;
		}

		return null;
	}

	private bool canMove(Vector2 newCord)
	{
		return !grid.ContainsKey(newCord);
	}
}