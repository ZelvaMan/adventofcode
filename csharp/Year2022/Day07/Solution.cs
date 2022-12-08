namespace Csharp.Year2022.Day07;

public class Solution
{
    public static string Part1(string[] input)
    {
        var fs = new FileSystem(input);
        Console.WriteLine("TEST");

        var dirs = fs.NestedDirectorySize();

        var dirsUndeMil = dirs.Where(x => x.Value < 100000);
        var sum = dirsUndeMil.Sum(x => x.Value);
        return sum.ToString();
    }

    public static string Part2(string[] input)
    {
        return "NOT IMPLEMENTED";
    }
}

public class FileSystem
{
    private Dictionary<string, long> Directories = new Dictionary<string, long>();


    public FileSystem(string[] rawInputs)
    {
        var cwd = "/";
        bool lastLs = false;

        List<string> lines = new List<string>();


        for (var i = 0; i < rawInputs.Length; i++)
        {
            var line = rawInputs[i];

            //ls entry not command
            if (!line.StartsWith("$"))
            {
                lines.Add(line);
                continue;
            }


            //if it is end of ls command add directory entry
            if (lastLs)
            {
                AddDirectory(cwd, lines);
            }

            if (line.StartsWith("$ cd"))
            {
                lastLs = false;

                var changeTo = line[5..];
                switch (changeTo)
                {
                    case "..":
                        cwd = cwd[..cwd.LastIndexOf("/")];
                        if (!cwd.StartsWith("/"))
                        {
                            cwd = "/";
                        }

                        continue;
                    case "/":
                        cwd = "/";
                        continue;
                    default:

                        if (cwd != "/")
                        {
                            cwd += "/";
                        }

                        cwd += changeTo;

                        break;
                }
            }

            if (line.StartsWith("$ ls"))
            {
                lastLs = true;
                lines = new List<string>();
            }
        }

        //if last command is ls
        if (lastLs)
        {
            AddDirectory(cwd, lines);
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
        var Sizes = Directories
            .OrderByDescending(x => x.Key.Length)
            .ToDictionary(x => x.Key, x => x.Value);

        foreach (var (name, size) in Sizes)
        {
            Console.WriteLine(name);

            if (name == "/")
            {
                continue;
            }
            
            var newName = name[..name.LastIndexOf("/")];

            if (newName == "")
            {
                newName = "/";
            }


            //recursively add

            Console.WriteLine($"Add  {size} FROM {name} TO {newName}");

            Sizes[newName] += size;
            
        }

        return Sizes;
    }
}