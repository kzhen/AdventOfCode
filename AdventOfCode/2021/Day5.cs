using AdventOfCode.Helpers;
using Runner._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TBC = System.Collections.Generic.IEnumerable<AdventOfCode._2021.MapSegment>;

namespace AdventOfCode._2021
{
    public class MapSegment
    {
        public Position Start { get; set; }
        public Position End { get; set; }
        public bool Diagonal { get; internal set; }
    }

    public class Day5 : PuzzleBase<TBC>
    {
        public Day5() : base(5, 2021, "5", "12") { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            foreach (var item in input)
            {
                var segments = item.Split(" -> ");

                var firstSegmentStart = int.Parse(segments[0].Substring(0, item.IndexOf(",")));
                var firstSegmentEnd = int.Parse(segments[0].Substring(segments[0].IndexOf(",") + 1));

                var secondSegmentStart = int.Parse(segments[1].Substring(0, segments[1].IndexOf(",")));
                var secondSegmentEnd = int.Parse(segments[1].Substring(segments[1].IndexOf(",") + 1));

                yield return new MapSegment
                {
                    Start = new Position(firstSegmentStart, firstSegmentEnd),
                    End = new Position(secondSegmentStart, secondSegmentEnd),
                    Diagonal = !(firstSegmentStart == secondSegmentStart || firstSegmentEnd == secondSegmentEnd)
                };

            }
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

            foreach (var item in input.Where(i => !i.Diagonal))
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



            Console.Clear();
            

            return sum.ToString();
        }

        public string SolveProblem2aa(TBC input)
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
                    ydirection = HozDirection.RIGHT;
                    lengthOfTravel = item.Start.x - item.End.x;
                }
                else if (item.Start.x < item.End.x)
                {
                    ydirection = HozDirection.LEFT;
                    lengthOfTravel = item.End.x - item.Start.x;
                }
                else if (item.Start.x == item.Start.x)
                {
                    //diagonal
                    ydirection = HozDirection.NOMOVEMENT;
                }
                else
                {
                    throw new Exception("ummmm xdirection?!");
                }

                //is it going left/right
                if (item.Start.y > item.End.y)
                {
                    xdirection = Direction.DOWN;
                    lengthOfTravel = item.Start.y - item.End.y;
                }
                else if (item.Start.y < item.End.y)
                {
                    xdirection = Direction.UP;
                    lengthOfTravel = item.End.y - item.Start.y;
                }
                else if (item.Start.y == item.Start.y)
                {
                    //diagonal
                    xdirection = Direction.NOMOVEMENT;
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

                if (item.Diagonal)
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

                if (item.Diagonal)
                {
                    y = item.Start.y;
                    x = item.Start.x;
                }

                //loop
                var idx = 0;
                for (int i = 0; i <= lengthOfTravel; i++)
                {
                    if (item.Diagonal)
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
                    }


                    idx += increment;
                }


            }

            var sum = grid.Where(kvp => kvp.Value > 1).Count();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var pos = grid.TryGetValue(new Position(i, j), out int valAtPos);
                    if (valAtPos > 0)
                    {
                        Console.Write(valAtPos.ToString());
                    }
                    Console.Write(".");
                }
                Console.WriteLine();
            }

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

                if (item.Diagonal)
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

                if (item.Diagonal)
                {
                    y = item.Start.y;
                    x = item.Start.x;
                }

                //loop
                var idx = 0;
                for (int i = 0; i <= lengthOfTravel; i++)
                {
                    if (item.Diagonal)
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
                    }
                    

                    idx += increment;
                }

                
            }

            var sum = grid.Where(kvp => kvp.Value > 1).Count();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var pos = grid.TryGetValue(new Position(i, j), out int valAtPos);
                    if (valAtPos > 0)
                    {
                        Console.Write(valAtPos.ToString());
                    }
                    Console.Write(".");
                }
                Console.WriteLine();
            }

            return sum.ToString();
        }
    }
}
