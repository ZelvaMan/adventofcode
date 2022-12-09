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
		var forest = input.Select(x => x.Select(x => int.Parse(x.ToString())).ToArray()).ToArray();

		var heighMap = new HeighMap(forest);

		var maxScenicScore = heighMap.GetHighestScenicScore();
		return maxScenicScore.ToString();
	}
}

public class HeighMap
{
	private int[][] heightMap;
	private int width;

	public HeighMap(int[][] heightMap)
	{
		this.heightMap = heightMap;
		width = heightMap.Length;
	}

	public HashSet<(int, int)> GetVisibleTrees()
	{
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


	public long GetHighestScenicScore()
	{
		long maxScore = 0;
		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < width; x++)
			{
				var cur = c(x, y);
				long newScore = calculateScenicScore(x, y);
				// Console.WriteLine($" [{x};{y}] = {newScore}");

				if (newScore > maxScore)
				{
					maxScore = newScore;
				}
			}
		}

		return maxScore;
	}

	public long calculateScenicScore(int x, int y)
	{
		int score = 1;
		int originalTree = heightMap[y][x];

		int tmpx = x;
		// x --
		while (true)
		{
			tmpx--;
			//we found bigger tree
			if (c(tmpx, y) >= originalTree)
			{
				int distance = Math.Abs(x - (tmpx));
				
				if (c(tmpx, y) == 999999)
				{
					distance--;
				}
				
				score *= distance;
				break;
			}
		}


		tmpx = x;
		while (true)
		{
			tmpx++;
			//we found bigger tree
			if (c(tmpx, y) >= originalTree)
			{
				int distance = Math.Abs(x - (tmpx));
				if (c(tmpx, y) == 999999)
				{
					distance--;
				}
				

				score *= distance;
				break;
			}
		}

		int tmpy = y;
		while (true)
		{
			tmpy--;
			//we found bigger tree
			if (c(x, tmpy) >= originalTree)
			{
				int distance = Math.Abs(y - (tmpy));
				
				if (c(x, tmpy) == 999999)
				{
					distance--;
				}
				

				score *= distance;
				break;
			}
		}

		tmpy = y;
		while (true)
		{
			tmpy++;
			//we found bigger tree
			if (c(x, tmpy) >= originalTree)
			{
				int distance = Math.Abs(y - (tmpy));
				if (c(x, tmpy) == 999999)
				{
					distance--;
				}
				

				score *= distance;
				break;
			}
		}

		return score;
	}

	private int c(int x, int y)
	{
		if (x < 0 || x >= width || y < 0 || y >= width)
		{
			return 999999;
		}

		return heightMap[y][x];
	}

	public static void printVisible(HashSet<(int, int)> visible, int a)
	{
		for (int y = 0; y < a; y++)
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