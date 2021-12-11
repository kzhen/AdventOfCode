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
    public class Day11 : PuzzleBase<TBC>
    {
        public Day11(ITestOutputHelper output) : base(11, 2021, "1656", "195", output) { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            //return input.Select(int.Parse);
            var result = input.SelectMany((line, idx) =>
                line.Select((col, colIdx) => new { pos = new Position(colIdx, idx), value = int.Parse(col.ToString()) }
                )

            ).ToDictionary(k => k.pos, v => v.value);

            return result;
        }

        public override string SolveProblem1(TBC input)
        {
            long increments = 0;

            for (int i = 0; i < 100; i++)
            {
                foreach (var pos in input)
                {
                    input[pos.Key]++;

                }

                if (input.Any(pos => pos.Value > 9))
                {
                    var visited = new List<Position>();

                    do
                    {
                        var pos = input.First(x => x.Value > 9 && !visited.Contains(x.Key));
                        visited.Add(pos.Key);
                        input[pos.Key] = 0;
                        increments++;
                        IncrementAdjacent(input, pos.Key, visited);

                    } while (input.Any(x => x.Value > 9 && !visited.Contains(x.Key)));
                }

                //if any with >9
                //increment flashes
                //increase all adjacent octopuses
                //set energy level to 0
                //if any greater than 9 (that haven't flashed already)
                //goto start

            }

            return increments.ToString();
        }

        private void IncrementAdjacent(TBC input, Position pos, List<Position> visited)
        {
            //right
            var right = input.FirstOrDefault(x => x.Key.x == pos.x + 1 && x.Key.y == pos.y && !visited.Contains(x.Key));
            if (right.Key != null)
            {
                input[right.Key]++;
            }

            //left
            var left = input.FirstOrDefault(x => x.Key.x == pos.x - 1 && x.Key.y == pos.y && !visited.Contains(x.Key));
            if (left.Key != null)
            {
                input[left.Key]++;
            }

            //top
            var top = input.FirstOrDefault(x => x.Key.x == pos.x && x.Key.y == pos.y - 1 && !visited.Contains(x.Key));
            if (top.Key != null)
            {
                input[top.Key]++;
            }

            //bottom
            var bottom = input.FirstOrDefault(x => x.Key.x == pos.x && x.Key.y == pos.y + 1 && !visited.Contains(x.Key));
            if (bottom.Key != null)
            {
                input[bottom.Key]++;
            }

            //diagonal left top
            var diagTopLeft = input.FirstOrDefault(x => x.Key.x == pos.x - 1 && x.Key.y == pos.y - 1 && !visited.Contains(x.Key));
            if (diagTopLeft.Key != null)
            {
                input[diagTopLeft.Key]++;
            }

            //diagonal right top
            var diagTopRight = input.FirstOrDefault(x => x.Key.x == pos.x + 1 && x.Key.y == pos.y - 1 && !visited.Contains(x.Key));
            if (diagTopRight.Key != null)
            {
                input[diagTopRight.Key]++;
            }

            //diagonal left bottom
            var diagBottomLeft = input.FirstOrDefault(x => x.Key.x == pos.x - 1 && x.Key.y == pos.y + 1 && !visited.Contains(x.Key));
            if (diagBottomLeft.Key != null)
            {
                input[diagBottomLeft.Key]++;
            }
            //diagonal right bottom
            var diagBottomRight = input.FirstOrDefault(x => x.Key.x == pos.x + 1 && x.Key.y == pos.y + 1 && !visited.Contains(x.Key));
            if (diagBottomRight.Key != null)
            {
                input[diagBottomRight.Key]++;
            }

        }

        public override string SolveProblem2(TBC input)
        {
            long increments = 0;
            int i = 1;

            while (true)
            {
                foreach (var pos in input)
                {
                    input[pos.Key]++;

                }

                if (input.Any(pos => pos.Value > 9))
                {
                    var visited = new List<Position>();

                    do
                    {
                        var pos = input.First(x => x.Value > 9 && !visited.Contains(x.Key));
                        visited.Add(pos.Key);
                        input[pos.Key] = 0;
                        increments++;
                        IncrementAdjacent(input, pos.Key, visited);

                    } while (input.Any(x => x.Value > 9 && !visited.Contains(x.Key)));
                }

                //if any with >9
                //increment flashes
                //increase all adjacent octopuses
                //set energy level to 0
                //if any greater than 9 (that haven't flashed already)
                //goto start
                if (input.All(x => x.Value == 0))
                {
                    return (i ).ToString();
                }
                i++;
            }
        }
        //   return increments.ToString();

    }
}

