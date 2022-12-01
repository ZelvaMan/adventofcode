using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Runner
{
    internal class Puzzle
    {
        public enum Part
        {
            first,second, both
        }

        public int Day { private set; get; }
        public int Year { private set; get; }
        public Part SelectedPart;


        public Puzzle()
        {
            Day = DateTime.Today.Day;
            Year = DateTime.Today.Year;
            SelectedPart = Part.both;
        }

        public Puzzle(int day,int year,Part part)
        {
            Day = day;
            Year = year;
            SelectedPart = part;
        }

        public Dictionary<int,PuzzleResult> Solve()
        {
            return new Dictionary<int,PuzzleResult>();


        }

        private PuzzleResult SolvePart(Part part)
        {
            var method = getMethod(part);
           
           var stopwatch = new Stopwatch();
           stopwatch.Start();
           var methodReturn = method.Invoke(null, null);
           stopwatch.Stop();
           
           if (methodReturn is not string)
           {
               throw new Exception("Solution method didn't return string");
           }

           return new PuzzleResult(stopwatch.Elapsed, (string)methodReturn, part);

        }

        private MethodInfo getMethod(Part part)
        {
            string paddedDay = Day.ToString().PadLeft(2, '0');

            string path = $"AOC.Y{Year}.D{paddedDay}.Solution";

            Type puzzleSolutionClass = Type.GetType(path);

            if (puzzleSolutionClass == null)
            {
                throw new Exception("Solution not found");
            }


            string methodName = part switch
            {
                Part.first => "SolverPart1",
                Part.second => "SolverPart1"
            };

            MethodInfo method = puzzleSolutionClass.GetMethod(methodName, BindingFlags.Static);

            if (method == null)
            {
                throw new Exception($"method ${methodName} doesnt exist in class");
            }

            return method;
        }
    }
}
