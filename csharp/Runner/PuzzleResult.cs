using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Runner
{
    public class PuzzleResult
    {
        public TimeSpan Took;
        public string Result;
        public Puzzle.Part RunnedPart;

        public PuzzleResult(TimeSpan took, string result, Puzzle.Part runnedPart)
        {
            Took = took;
            Result = result;
            RunnedPart = runnedPart;

        }
    }
}
