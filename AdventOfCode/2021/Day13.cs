using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using TBC = System.Tuple<System.Collections.Generic.IEnumerable<AdventOfCode.Helpers.Position>, System.Collections.Generic.IEnumerable<string>>;

namespace AdventOfCode._2021
{
    public class Day13 : PuzzleBase<TBC>
    {
        public Day13(ITestOutputHelper outputHelper) : base(13, 2021, "17", "see test output", outputHelper) { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            var positions = input.TakeWhile(i => !string.IsNullOrWhiteSpace(i)).Select(m => m.Split(",")).Select(m => new Position(int.Parse(m[0]), int.Parse(m[1])));
            var instructinos = input.SkipWhile(i => !string.IsNullOrWhiteSpace(i)).Skip(1).Select(m => m);

            return new TBC(positions, instructinos);
        }

        public override string SolveProblem1(TBC input)
        {
            var result = Fold(input.Item1, input.Item2, 1);

            return result.Distinct().Count().ToString();
        }

        private List<Position> Fold(IEnumerable<Position> grid, IEnumerable<string> instructions, int folds)
        {
            var outerGrid = new List<Position>();
            outerGrid.AddRange(grid);
            for (int i = 0; i < folds; i++)
            {
                List<Position> newGrid = new List<Position>();

                var maxY = outerGrid.Select(x => x.y).Max();
                var maxX = outerGrid.Select(x => x.x).Max();

                var foldInstruction = instructions.Skip(i).First();

                var idx = foldInstruction.Split("=")[0].LastIndexOf(" ");
                var axis = foldInstruction.Split("=")[0].Substring(11, 1);
                var rowOrColumn = int.Parse(foldInstruction.Split("=")[1]);

                if (axis.Equals("y"))
                {
                    var toAdd = outerGrid.Where(xy => xy.y < rowOrColumn);
                    newGrid.AddRange(toAdd);

                    var toTransform = outerGrid.Where(xy => xy.y > rowOrColumn);
                    foreach (var item in toTransform)
                    {
                        var newY = maxY-item.y;
                        var newPosition = new Position(item.x, newY);
                        newGrid.Add(newPosition);
                    }
                }

                if (axis.Equals("x"))
                {
                    var toAdd = outerGrid.Where(xy => xy.x < rowOrColumn);
                    newGrid.AddRange(toAdd);

                    var toTransform = outerGrid.Where(xy => xy.x > rowOrColumn);
                    foreach (var item in toTransform)
                    {
                        var newX = maxX-item.x;
                        var newPosition = new Position(newX, item.y);
                        newGrid.Add(newPosition);
                    }
                }

                outerGrid = newGrid;
            }

            return outerGrid;
        }

        public override string SolveProblem2(TBC input)
        {
            var result = Fold(input.Item1, input.Item2, input.Item2.Count());

            result.PrintGrid(OutputHelper, " ");

            return "see test output";
        }
    }
}
