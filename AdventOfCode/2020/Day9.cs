using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AdventOfCode._2020
{
    public class Day9 : PuzzleBase<IEnumerable<long>>
    {
        public Day9(ITestOutputHelper output) : base(9, 2020, "127", "62", output) { }

        public override IEnumerable<long> ParseInput(IEnumerable<string> input)
        {
            return input.Select(long.Parse).ToList();
        }

        public override string SolveProblem1(IEnumerable<long> input)
        {
            //for the sample preamble length is 5
            int preambleLength = 25;
            int pos = 0;

            for (int i = 0; i < input.Count(); i++)
            {
                var preamble = input.Skip(i).Take(preambleLength);
                var nextNumber = input.Skip(i + preambleLength).First();

                if (!SumCanBeFoundInPreamble(preamble, nextNumber))
                {
                    return nextNumber.ToString(); ;
                }

                
            }

            return "oooops";
        }

        private bool SumCanBeFoundInPreamble(IEnumerable<long> preamble, long nextNumber)
        {
            if (preamble.Contains(nextNumber))
            {
                throw new InvalidOperationException("");
            }

            var found = preamble
                .Where(a => a != nextNumber)
                .Any(a => 
                    preamble.Where(b => b != nextNumber).Any(b => (a + b) == nextNumber));

            return found;
        }

        public override string SolveProblem2(IEnumerable<long> input)
        {

            int preambleLength = 25;
            long invalidNumber = 0;

            for (int i = 0; i < input.Count(); i++)
            {
                var preamble = input.Skip(i).Take(preambleLength);
                var nextNumber = input.Skip(i + preambleLength).First();

                if (!SumCanBeFoundInPreamble(preamble, nextNumber))
                {
                    invalidNumber = nextNumber;
                    break;
                }
            }

            if (invalidNumber == 0)
            {
                throw new InvalidOperationException("");
            }

            //long invalidNumber2 = 32321523;

            (long min, long max) = GetContiguous2(input.ToList(), invalidNumber);

            //4794981
            //4203199
            //4203199
            //4203199 too low
            //4623811 too low
            //4623811 too low
            return (min + max).ToString();
        }

        private (long min, long max) GetContiguous2(List<long> input, long targetNumber)
        {
            for (int i = 0; i < input.Count; i++)
            {
                long total = 0;
                var found = input.Skip(i).Select((val, idx) => new { RunningTotal = total += val, LastIdx = idx })
                    .Where(s => s.RunningTotal == targetNumber).ToList();

                if (found.Count() == 1)
                {
                    var range = input.Skip(i).Take(found.First().LastIdx+1);
                    var sum = range.Sum();
                    var min = range.Min();
                    var max = range.Max();

                    return (min, max);
                    
                    //return (1, 2);
                }
            }

            return (0, 0);
        }

        private (long min, long max) GetContiguous(List<long> input, long invalidNumber)
        {
            for (int i = 0; i < input.Count(); i++)
            {
                long min = input[i];
                long total = 0;
                var found = input.Skip(i).Select(s => new { RunningTotal = (total += s), LastElement = s }).Where(s => s.RunningTotal == invalidNumber).ToList();

                if (found.Count() == 1)
                {
                    var lastElement = found.First().LastElement;
                    var range = input.SkipWhile(i => i != min).TakeWhile(a => a != lastElement);
                    var min2 = range.Min();
                    var max = range.Max();

                    //var sum = found.Sum(a => a.RunningTotal);
                    var runningtot = found.First().RunningTotal;

                    var res = min2 + max;

                    return (range.Min(), range.Max());
                }



                //int sum = 0;
                ////input.Skip(i).TakeWhile(n => (n + sum))
                //long total = 0;
                //var runningTotals = (input.Skip(i).Select(x => total += x))
                //if (runningTotals)
                //{
                //    return (5, 5);
                //}

            }

            return (-1, -1);
        }
    }
}
