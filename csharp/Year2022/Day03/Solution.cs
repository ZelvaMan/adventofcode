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
        //get all common chars between half's of the line 
        var commonItems = input
            .Select(x =>
            (
                x[..(x.Length / 2)],
                x[(x.Length / 2)..]
            ))
            .Select(x =>
                x.Item1.Intersect(x.Item2).First()
            ).ToList();

        //only return one, because there is always only one
        return commonItems;
    }

    private static List<char> CommonItemInGroup(string[] input)
    {
        var commonItems = input
            .Chunk(3)
            .Select(s =>
            {
                //get intersecting char between all three strings
                var result = new HashSet<char>(s[0]);
                result.IntersectWith(s[1]);
                result.IntersectWith(s[2]);
                //there is always only one character
                return result.First();
            }).ToList();

        return commonItems;
    }

    /// <summary>
    /// for CAPITAL letters subtract 65(A)  and then add 27 = -38
    /// for non capital subtract 96(A)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static int GetPriority(char item) =>
        item < 97
            ? item - 38
            : item - 96;
}