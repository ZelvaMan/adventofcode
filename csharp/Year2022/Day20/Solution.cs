using System.Collections;
using System.Numerics;

namespace Csharp.Year2022.Day20;

public class Solution
{
	public static string Part1(string[] input)
	{
		var parsed = input.Select(BigInteger.Parse);
		var Mixer = new Mixer(parsed);

		Mixer.Mix();

		var numbers = Mixer.Numbers.Take(3001).ToArray();
		var result = ReadVal(numbers, 1000) + ReadVal(numbers, 2000) + ReadVal(numbers, 3000);

		return result.ToString();
	}

	private static BigInteger ReadVal(Element[] numbers, BigInteger index)
	{
		return numbers[(long) index % numbers.Length].value;
	}

	public static string Part2(string[] input)
	{
		var parsed = input.Select(BigInteger.Parse).Select(x => x * 811_589_153);

		var Mixer = new Mixer(parsed);
		// Mixer.PrintList();
		// Console.WriteLine("PART 2 CORE START");
		for (BigInteger i = 0; i < 10; i++)
		{
			Mixer.Mix();
			// Mixer.PrintList();
		}

		var numbers = Mixer.Numbers.Take(3001).ToArray();
		var result = ReadVal(numbers, 1000) + ReadVal(numbers, 2000) + ReadVal(numbers, 3000);

		return result.ToString();
	}
}

class Mixer
{
	public List<Element> Numbers;

	public Mixer(IEnumerable<BigInteger> input)
	{
		CreateLinkedList(input);
	}

	private void CreateLinkedList(IEnumerable<BigInteger> input)
	{
		Numbers = new List<Element>();

		Element prev = null;
		foreach (var el in input)
		{
			var cur = new Element(prev, el);

			if (prev != null)
			{
				prev.Right = cur;
			}

			Numbers.Add(cur);
			prev = cur;
		}

		Numbers.First().left = Numbers.Last();
		Numbers.Last().Right = Numbers.First();
	}


	public void Mix()
	{
		//nums order doesnt change

		foreach (var cur in Numbers)
		{
			// Console.WriteLine($"DO MOVE FOR {cur.value}");
			//move right
			if (cur.value == 0) continue;

			if (Math.Sign((long) cur.value) == 1)
			{
				for (BigInteger i = 0; i < cur.value % (Numbers.Count - 1); i++)
				{
					// Console.WriteLine("move one to the right of" + cur.Right.value);
					cur.MoveRight();
				}
			}
			else
			{
				for (BigInteger i = 0; i < Math.Abs((long) (cur.value % (Numbers.Count - 1))); i++)
				{
					// Console.WriteLine("move one to the left of" + cur.left.value);
					cur.MoveLeft();
				}
			}
		}
	}

	public List<Element> BuildList()
	{
		var head = Numbers.Where(x => x.value == 0).First();

		var current = head;
		var List = new List<Element>();
		do
		{
			List.Add(current);
			current = current.Right;
		} while (current != head);

		return List;
	}

	public void PrintList()
	{
		foreach (var element in BuildList())
		{
			Console.Write(element.value + ", ");
		}

		Console.Write("\n");
	}
}

internal class Element
{
	public Element left, Right;
	public readonly BigInteger value;

	public Element(Element left, BigInteger value)
	{
		this.left = left;
		this.value = value;
		Right = null;
	}

	public void MoveRight()
	{
		//we need to change is head to moved head

		var el1 = this.left;
		var el2 = this.Right;
		var el3 = this;
		var el4 = Right.Right;

		el1.Right = el2;
		el2.left = el1;

		el2.Right = el3;
		el3.left = el2;

		el3.Right = el4;
		el4.left = el3;
	}


	public void MoveLeft()
	{
		left.MoveRight();
	}
}