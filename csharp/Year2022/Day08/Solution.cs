namespace Csharp.Year2022.Day08;

public class Solution
{
	public static string Part1(string[] input)
	{
		var forest = input.Select(x => x.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();

		var heighMap = new HeighMap(forest);

		var visible = heighMap.GetVisibleTrees();
		
		// HeighMap.printVisible(visible,input[0].Length);
		return visible.Count.ToString();
	}

	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}
}

public class HeighMap
{
	private int[][] heightMap;

	public HeighMap(int[][] heightMap)
	{
		this.heightMap = heightMap;
	}

	public HashSet<(int, int)> GetVisibleTrees()
	{
		var width = heightMap.Length;

		var visible = new HashSet<(int, int)>();

		for (var y = 0; y < width; y++)
		{
			int maxHeight = -1;
			for (int x = 0; x < width; x++)
			{
				var tree = heightMap[y][x];
				//we cant see this tree
				if (tree <= maxHeight)
				{
					continue;
				}

				maxHeight = tree;
				visible.Add((x, y));
			}

			maxHeight = -1;

			for (var x = width - 1; x >= 0; x--)
			{
				var tree = heightMap[y][x];
				//we cant see this tree
				if (tree <= maxHeight)
				{
					continue;
				}

				maxHeight = tree;
				visible.Add((x, y));
			}
		}

		for (var x = 0; x < width; x++)
		{
			int maxHeight = -1;
			for (int y = 0; y < width; y++)
			{
				var tree = heightMap[y][x];


				//we cant see this tree
				if (tree <= maxHeight)
				{
					continue;
				}

				maxHeight = tree;
				visible.Add((x, y));
			}

			maxHeight = -1;

			// Console.WriteLine("BOTTOM TO TOP");

			for (var y = width - 1; y >= 0; y--)
			{
				var tree = heightMap[y][x];

				//we cant see this tree
				if (tree <= maxHeight)
				{
					continue;
				}

				maxHeight = tree;
				visible.Add((x, y));
			}
		}

		return visible;
	}
	

	public static void printVisible(HashSet<(int, int)> visible, int a)
	{
		for (int y = 0; y < a;y++)
		{
			for (int x = 0; x < a; x++)
			{
				if (visible.Contains((x, y)))
				{
					Console.Write("X");
				}
				else
				{
					Console.Write(" ");
				}
			}

			Console.Write('\n');
		}
	}
}