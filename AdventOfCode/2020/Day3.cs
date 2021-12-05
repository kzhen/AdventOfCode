using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{

    public class Day3 : PuzzleBase<IEnumerable<WrappingLine>>
    {
        private const string TREE = "#";

        public Day3() : base(3, 2020, "7", "336") { }

        public override IEnumerable<WrappingLine> ParseInput(IEnumerable<string> input)
        {
            return input.Select(l => new WrappingLine(l));
        }

        public override string SolveProblem1(IEnumerable<WrappingLine> input)
        {
            var inputAsList = input.ToList();

            var treesHit = 0;

            //right 3, down 1
            int x = 0; //hoz
            int y = 0; //vert

            for (int i = 0; i < input.Count() - 1; i++)
            {
                x += 3;
                y += 1;

                var coordinateValue = inputAsList[y].GetValueAt(x);
                if (coordinateValue.Equals(TREE))
                {
                    treesHit++;
                }
            }

            //101 too low
            return treesHit.ToString();
        }

        public override string SolveProblem2(IEnumerable<WrappingLine> input)
        {
            //right (x), down (y)
            var combinationsToCheck = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2)
            };

            long result = 1;
            foreach (var combination in combinationsToCheck)
            {
                var inputAsList = input.ToList();

                var treesHit = 0;

                //right 3, down 1
                int x = 0; //hoz
                int y = 0; //vert

                for (int i = 0; i < input.Count() - 1; i++)
                {
                    x += combination.Item1;
                    y += combination.Item2;

                    if (y > input.Count()) break;

                    var coordinateValue = inputAsList[y].GetValueAt(x);
                    if (coordinateValue.Equals(TREE))
                    {
                        treesHit++;
                    }
                }

                result *= treesHit;

            }

            return result.ToString();
        }
    }
}
