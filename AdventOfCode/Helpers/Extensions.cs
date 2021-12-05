using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public static class Extensions
    {
        public static void PrintGrid(this Dictionary<Position, int> grid, string blankOrZeroChar = "-0-")
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var pos = grid.TryGetValue(new Position(i, j), out int valAtPos);
                    if (valAtPos > 0)
                    {
                        Console.Write(valAtPos.ToString());
                    }
                    Console.Write(blankOrZeroChar);
                }
                Console.WriteLine();
            }
        }
    }
}
