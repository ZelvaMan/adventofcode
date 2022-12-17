using System.Numerics;
using System.Text.RegularExpressions;

namespace Csharp.Year2022.Day15;

public class Solution
{
	public static string Part1(string[] input)
	{
		const long YLEVEL = 2_000_000;


		var sensors = input.Select(x => new Sensor(x)).ToList();


		var biggestArea = sensors.Max(x => x.ManhattanDistance);
		var leftest = sensors.Min(x => x.Position.Y);
		var mostRight = sensors.Max(x => x.Position.Y);


		var startx = (long) (leftest - (biggestArea));
		var endX = (long) mostRight + (biggestArea);
		Console.WriteLine($"{startx} -> {endX}");
		BigInteger c = 0;
		for (long x = startx; x < endX; x++)
		{
			var point = new Vector2(x, YLEVEL);

			//you cant place beacon if there is already one
			if (sensors.Any(sensor => sensor.IsTooClose(point) && sensor.Beacon != point))
			{
				c += 1;
			}
			else
			{
				// Console.WriteLine(x);
			}
		}

		return c.ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}
}

public class Sensor
{
	public long ManhattanDistance { get; private set; }
	public Vector2 Position { get; private set; }
	public Vector2 Beacon { get; private set; }

	public Sensor(string input)
	{
		var vectors = ParseCords(input);
		Position = vectors.sensor;
		ManhattanDistance = Sensor.CalcDistance(vectors.sensor, vectors.beacon);
		Beacon = vectors.beacon;
	}

	private static (Vector2 sensor, Vector2 beacon) ParseCords(string line)
	{
		//fuck negative numbers
		const string regexPatern = @"([xy])(=)(-?\d+)";


		var matches = Regex.Matches(line, regexPatern, RegexOptions.Multiline)
			.Select(x => x.Groups[3].Captures.First().Value).Select(long.Parse).ToList();
		return (new Vector2(matches[0], matches[1]), new Vector2(matches[2], matches[3]));
	}

	public bool IsTooClose(Vector2 pos)
	{
		var distance = CalcDistance(pos, Position);
		return (distance <= ManhattanDistance);
	}

	public static long CalcDistance(Vector2 p1, Vector2 p2)
	{
		return (long) (Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y));
	}
}