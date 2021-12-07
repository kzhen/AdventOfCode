using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TBC = System.Collections.Generic.IEnumerable<AdventOfCode._2021.Vector>;

namespace AdventOfCode._2021
{
    public record Vector(Position Start, Position End, bool IsDiagonalDirection);

    public class Day5 : PuzzleBase<TBC>
    {
        private Position ConvertToPosition(string input)
        {
            var xy = input.Split(",").Select(i => int.Parse(i)).ToArray();

            return new Position(xy[0], xy[1]);
        }

        public Day5() : base(5, 2021, "5", "12") { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            var result = input.Select(line =>
                line.Split(" -> ")
                    .Select(i => ConvertToPosition(i))
                    .ToArray())
                .Select(posMap =>
                    new Vector(posMap[0],
                                posMap[1],
                                !(posMap[0].x == posMap[1].x || posMap[0].y == posMap[1].y))
                );

            return result;
        }

        public enum Direction
        {
            UP,
            DOWN,
            NOMOVEMENT
        }

        public enum HozDirection
        {
            LEFT,
            RIGHT,
            NOMOVEMENT
        }

        public override string SolveProblem1(TBC input)
        {
            var grid = new Dictionary<Position, int>();

            foreach (var item in input.Where(i => !i.IsDiagonalDirection))
            {
                var lengthOfTravel = 0;
                Direction xdirection;
                HozDirection ydirection;
                //0,9 - 5,9
                //is it going up/down

                if (item.Start.x > item.End.x)
                {
                    xdirection = Direction.UP;
                    lengthOfTravel = item.Start.x - item.End.x;
                }
                else if (item.Start.x < item.End.x)
                {
                    xdirection = Direction.DOWN;
                    lengthOfTravel = item.End.x - item.Start.x;
                }
                else if (item.Start.x == item.Start.x)
                {
                    xdirection = Direction.NOMOVEMENT;
                }
                else
                {
                    throw new Exception("ummmm xdirection?!");
                }

                //is it going left/right
                if (item.Start.y > item.End.y)
                {
                    ydirection = HozDirection.LEFT;
                    lengthOfTravel = item.Start.y - item.End.y;
                }
                else if (item.Start.y < item.End.y)
                {
                    ydirection = HozDirection.RIGHT;
                    lengthOfTravel = item.End.y - item.Start.y;
                }
                else if (item.Start.y == item.Start.y)
                {
                    ydirection = HozDirection.NOMOVEMENT;
                }
                else
                {
                    throw new Exception("ummmm ydirection?!");
                }

                int y = 0;
                int x = 0;
                //if going up, increment is -1, if going down increment is +1
                //if going right, increment is +1, if going left increment is -1
                var increment = 0;
                if (xdirection != Direction.NOMOVEMENT)
                {
                    //fix y
                    y = item.Start.y;

                    increment = (xdirection == Direction.UP) ? -1 : 1;
                }
                else
                {
                    //fix x
                    x = item.Start.x;
                    increment = (ydirection == HozDirection.LEFT) ? -1 : 1;
                }

                //loop
                var idx = 0;
                for (int i = 0; i <= lengthOfTravel; i++)
                {
                    if (xdirection != Direction.NOMOVEMENT)
                    {
                        var position = new Position(item.Start.x + idx, y);
                        //grid[position]++;
                        if (grid.TryGetValue(position, out var currentVal))
                        {
                            grid[position] = currentVal +1;
                        }
                        else
                        {
                            grid.Add(position, 1);
                        }
                    }

                    if (ydirection != HozDirection.NOMOVEMENT)
                    {
                        //var position = new Position(item.Start.x + idx, y);
                        var position = new Position(x, item.Start.y + idx);
                        //grid[position]++;
                        if (grid.TryGetValue(position, out var currentVal))
                        {
                            grid[position] = currentVal +1;
                        }
                        else
                        {
                            grid.Add(position, 1);
                        }
                    }

                    idx += increment;
                }
            }

            var sum = grid.Where(kvp => kvp.Value > 1).Count();

            return sum.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            var grid = new Dictionary<Position, int>();

            foreach (var item in input)
            {
                var lengthOfTravel = 0;
                Direction xdirection;
                HozDirection ydirection;
                //0,9 - 5,9
                //is it going up/down

                if (item.Start.x > item.End.x)
                {
                    xdirection = Direction.UP;
                    lengthOfTravel = item.Start.x - item.End.x;
                }
                else if (item.Start.x < item.End.x)
                {
                    xdirection = Direction.DOWN;
                    lengthOfTravel = item.End.x - item.Start.x;
                }
                else if (item.Start.x == item.Start.x)
                {
                    //diagonal
                    xdirection = Direction.NOMOVEMENT;
                }
                else
                {
                    throw new Exception("ummmm xdirection?!");
                }

                //is it going left/right
                if (item.Start.y > item.End.y)
                {
                    ydirection = HozDirection.LEFT;
                    lengthOfTravel = item.Start.y - item.End.y;
                }
                else if (item.Start.y < item.End.y)
                {
                    ydirection = HozDirection.RIGHT;
                    lengthOfTravel = item.End.y - item.Start.y;
                }
                else if (item.Start.y == item.Start.y)
                {
                    //diagonal
                    ydirection = HozDirection.NOMOVEMENT;
                }
                else
                {
                    throw new Exception("ummmm ydirection?!");
                }

                //diagonal
                if (item.Start.x != item.End.x && item.Start.y != item.End.y)
                {
                    lengthOfTravel = Math.Abs(item.Start.x - item.End.x);
                }

                int y = 0;
                int x = 0;
                //if going up, increment is -1, if going down increment is +1
                //if going right, increment is +1, if going left increment is -1
                var increment = 0;
                var diagonalYIncrement = 0;

                if (item.IsDiagonalDirection)
                {
                    diagonalYIncrement = (ydirection == HozDirection.LEFT) ? -1 : 1;
                }

                if (xdirection != Direction.NOMOVEMENT)
                {
                    //fix y
                    y = item.Start.y;

                    increment = (xdirection == Direction.UP) ? -1 : 1;
                }
                else
                {
                    //fix x
                    x = item.Start.x;
                    increment = (ydirection == HozDirection.LEFT) ? -1 : 1;
                }

                if (item.IsDiagonalDirection)
                {
                    y = item.Start.y;
                    x = item.Start.x;
                }

                //loop
                var idx = 0;
                for (int i = 0; i <= lengthOfTravel; i++)
                {
                    if (item.IsDiagonalDirection)
                    {
                        var position = new Position(x, y);
                        if (grid.TryGetValue(position, out var currentVal))
                        {
                            grid[position] = currentVal +1;
                        }
                        else
                        {
                            grid.Add(position, 1);
                        }
                        x += increment;
                        y += diagonalYIncrement;
                    }
                    else
                    {
                        if (xdirection != Direction.NOMOVEMENT)
                        {
                            var position = new Position(item.Start.x + idx, y);
                            if (grid.TryGetValue(position, out var currentVal))
                            {
                                grid[position] = currentVal +1;
                            }
                            else
                            {
                                grid.Add(position, 1);
                            }
                        }

                        if (ydirection != HozDirection.NOMOVEMENT)
                        {
                            var position = new Position(x, item.Start.y + idx);
                            if (grid.TryGetValue(position, out var currentVal))
                            {
                                grid[position] = currentVal +1;
                            }
                            else
                            {
                                grid.Add(position, 1);
                            }
                        }
                    }


                    idx += increment;
                }


            }

            var sum = grid.Where(kvp => kvp.Value > 1).Count();

            grid.PrintGrid(".");

            return sum.ToString();
        }
    }
}
