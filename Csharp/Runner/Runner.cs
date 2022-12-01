namespace AOC.Runner;

public class Runner
{
    public static string RunTask(int day, int year, Arguments.ProblemPart part)
    {
        var solution = Type.GetType($"AOC.Year20{year}.Day{day.ToString().PadLeft(2,'0')}");
        if (solution == null)
        {
            throw new Exception("Solution not found");
        }

        
        return String.Empty;
    }
}