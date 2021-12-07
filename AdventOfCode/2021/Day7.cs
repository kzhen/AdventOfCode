using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TBC = System.Collections.Generic.IEnumerable<int>;

namespace AdventOfCode._2021
{


    public class Day7 : PuzzleBase<TBC>
    {
        public Day7() : base(7, 2021, "37", "168")
        {

        }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input.First().Split(",").Select(int.Parse);
        }

        public override string SolveProblem1(TBC input)
        {
            var minDistanceToTravel = int.MaxValue;

            foreach (var item in input)
            {
                var distance = 0;
                var otherCrabs = input.Where(pos => pos != item); //because pos == item means 0 fuel
                //var diff = Math.Abs(item )
                foreach (var crab in otherCrabs)
                {
                    var diff = Math.Abs(crab - item);
                    distance += diff;
                }
                if (distance < minDistanceToTravel)
                {
                    minDistanceToTravel = distance;
                }
            }

            return minDistanceToTravel.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            long minDistanceToTravel = long.MaxValue;

            var max = input.Max();
            var min = input.Min();

            for (int i = min; i < max; i++)
            {
                var item = i;
                var distance = 0;
                var otherCrabs = input.Where(pos => pos != item); //because pos == item means 0 fuel
                foreach (var crab in otherCrabs)
                {
                    distance += CalculateCumulativeDistance(crab, item);
                }
                if (distance < minDistanceToTravel)
                {
                    minDistanceToTravel = distance;
                }

            }

            return minDistanceToTravel.ToString();
        }

        //distance between = 16-11 = 5 crab is at 16
        private int CalculateCumulativeDistance(int crabPosition, int targetPosition)
        {
            var diff = Math.Abs(crabPosition - targetPosition);

            return ((1+ diff) * diff) /2;
        }
    }
}
