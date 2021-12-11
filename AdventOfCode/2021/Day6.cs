using AdventOfCode.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit.Abstractions;
using TBC = System.Collections.Generic.Dictionary<int, System.Numerics.BigInteger>;

namespace AdventOfCode._2021
{

    public class Day6 : PuzzleBase<TBC>
    {
        public Day6(ITestOutputHelper output) : base(6, 2021, "5934", "26984457539", output) { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input.First().Split(",")
                .GroupBy(l => l)
                .ToDictionary(key => int.Parse(key.Key), val => new BigInteger(val.Count()));
        }

        public override string SolveProblem1(TBC input)
        {
            int daysToSimulate = 80;
            BigInteger result = CalculateNumberOfFishAfterXDays(ref input, daysToSimulate);

            return result.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            int daysToSimulate = 256;
            BigInteger result = CalculateNumberOfFishAfterXDays(ref input, daysToSimulate);

            return result.ToString();
        }

        private static BigInteger CalculateNumberOfFishAfterXDays(ref TBC input, int maxDays)
        {
            int daysRun = 0;
            do
            {
                var nextDay = new TBC();
                var keys = input.Keys;

                //key is the number of days remaining until respawn
                //value is the number of fish
                foreach (var key in keys)
                {
                    if (key != 0)
                    {
                        nextDay.GetKeyVauleOrSetDefaultValue(key - 1, 0);
                        nextDay[key - 1] += input[key];
                    }
                    else
                    {
                        nextDay.GetKeyVauleOrSetDefaultValue(8, 0);
                        nextDay[8] += input[key];

                        nextDay.GetKeyVauleOrSetDefaultValue(6, 0);
                        nextDay[6] += input[key];
                    }
                }

                input = nextDay;
            } while (++daysRun < maxDays);

            var result = input.Select(kvp => kvp.Value).SumBigInteger();

            return result;
        }
    }
}
