using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using TBC = System.Collections.Generic.IEnumerable<AdventOfCode._2021.Display>;

namespace AdventOfCode._2021
{
    public record Display(List<string> SignalPattersn, List<string> OutputValues);

    public class Day8 : PuzzleBase<TBC>
    {
        public Day8(ITestOutputHelper output) : base(8, 2021, "26", "61229", output) { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input.Select(
                line => line.Split(" | "))
                .Select(splt => new Display(splt[0].Split(" ").ToList(), splt[1].Split(" ").ToList()));
        }

        public override string SolveProblem1(TBC input)
        {
            int result = 0;

            foreach (var display in input)
            {
                result += DoesDisplayContainOneFourSevenOrEight(display.OutputValues);
            }

            return result.ToString();
        }

        private int DoesDisplayContainOneFourSevenOrEight(List<string> outputValues)
        {
            return outputValues.Count(val =>
                val.Length == one ||
                val.Length == four ||
                val.Length == seven ||
                val.Length == eight
               );
        }

        int one = 2;
        int four = 4;
        int seven = 3;
        int eight = 7;

        public override string SolveProblem2(TBC input)
        {
            long result = 0;

            foreach (var line in input)
            {
                var map = GetMappingDigit(line.SignalPattersn);

                for (int i = 0; i < 4; i++)
                {
                    var display = line.OutputValues[i];
                    var a = map.Single(m => m.Value.Length == display.Length && 
                            m.Value.ToArray().Intersect(display.ToArray()).Count() == display.Length).Key;

                    //1000
                    //100
                    //10
                    //1
                    result += (i == 0) ? a * 1000 : (i == 1) ? a * 100 : (i == 2) ? a * 10 : a;

                }
            }


            return result.ToString();
        }

        private Dictionary<int, string> GetMappingDigit(List<string> outputValues)
        {
            Dictionary<int, string> map = new Dictionary<int, string>();
            map[1] = outputValues.Single(m => m.Length == 2); //one
            map[4] = outputValues.Single(m => m.Length == 4); //four
            map[7] = outputValues.Single(m => m.Length == 3); //seven
            map[8] = outputValues.Single(m => m.Length == 7); //eight

            outputValues.Remove(map[1]);
            outputValues.Remove(map[4]);
            outputValues.Remove(map[7]);
            outputValues.Remove(map[8]);

            var three = outputValues.Single(m => m.Length == 5 && m.ToArray().Intersect(map[1].ToArray()).Count() == 2); //five
            map[3] = three;
            outputValues.Remove(map[3]);

            var five = outputValues.Single(m => m.Length == 5 && m.ToArray().Intersect(map[4].ToArray()).Count() == 3); //five
            map[5] = five;
            outputValues.Remove(map[5]);
            var two = outputValues.Single(m => m.Length == 5 && m.ToArray().Intersect(map[4].ToArray()).Count() == 2); //five
            map[2] = two;
            outputValues.Remove(map[2]);

            //six, nine and zero remain
            var nine = outputValues.Single(m => m.Length == 6 && m.ToArray().Intersect(map[4]).Count() == 4);
            map[9] = nine;
            outputValues.Remove(nine);

            var zero = outputValues.Single(m => m.Length == 6 && m.ToArray().Intersect(map[1]).Count() == 2);
            map[0] = zero;
            outputValues.Remove(zero);

            map[6] = outputValues.Single();

            return map;
        }
    }
}
