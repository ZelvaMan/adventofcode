namespace AOC.Runner;

public class Arguments
{
	public Arguments(string[] args)
	{
		//parse part
		var part = args.Where(x => x.StartsWith("-p")).Select(x => x[3..]).ToArray();
		if (part.Any())
			Part = part.First() switch
			{
				"1" => Puzzle.Part.first,
				"2" => Puzzle.Part.second,
				_ => Puzzle.Part.both
			};
		else
			Part = Puzzle.Part.both;


		var argsWithoutOptions = args.Where(x => !x.StartsWith("-")).ToList();

		var today = DateTime.Today;

		Day = today.Day;
		Year = today.Year;

		if (argsWithoutOptions.Count > 0) Day = int.Parse(argsWithoutOptions[0]);

		if (argsWithoutOptions.Count > 1) Year = int.Parse(argsWithoutOptions[1]);

		var input = args.Where(x => x.StartsWith("-i")).Select(x => x[3..]).ToArray();

		if (input.Any())
		{
			InputPath = input.First();
		}
		else
		{
			var folder = $".\\Year{Year}\\Day{Day.ToString().PadLeft(2, '0')}\\";

			if (args.Any(x => x == "-ti"))
				InputPath = folder + "ti.txt";
			else
				InputPath = folder + "i.txt";
		}
	}

	public int Day { get; set; }
	public int Year { get; set; }
	public Puzzle.Part Part { get; set; }

	public string InputPath { get; set; }
}