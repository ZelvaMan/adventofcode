namespace Csharp.Year2022.Day03;

public class Solution
{
    public static string Part1(string[] input)
    {
        var commonItems = CommonItems(input);
        var sum = commonItems.Select(GetPriority).Sum();

        return sum.ToString();
    }

    public static string Part2(string[] input)
    {
        var badges = CommonItemInGroup(input);
        var sum = badges.Select(GetPriority).Sum();
        return sum.ToString();
    }

    private static List<char> CommonItems(string[] input)
    {
        var commonItems = input.Select(x =>
                (x.Substring(0, x.Length / 2),
                    x.Substring(x.Length / 2, x.Length / 2)))
            .Select(x => x.Item1.ToHashSet().Intersect(x.Item2.ToHashSet()));

        return commonItems.Select(x => x.First()).ToList();
    }

    private static List<char> CommonItemInGroup(string[] input)
    {
        var commonItems = input.Chunk(3)
            .Select(x =>
                x[0].ToHashSet().Intersect(
                    x[1].ToHashSet().Intersect(
                        x[2].ToHashSet()
                    )
                ).First()
            ).ToList();

        return commonItems;
    }

    private static int GetPriority(char item)
    {
        //UPPERCASE
        if (item < 97) return item - 38;

        //lovercase
        return item - 96;
    }
}