using System.Diagnostics;
using System.Reflection;

namespace AOC.Runner;

public class Puzzle
{
	public enum Part
	{
		first,
		second,
		both
	}


	public Puzzle(Arguments args)
	{
		Day = args.Day;
		Year = args.Year;
		InputPath = args.InputPath;
	}

	public int Day { get; }
	public int Year { get; }

	private string InputPath { get; }

	public (PuzzleResult, PuzzleResult) SolveBoth()
	{
		return (SolvePart(Part.first), SolvePart(Part.second));
	}

	public PuzzleResult SolvePart(Part part)
	{
		var methodNumber = part switch
		{
			Part.first => 1,
			Part.second => 2
		};

		var method = getMethod(methodNumber);


		var input = File.ReadAllLines(InputPath);
		var stopwatch = new Stopwatch();
		stopwatch.Start();

		var methodReturn = method.Invoke(null, new[] {input});

		stopwatch.Stop();

		if (methodReturn is not string) throw new Exception("Solution method didn't return string");

		return new PuzzleResult(stopwatch.Elapsed, (string) methodReturn, part);
	}

	private MethodInfo getMethod(int partNumber)
	{
		var paddedDay = Day.ToString().PadLeft(2, '0');

		var path = $"Csharp.Year{Year}.Day{paddedDay}.Solution";

		var puzzleSolutionClass = Type.GetType(path);

		if (puzzleSolutionClass == null) throw new Exception("Solution not found");


		var methodName = "Part" + partNumber;

		var method = puzzleSolutionClass.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);

		if (method == null) throw new Exception($"method ${methodName} doesnt exist in class");

		return method;
	}
}