using System.Numerics;

namespace Csharp.Year2022.Day12;

public class Solution
{
	public static string Part1(string[] input)
	{
		var map = new HeighMap(input);

		var start = map.FindPoints('S').First();
		var end = map.FindPoints('E').First();

		var distance = map.FindShortestPath(start, end, Int32.MaxValue);
		return distance.ToString();
	}

	public static string Part2(string[] input)
	{
		var map = new HeighMap(input);

		var starts = map.FindPoints('a').ToList();
		var end = map.FindPoints('E').First();

		var bestDistance = int.MaxValue;

		Console.WriteLine($"starts = {starts.Count}");
		foreach (var start in starts)
		{
			var distance = map.FindShortestPath(start, end,bestDistance);
			Console.WriteLine($"distance: {distance}");
			if (distance != -1 && distance < bestDistance)
			{
				Console.WriteLine($"Update best distance to {distance}");
				bestDistance = distance;
			}
		}

		return bestDistance.ToString();
	}
}

public class HeighMap
{
	private char[][] map;
	private int widht;
	private int heigh;

	public HeighMap(string[] input)
	{
		map = input.Select(x => x.ToCharArray()).ToArray();
		widht = input[0].Length;
		heigh = input.Length;
	}

	private Queue<(Vector2 point, int distance)> queue;
	private HashSet<Vector2> visited;

	public int FindShortestPath(Vector2 start, Vector2 end, int returnDistance)
	{
		visited = new HashSet<Vector2>();
		queue = new Queue<(Vector2 point, int distance)>();

		queue.Enqueue((start, 0));
		visited.Add(start);

		while (queue.Any())
		{
			var current = queue.Dequeue();
			// Console.WriteLine(current);
			//check if we found end
			if (current.point == end)
			{
				return current.distance;
			}

			if (!(current.distance + 1 < returnDistance))
			{
				continue;
			}

			TryAddToQueue(getVal(current.point), current.point + new Vector2(0, 1), current.distance + 1);
			TryAddToQueue(getVal(current.point), current.point + new Vector2(0, -1), current.distance + 1);
			TryAddToQueue(getVal(current.point), current.point + new Vector2(1, 0), current.distance + 1);
			TryAddToQueue(getVal(current.point), current.point + new Vector2(-1, 0), current.distance + 1);
		}

		return -1;
	}

	public void TryAddToQueue(char oldVal, Vector2 newVector, int newDistance)
	{
		if (visited.Contains(newVector))
		{
			return;
		}

		if (newVector.X < 0 || newVector.X >= widht || newVector.Y < 0 || newVector.Y >= heigh)
		{
			return;
		}

		var newValue = getVal(newVector);

		// Console.WriteLine($"{oldVal} => {newValue}");

		if ((newValue - 1) == oldVal || newValue <= oldVal)
		{
			// Console.WriteLine($"adding point {newVector}");
			queue.Enqueue((newVector, newDistance));
			visited.Add(newVector);
		}
	}

	private char getVal(Vector2 point)
	{
		if (point.X < 0 || point.X >= widht || point.Y < 0 || point.Y >= heigh)
		{
			return (char) 255;
		}

		var value = map[(int) point.Y][(int) point.X];

		return value switch
		{
			'E' => 'z',
			'S' => 'a',
			_ => value
		};
	}

	public List<Vector2> FindPoints(char ch)
	{
		var found = new List<Vector2>();
		for (int x = 0; x < widht; x++)
		{
			for (int y = 0; y < heigh; y++)
			{
				if (map[y][x] == ch)
					found.Add(new Vector2(x, y));
			}
		}

		return found;
	}
}