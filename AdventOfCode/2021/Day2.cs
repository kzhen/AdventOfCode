using System;
using System.Collections.Generic;

namespace AdventOfCode._2021
{
    public class Day2 : PuzzleBase<IEnumerable<string>>
    {
        public Day2() : base(2, 2021, "150", "900")
        {

        }
        public override IEnumerable<string> ParseInput(IEnumerable<string> input)
        {
            return input;
        }

        public override string SolveProblem1(IEnumerable<string> input)
        {
            int hoz = 0;
            int depth = 0;

            foreach (var line in input)
            {
                var l = line.Split(" ");
                switch (l[0])
                {
                    case "forward":
                        hoz += Convert.ToInt32(l[1]);

                        break;
                    case "down":
                        depth += Convert.ToInt32(l[1]);
                        break;
                    case "up":
                        depth -= Convert.ToInt32(l[1]);
                        break;
                }
            }
            var total = hoz * depth;

            return total.ToString();
        }

        public override string SolveProblem2(IEnumerable<string> input)
        {
            int hoz = 0;
            int depth = 0;
            int aim = 0;

            foreach (var line in input)
            {
                var l = line.Split(" ");
                switch (l[0])
                {

                    case "forward":
                        depth += (aim * Convert.ToInt32(l[1]));
                        hoz += Convert.ToInt32(l[1]);

                        break;
                    case "down":
                        aim += Convert.ToInt32(l[1]);
                        break;
                    case "up":
                        aim -= Convert.ToInt32(l[1]);
                        break;
                }
            }
            var total = hoz * depth;

            return total.ToString();
        }
    }
}
