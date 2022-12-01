using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Runner
{
    public class Puzzle
    {
        public enum Part
        {
            first, second, both
        }

        public int Day { private set; get; }
        public int Year { private set; get; }

        private string InputPath { set; get; }


        public Puzzle(Arguments args)
        {
            Day = args.Day;
            Year = args.Year;
            InputPath = args.InputPath;
        }

        public (PuzzleResult, PuzzleResult) SolveBoth()
        {
            return (SolvePart(Part.first), SolvePart(Part.second));

        }

        public PuzzleResult SolvePart(Part part)
        {
            var methodNumber = part switch
            {
                Part.first => 1,
                Part.second => 2,
            };

            var method = getMethod(methodNumber);


            var input = File.ReadAllLines(InputPath);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var methodReturn = method.Invoke(null, new[] { input });

            stopwatch.Stop();

            if (methodReturn is not string)
            {
                throw new Exception("Solution method didn't return string");
            }

            return new PuzzleResult(stopwatch.Elapsed, (string)methodReturn, part);

        }

        private MethodInfo getMethod(int partNumber)
        {
            string paddedDay = Day.ToString().PadLeft(2, '0');

            string path = $"Csharp.Year{Year}.Day{paddedDay}.Solution";

            Type puzzleSolutionClass = Type.GetType(path);

            if (puzzleSolutionClass == null)
            {
                throw new Exception("Solution not found");
            }


            string methodName = "Part" + partNumber;

            MethodInfo method = puzzleSolutionClass.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);

            if (method == null)
            {
                throw new Exception($"method ${methodName} doesnt exist in class");
            }

            return method;
        }
    }
}
