namespace AOC.Runner;

public class Runner
{
	private readonly Arguments args;

	public Runner(Arguments args)
	{
		this.args = args;
	}

	public void RunBoth()
	{
		var Puzzle = new Puzzle(args);
		var (p1, p2) = Puzzle.SolveBoth();

		printResult(p1);
		printResult(p2);
	}

	private void printResult(PuzzleResult puzzleResult)
	{
		var headerString =
			$"--------------------{formattedPart(puzzleResult.RunnedPart)} in {puzzleResult.Took.Milliseconds}ms--------------------";
		Console.WriteLine(headerString);
		Console.WriteLine(puzzleResult.Result);
		Console.WriteLine();
	}

	private string formattedPart(Puzzle.Part part)
	{
		return part switch
		{
			Puzzle.Part.first => "Part ONE",
			Puzzle.Part.second => "Part TWO",
			Puzzle.Part.both => "BOTH"
		};
	}
}