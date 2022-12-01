using CommandLine;

namespace AOC.Runner;

public class Arguments
{
    public enum ProblemPart{
        all,first,second
    }
    
    public int Day { get; set; }
    public int Year { get; set; }
    public ProblemPart Part { get; set; }

    public Arguments(string[] args)
    {
        //parse part
        var part = args.Where(x => x.StartsWith("-p")).Select(x => x[3..]).ToArray();
        if (part.Any())
        {
            Part = part.First() switch
            {
                "1" => ProblemPart.first,
                "2" => ProblemPart.second,
                _ => ProblemPart.all
            };
        }
        else
        {
            Part = ProblemPart.all;
        }

        var argsWithoutOptions = args.Where(x => !x.StartsWith("-")).ToList();

        var today = DateTime.Today;

        Day = today.Day;
        Year = today.Year;
        if (argsWithoutOptions.Count > 0)
        {
            Day = int.Parse(argsWithoutOptions[0]); 

        }

        if (argsWithoutOptions.Count > 1)
        {
            Year = int.Parse(argsWithoutOptions[1]); 

        }
    }
}