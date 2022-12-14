namespace Csharp.Year2022.Day13;

public class Solution
{
	public static string Part1(string[] input)
	{
		var parsed = input
			.Chunk(3)
			.Select(chunk => chunk
				.Take(2)
				.Select(x => new Packet(x))
				.ToArray())
			.Select(x => (x[0], x[1])).ToArray();

		var bigger = parsed.Select((val, index) => (index, val.Item1.CompareTo(val.Item2))).ToList();
		var results = bigger.Where(x => x.Item2 < 0).Select(x => x.index + 1).ToArray();

		return results.Sum().ToString();
	}

	public static string Part2(string[] input)
	{
		var parsed = input.Where(x => !String.IsNullOrEmpty(x))
			.Select(x => new Packet(x)).ToList();


		var p1 = new Packet("[[2]]");
		var p2 = new Packet("[[6]]");
		parsed.Add(p1);
		parsed.Add(p2);

		var filtered = parsed.OrderBy(x => x).ToList();

		foreach (Packet packet in filtered)
		{
			Console.WriteLine(packet.packetString);
		}

		return ((filtered.IndexOf(p1) + 1) * (filtered.IndexOf(p2) + 1)).ToString();
	}
}

public class Packet : IComparable<Packet>
{
	private List<Packet> innerChilder = new List<Packet>();
	private bool isNumber = true;
	private int number = 0;
	public string packetString;


	public Packet(string rawPacket)
	{
		packetString = rawPacket;

		//empty list is always smaller
		if (packetString == "[]")
		{
			isNumber = false;
			innerChilder = new List<Packet>();
			return;
		}

		//number packet
		if (!packetString.StartsWith("["))
		{
			number = int.Parse(packetString);
			return;
		}

		//list packet
		isNumber = false;
		int nestLevel = 0;
		string currentChildPacket = "";

		foreach (var ch in packetString[1..^1])
		{
			if (ch == ',')
			{
				// Console.WriteLine(nestLevel + "   " + currentChildPacket);
			}

			if (ch == ',' && nestLevel == 0)
			{
				innerChilder.Add(new Packet(currentChildPacket));
				currentChildPacket = "";
				continue;
			}

			currentChildPacket += ch;

			if (ch == '[')
			{
				nestLevel++;
			}

			if (ch == ']')
			{
				nestLevel--;
			}
		}

		if (!string.IsNullOrEmpty(currentChildPacket))
		{
			innerChilder.Add(new Packet(currentChildPacket));
		}
	}

	public int CompareTo(Packet that)
	{
		//if both are integers
		if (this.isNumber && that.isNumber)
		{
			// Console.WriteLine($"Comparing numbers {this.number} ==  {that.number}");
			return this.number - that.number;
		}

		if (!this.isNumber && !that.isNumber)
		{
			// Console.WriteLine($"Comparing lists {this.packetString}   ==  {that.packetString}");
			for (int i = 0; i < Math.Min(this.innerChilder.Count, that.innerChilder.Count); i++)
			{
				var diff = this.innerChilder[i].CompareTo(that.innerChilder[i]);

				if (diff != 0)
				{
					// Console.WriteLine($"Diff is {diff}");
					return diff;
				}
			}

			// Console.WriteLine($"lists  elements arent different {this.innerChilder.Count} {that.innerChilder.Count}");
			// Console.WriteLine($"Diff is {this.innerChilder.Count - that.innerChilder.Count}");
			return this.innerChilder.Count - that.innerChilder.Count;
		}


		//convert both to lists
		var packetL = this.isNumber ? new Packet($"[{this.number}]") : this;
		var packetR = that.isNumber ? new Packet($"[{that.number}]") : that;

		return packetL.CompareTo(packetR);
	}
}