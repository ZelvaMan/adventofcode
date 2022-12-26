using System.Numerics;

namespace Csharp.Year2022.Day18;

public class Solution
{
	public static string Part1(string[] input)
	{
		var vectors = input.Select(x => parseVector(x)).ToList();

		var volcano = new Volcano(vectors);
		var area = volcano.CalculateArea();
		
		return area.ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}

	private static Vector3 parseVector(string line)
	{
		var split = line.Split(",").Select(int.Parse).ToArray();
		return new Vector3(split[0], split[1], split[2]);
	}
}

public class Volcano
{
	public HashSet<Vector3> Droplets;

	public Volcano(IEnumerable<Vector3> droplets)
	{
		Droplets = droplets.ToHashSet();
	}


	private static Vector3[] sidesToCheck = new[]
	{
		new Vector3(1, 0, 0),
		new Vector3(-1, 0, 0),
		new Vector3(0, 1, 0),
		new Vector3(0, -1, 0),
		new Vector3(0, 0, 1),
		new Vector3(0, 0, -1),
	};

	private int GetCubeSurface(Vector3 cube)
	{
		//Check if droplets contain any of 6 
		int area = 6;
		foreach (var dir in sidesToCheck)
		{
			if (Droplets.Contains(cube + dir))
			{
				area--;
			}
		}

		return area;
	}

	public long CalculateArea()
	{
		return Droplets.Select(x => GetCubeSurface(x)).Sum();
	}
}