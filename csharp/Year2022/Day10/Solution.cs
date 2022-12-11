namespace Csharp.Year2022.Day10;

public class Solution
{
	public static string Part1(string[] input)
	{
		var cpu = new CPU();
		cpu.RunProgram(input);

		return cpu.signalStrength.ToString();
	}

	public static string Part2(string[] input)
	{
		var cpu = new CPU();
		cpu.RunProgram(input);
		cpu.printScreenBuffer();
		return cpu.screenBuffer;
	}
}

class CPU
{
	private long cycle;
	private long RegisterX;
	public string screenBuffer { get; private set; }

	public long signalStrength { get; private set; }

	public CPU()
	{
		cycle = 1;
		RegisterX = 1;
		signalStrength = 0;
		screenBuffer = "";
	}

	public void RunProgram(string[] instructions)
	{
		foreach (var instruction in instructions)
		{
			switch (instruction[..4])
			{
				case "noop":
					Noop();
					break;
				case "addx":
					AddX(int.Parse(instruction[4..]));
					break;
			}
		}
	}


	private void Noop()
	{
		Tick();
	}

	private void AddX(int change)
	{
		//During cycle
		Tick();
		//During cycle
		Tick();
		//After 2 cycles finish
		RegisterX += change;
	}

	private void Tick()
	{
		// Console.WriteLine($"{cycle} = {RegisterX}");

		if (visible())
		{
			screenBuffer += "⬜";
		}
		else
		{
			screenBuffer += " ";
		}

		if (cycle % 40 == 20)
		{
			var currentStrength = cycle * RegisterX;
			// Console.WriteLine($"during {cycle} x = {RegisterX} strength is {currentStrength}");

			signalStrength += currentStrength;

		}

		//new line
		if (cycle % 40 == 0)
		{
			screenBuffer += "\n";

		}


		cycle++;
	}

	private bool visible()
	{
		var currentPixel = cycle % 40;
		return currentPixel == RegisterX || currentPixel == RegisterX + 1 || currentPixel == RegisterX + 2;
	}

	public void printScreenBuffer()
	{
		Console.Write(screenBuffer);
	}
}