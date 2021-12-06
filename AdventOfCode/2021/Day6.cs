using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

//using TBC = System.Collections.Generic.Dictionary<int, int>;
using TBC = System.Collections.Generic.Dictionary<int, System.Numerics.BigInteger>;

namespace AdventOfCode._2021
{

    public class Day6 : PuzzleBase<TBC>
    {
        public Day6() : base(6, 2021, "5934", "26984457539") { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input.First().Split(",")
                .GroupBy(l => l)
                //.ToDictionary(key => int.Parse(key.Key), val => val.Count())
                .ToDictionary(key => int.Parse(key.Key), val => new BigInteger(val.Count()))
                //.Select(m => new LanternFish { InternalTimer = int.Parse(m) })
                //.ToDictionary(m => m.InternalTimer, k => k)
                ;
        }

        public override string SolveProblem1(TBC input)
        {
            int maxDays = 80;
            BigInteger result = CalculateNumberOfFishAfterXDays(ref input, maxDays);

            return result.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            int maxDays = 256;
            BigInteger result = CalculateNumberOfFishAfterXDays(ref input, maxDays);

            return result.ToString();
        }

        private static BigInteger CalculateNumberOfFishAfterXDays(ref TBC input, int maxDays)
        {
            int daysRun = 0;
            do
            {
                var nextDay = new Dictionary<int, BigInteger>();
                var keys = input.Keys;

                //key is the number of days remaining until respawn
                //value is the number of fish
                foreach (var key in keys)
                {
                    if (key != 0)
                    {
                        var currentvalue = nextDay.GetKeyVauleOrSetDefaultValue(key - 1, 0);
                        nextDay[key - 1] += input[key];
                    }
                    else
                    {
                        var currentvalue = nextDay.GetKeyVauleOrSetDefaultValue(8, 0);
                        nextDay[8] += input[key];

                        var currentvalue2 = nextDay.GetKeyVauleOrSetDefaultValue(6, 0);
                        nextDay[6] += input[key];
                    }
                }

                input = nextDay;

                daysRun++;
            } while (daysRun < maxDays);

            BigInteger result = 0;

            foreach (var item in input)
            {
                result = BigInteger.Add(result, item.Value);
            }

            

            return result;
        }
    }
}
