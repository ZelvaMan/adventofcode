using System.Collections;

namespace Csharp.Year2022.Day20;

public class Solution
{
	public static string Part1(string[] input)
	{
		var parsed = input.Select(int.Parse);
		var Mixer = new Mixer(parsed);

		Mixer.Mix();

		var numbers = Mixer.Numbers.Take(3001).ToArray();
		var result = ReadVal(numbers,1000) + ReadVal(numbers,2000) + ReadVal(numbers,3000);

		return result.ToString();
	}

	private static int ReadVal(Element [] numbers, int index)
	{
		return numbers[index % numbers.Length].value;
	}
	
	public static string Part2(string[] input)
	{
		return "NOT IMPLEMENTED";
	}
}

class Mixer
{
	public List<Element> Numbers;

	public Mixer(IEnumerable<int> input)
	{
		CreateLinkedList(input);
	}

	private void CreateLinkedList(IEnumerable<int> input)
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
			Console.WriteLine($"DO MOVE FOR {cur.value}");
			//move right
			if (cur.value == 0) continue;

			if (Math.Sign(cur.value) == 1)
			{
				for (int i = 0; i < cur.value % (Numbers.Count - 1); i++)
				{
					// Console.WriteLine("move one to the right of" + cur.Right.value);
					cur.MoveRight();
				}
			}
			else
			{
				for (int i = 0; i < Math.Abs(cur.value % (Numbers.Count - 1)); i++)
				{
					// Console.WriteLine("move one to the left of" + cur.left.value);
					cur.MoveLeft();
				}
			}

			BuildList();
		}

		Numbers = BuildList();
	}

	public List<Element> BuildList()
	{
		var head = Numbers.Where(x => x.value == 0).First();

		var current = head;
		var List = new List<Element>();
		do
		{
			List.Add(current);
			// Console.Write(current.value + ", ");
			current = current.Right;
		} while (current != head);

		// Console.Write("\n");
		return List;
	}
}

internal class Element
{
	public Element left, Right;
	public readonly int value;

	public Element(Element left, int value)
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