using Runner._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public class Day6 : PuzzleBase<IEnumerable<string[]>>
    {
        public Day6() : base(6, 2020, "11", "6")
        {

        }
        public override IEnumerable<string[]> ParseInput(IEnumerable<string> input)
        {
            var allGroups = new List<string[]>();

            var currentGroup = new List<string>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    allGroups.Add(currentGroup.Distinct().ToArray());
                    currentGroup = new List<string>();
                    continue;
                }

                var lineSplit = line.Select(l => l.ToString());
                currentGroup.AddRange(lineSplit);
            }

            allGroups.Add(currentGroup.Distinct().ToArray());

            return allGroups;
        }

        protected override IEnumerable<string[]> ParseInputForSecondProblem(IEnumerable<string> input)
        {
            var allGroups = new List<string[]>();

            var currentGroup = new List<string>();
            int numberOfPeopleInGroup = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    allGroups.Add(currentGroup.GroupBy(g => g).Where(g => g.Count() == numberOfPeopleInGroup).Select(g => g.Key).ToArray());
                    currentGroup = new List<string>();
                    numberOfPeopleInGroup = 0;
                    continue;
                }
                numberOfPeopleInGroup++;

                var lineSplit = line.Select(l => l.ToString());
                currentGroup.AddRange(lineSplit);
            }

            allGroups.Add(currentGroup.GroupBy(g => g).Where(g => g.Count() == numberOfPeopleInGroup).Select(g => g.Key).ToArray());

            return allGroups;
        }

        public override string SolveProblem1(IEnumerable<string[]> input)
        {
            var result = input.Sum(r => r.Count());

            return result.ToString();
        }

        public override string SolveProblem2(IEnumerable<string[]> input)
        {
            var result = input.Sum(r => r.Count());

            //3370 too high
            return result.ToString();
        }
    }
}
