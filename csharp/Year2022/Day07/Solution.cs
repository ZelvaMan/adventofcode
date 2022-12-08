namespace Csharp.Year2022.Day07;

public class Solution
{
	public static string Part1(string[] input)
	{
		var fs = new FileSystem(input);

		var dirs = fs.NestedDirectorySize();

		var dirsUndeMil = dirs.Where(x => x.Value < 100000);
		var sum = dirsUndeMil.Sum(x => x.Value);
		
		return sum.ToString();
	}

	public static string Part2(string[] input)
	{
		const long totalSize = 70000000, updateSize = 30000000;

		var fs = new FileSystem(input);
		
		var dirs = fs.NestedDirectorySize();

		var freeSpace = totalSize - dirs["/"];
		var spaceNeeded = updateSize - freeSpace;
		
		var lowestPossible = dirs.Where(x => x.Value > spaceNeeded).MinBy(x => x.Value);
		return lowestPossible.Value.ToString();
	}
}

public class FileSystem
{
	private Dictionary<string, long> Directories = new Dictionary<string, long>();
	private string cwd = "/";

	public FileSystem(IEnumerable<string> rawInputs)
	{
		var lastLs = false;

		var lsOutput = new List<string>();
		foreach (var line in rawInputs)
		{
			//it isnt command, so its part of ls output
			if (!line.StartsWith("$"))
			{
				lsOutput.Add(line);
				continue;
			}


			//if it is end of ls command add directory entry
			if (lastLs)
			{
				AddDirectory(cwd, lsOutput);
			}

			//do command
			switch (line[2..4])
			{
				case "cd":
					lastLs = false;
					ChangeCwd(line);
					break;

				case "ls":
					lastLs = true;
					lsOutput = new List<string>();
					break;
			}
		}

		//if last command was ls
		if (lastLs)
		{
			AddDirectory(cwd, lsOutput);
		}
	}

	public void ChangeCwd(string command)
	{
		var changeTo = command[5..];
		switch (changeTo)
		{
			case "..":
				cwd = cwd[..cwd.LastIndexOf("/")];
				if (!cwd.StartsWith("/"))
				{
					cwd = "/";
				}

				break;
			case "/":
				cwd = "/";
				break;

			default:

				if (cwd != "/")
				{
					cwd += "/";
				}

				cwd += changeTo;
				break;
		}
	}

	private void AddDirectory(string name, IEnumerable<string> lsOutput)
	{
		long directChildrenSize = 0;

		foreach (var entry in lsOutput)
		{
			//for now skip directories
			if (entry.StartsWith("dir"))
			{
				continue;
			}

			directChildrenSize += int.Parse(entry.Split(" ").First());
		}

		Directories.Add(name, directChildrenSize);
	}

	public Dictionary<string, long> NestedDirectorySize()
	{
		var sizes = Directories
			.OrderByDescending(x => x.Key.Length)
			.ToDictionary(x => x.Key, x => x.Value);


		//go from deepest directories and add its size to parent directory
		foreach (var (name, size) in sizes)
		{
			if (name == "/")
			{
				continue;
			}

			var newName = name[..name.LastIndexOf("/")];

			if (newName == "")
			{
				newName = "/";
			}

			// Console.WriteLine($"Add  {size} FROM {name} TO {newName}");

			sizes[newName] += size;
		}

		return sizes;
	}
}