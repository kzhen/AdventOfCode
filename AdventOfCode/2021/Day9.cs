using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TBC = System.Collections.Generic.IEnumerable<AdventOfCode.Helpers.Position<int>>;

namespace AdventOfCode._2021
{
    public class Day9 : PuzzleBase<TBC>
    {
        public Day9() : base(9, 2021, "15", "1134") { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            var coll = new List<Position<int>>();
            //return input.Select((line, idx) => new )
            for (int y = 0; y < input.Count(); y++)
            {
                var line = input.Skip(y).First().ToArray();
                coll.AddRange(line.Select((each, x) => new Position<int>(x, y, int.Parse(each.ToString()))));
            }

            return coll;
        }

        public override string SolveProblem1(TBC input)
        {
            int totalLowPoints = 0;

            int minX = 0;
            int minY = 0;

            int maxX = input.Max(i => i.x);
            int maxY = input.Max(i => i.y);


            foreach (var point in input)
            {
                var adjacentTop = input.FirstOrDefault(p => p.x == point.x && p.y == point.y - 1);
                var adjacentBottom = input.FirstOrDefault(p => p.x == point.x && p.y == point.y + 1);
                var adjacentLeft = input.FirstOrDefault(p => p.x == point.x - 1 && p.y == point.y);
                var adjacentRight = input.FirstOrDefault(p => p.x == point.x + 1 && p.y == point.y);


                if (
                    (adjacentTop == null || point.valueAtPos < adjacentTop.valueAtPos) &&
                    (adjacentBottom == null || point.valueAtPos < adjacentBottom.valueAtPos) &&
                    (adjacentLeft == null || point.valueAtPos < adjacentLeft.valueAtPos) &&
                    (adjacentRight == null || point.valueAtPos < adjacentRight.valueAtPos))
                {
                    totalLowPoints += (1 + point.valueAtPos);
                }

            }


            return totalLowPoints.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            var pointsSerached = new List<Position<int>>();
            var basinSizes = new List<int>();

            foreach (var point in input.Where(pos => pos.valueAtPos != 9 && !pointsSerached.Contains(pos)))
            {
                var size = CalculateBasinSize(point, input);
                pointsSerached.AddRange(size.Item2);
                basinSizes.Add(size.Item1);

            }

            var topThree = basinSizes.OrderByDescending(i => i).Take(3).ToArray();
            var total = (topThree[0] * topThree[1] * topThree[2]);

            return total.ToString();

            //return totalLowPoints.ToString();
        }

        private (int, List<Position<int>>) CalculateBasinSize(Position<int> startingPoint, TBC input)
        {
            int totalBasinSize = 0; //starting at one

            var pointsToSearch = new List<Position<int>>();
            var pointsSearched = new List<Position<int>>();

            pointsToSearch.Add(startingPoint);

            do
            {

                var point = pointsToSearch.First();
                if (pointsSearched.Contains(point)) { pointsToSearch.Remove(point); continue; }
                pointsSearched.Add(point);

                var adjacentTop = input.FirstOrDefault(p => p.x == point.x && p.y == point.y - 1 && p.valueAtPos != 9 && !pointsToSearch.Contains(p));
                var adjacentBottom = input.FirstOrDefault(p => p.x == point.x && p.y == point.y + 1 && p.valueAtPos != 9 && !pointsToSearch.Contains(p));
                var adjacentLeft = input.FirstOrDefault(p => p.x == point.x - 1 && p.y == point.y && p.valueAtPos != 9 && !pointsToSearch.Contains(p));
                var adjacentRight = input.FirstOrDefault(p => p.x == point.x + 1 && p.y == point.y && p.valueAtPos != 9 && !pointsToSearch.Contains(p));

                if (adjacentTop != null)
                {
                    pointsToSearch.Add(adjacentTop);
                }
                if (adjacentBottom != null)
                {
                    pointsToSearch.Add(adjacentBottom);
                }
                if (adjacentLeft != null)
                {
                    pointsToSearch.Add(adjacentLeft);
                }
                if (adjacentRight != null)
                {
                    pointsToSearch.Add(adjacentRight);
                }

                totalBasinSize++;

                pointsToSearch.Remove(point);
            } while (pointsToSearch.Count > 0);

            return (totalBasinSize, pointsSearched);
        }
    }
}
