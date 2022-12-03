namespace AOC.Runner;

public class PuzzleResult
{
	public string Result;
	public Puzzle.Part RunnedPart;
	public TimeSpan Took;

	public PuzzleResult(TimeSpan took, string result, Puzzle.Part runnedPart)
	{
		Took = took;
		Result = result;
		RunnedPart = runnedPart;
	}
}