using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

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

        public static void PrintGrid(this Dictionary<Position, int> grid, ITestOutputHelper outputHelper, string blankOrZeroChar = "-0-")
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                sb.Clear();
                for (int i = 0; i < 10; i++)
                {
                    var pos = grid.TryGetValue(new Position(i, j), out int valAtPos);
                    if (valAtPos > 0)
                    {
                        sb.Append(valAtPos.ToString());
                    }
                    else
                    {
                        sb.Append(blankOrZeroChar);
                    }
                }
                outputHelper.WriteLine(sb.ToString());
            }
        }

        public static void PrintGrid(this IEnumerable<Position> grid, ITestOutputHelper outputHelper, string blankOrZeroChar = ".")
        {
            var maxX = grid.Select(xy => xy.x).Max();
            var maxY = grid.Select(xy => xy.y).Max();

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j <= maxY; j++)
            {
                sb.Clear();
                for (int i = 0; i <= maxX; i++)
                {
                    var pos = grid.FirstOrDefault(xy => xy.x == i && xy.y == j);
                    if (pos != null)
                    {
                        sb.Append("#");
                    }
                    else
                    {
                        sb.Append(blankOrZeroChar);

                    }
                }
                outputHelper.WriteLine(sb.ToString());
            }
        }

    }
}
