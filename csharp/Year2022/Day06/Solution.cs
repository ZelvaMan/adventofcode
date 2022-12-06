namespace Csharp.Year2022.Day06;


public class Solution
{
    public static string Part1(string[] input)
    {
        var packetRaw = input[0];
        for (var i = 0; i < packetRaw.Length; i++)
        {
            var lastFourCharacters = packetRaw.Substring(i, 4);

            //found packet 
            if (lastFourCharacters.Distinct().Count() == 4)
            {
                return (i+4).ToString();
            }
                
            Console.WriteLine(lastFourCharacters);
        }


        return "NO MARKER FOUND";
    }

    public static string Part2(string[] input)
    {
        const int  packetLength = 14;
        
        var packetRaw = input[0];
        for (var i = 0; i < packetRaw.Length; i++)
        {
            var lastFourCharacters = packetRaw.Substring(i, packetLength);

            //found packet 
            if (lastFourCharacters.Distinct().Count() == packetLength)
            {
                return (i+packetLength).ToString();
            }
        }


        return "NO MARKER FOUND";

    }
}