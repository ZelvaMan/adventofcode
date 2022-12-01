using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Runner
{
    internal class Runner
    {
        private int day, year;
        private Puzzle.Part part;

        /// <summary>
        /// no params = run both parts of current date
        /// </summary>
        public Runner()
        {
            day = DateTime.Today.Day;
            year = DateTime.Today.Year;
            part = Puzzle.Part.both;
        }

        public Runner(int day)
        {
            this.day = day;
            year = DateTime.Today.Year;
            part = Puzzle.Part.both;
        }
        public Runner(int day, int year)
        {
            this.day = day;
            this.year = year;
            part = Puzzle.Part.both;
        }
        public Runner(int day, int year, int part)
        {
            this.day = day;
            this.year = year;

            switch (part)
            {
                case 0:
                    this.part = Puzzle.Part.both;
                    break;
                case 1:
                    this.part = Puzzle.Part.first;
                    break;
                case 2:
                    this.part = Puzzle.Part.second;
                    break;
            }
        }


    }
}
