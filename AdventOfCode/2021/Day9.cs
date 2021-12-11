using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using TBC = System.Collections.Generic.Dictionary<AdventOfCode.Helpers.Position, int>;

namespace AdventOfCode._2021
{
    public class Day9 : PuzzleBase<TBC>
    {
        public Day9(ITestOutputHelper output) : base(9, 2021, "15", "1134", output) { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            var result = input.SelectMany((line, idx) =>
                line.Select((col, colIdx) => new { pos = new Position(colIdx, idx), value = int.Parse(col.ToString()) }
                )

            ).ToDictionary(k => k.pos, v => v.value);

            return result;
        }

        public override string SolveProblem1(TBC input)
        {
            int totalLowPoints = 0;


            foreach (var point in input)
            {
                var adjacentTop = input.FirstOrDefault(p => p.Key.x == point.Key.x && p.Key.y == point.Key.y - 1);
                var adjacentBottom = input.FirstOrDefault(p => p.Key.x == point.Key.x && p.Key.y == point.Key.y + 1);
                var adjacentLeft = input.FirstOrDefault(p => p.Key.x == point.Key.x - 1 && p.Key.y == point.Key.y);
                var adjacentRight = input.FirstOrDefault(p => p.Key.x == point.Key.x + 1 && p.Key.y == point.Key.y);


                if (
                    (adjacentTop.Key == null || point.Value < adjacentTop.Value) &&
                    (adjacentBottom.Key == null || point.Value < adjacentBottom.Value) &&
                    (adjacentLeft.Key == null || point.Value < adjacentLeft.Value) &&
                    (adjacentRight.Key == null || point.Value < adjacentRight.Value))
                {
                    totalLowPoints += (1 + point.Value);
                }

            }


            return totalLowPoints.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            var pointsSerached = new List<Position>();
            var basinSizes = new List<int>();

            foreach (var point in input.Where(pos => pos.Value != 9 && !pointsSerached.Contains(pos.Key)))
            {
                var size = CalculateBasinSize(point.Key, input);
                pointsSerached.AddRange(size.Item2);
                basinSizes.Add(size.Item1);

            }

            var topThree = basinSizes.OrderByDescending(i => i).Take(3).ToArray();
            var total = (topThree[0] * topThree[1] * topThree[2]);

            return total.ToString();
        }

        private (int, List<Position>) CalculateBasinSize(Position startingPoint, TBC input)
        {
            int totalBasinSize = 0; //starting at one

            var pointsToSearch = new List<Position>();
            var pointsSearched = new List<Position>();

            pointsToSearch.Add(startingPoint);

            do
            {

                var point = pointsToSearch.First();
                if (pointsSearched.Contains(point)) { pointsToSearch.Remove(point); continue; }
                pointsSearched.Add(point);

                var adjacentTop = input.FirstOrDefault(p => p.Key.x == point.x && p.Key.y == point.y - 1 && p.Value != 9 && !pointsToSearch.Contains(p.Key));
                var adjacentBottom = input.FirstOrDefault(p => p.Key.x == point.x && p.Key.y == point.y + 1 && p.Value != 9 && !pointsToSearch.Contains(p.Key));
                var adjacentLeft = input.FirstOrDefault(p => p.Key.x == point.x - 1 && p.Key.y == point.y && p.Value != 9 && !pointsToSearch.Contains(p.Key));
                var adjacentRight = input.FirstOrDefault(p => p.Key.x == point.x + 1 && p.Key.y == point.y && p.Value != 9 && !pointsToSearch.Contains(p.Key));

                if (adjacentTop.Key != null)
                {
                    pointsToSearch.Add(adjacentTop.Key);
                }
                if (adjacentBottom.Key != null)
                {
                    pointsToSearch.Add(adjacentBottom.Key);
                }
                if (adjacentLeft.Key != null)
                {
                    pointsToSearch.Add(adjacentLeft.Key);
                }
                if (adjacentRight.Key != null)
                {
                    pointsToSearch.Add(adjacentRight.Key);
                }

                totalBasinSize++;

                pointsToSearch.Remove(point);
            } while (pointsToSearch.Count > 0);

            return (totalBasinSize, pointsSearched);
        }
    }
}
