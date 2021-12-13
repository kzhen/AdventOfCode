using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public static class Extensions
    {
        public static TValue GetKeyVauleOrSetDefaultValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = defaultValue;
            }

            return dictionary[key];
        }

        public static BigInteger SumBigInteger(this IEnumerable<BigInteger> bigInts)
        {
            var runningValue = new BigInteger(0);
            foreach (var item in bigInts)
            {
                runningValue = BigInteger.Add(runningValue, item);
            }

            return runningValue;
        }

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

        public static void PrintGrid(this IEnumerable<Position> grid, string blankOrZeroChar = ".")
        {
            var maxX = grid.Select(xy => xy.x).Max();
            var maxY = grid.Select(xy => xy.y).Max();

            for (int i = 0; i <= maxX; i++)
            {
                for (int j = 0; j <= maxY; j++)
                {
                    var pos = grid.FirstOrDefault(xy => xy.x == i && xy.y == j);
                    if (pos != null)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(blankOrZeroChar);

                    }
                }
                Console.WriteLine();
            }
        }

    }
}
