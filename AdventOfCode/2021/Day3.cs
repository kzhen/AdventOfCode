using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day3 : PuzzleBase<IEnumerable<char[]>>
    {
        public Day3() : base(3, 2021, "198", "230") { }

        public override IEnumerable<char[]> ParseInput(IEnumerable<string> input)
        {
            var output = input.Select(m => m.ToArray());

            return output;
        }

        public override string SolveProblem1(IEnumerable<char[]> input)
        {
            var result = input
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();

            string[] gamma = new string[input.First().Length];
            string[] epsilon = new string[input.First().Length];

            for (int i = 0; i < result.Count; i++)
            {
                var zeros = result[i].Count(f => int.Parse(f.ToString()) == 0);//.Dump();
                var ones = result[i].Count(f => int.Parse(f.ToString()) == 1);//.Dump();

                if (zeros > ones)
                {
                    gamma[i] = "0";
                    epsilon[i] = "1";
                }
                else
                {
                    gamma[i] = "1";
                    epsilon[i] = "0";
                }
            }

            var a = Convert.ToInt32(string.Join("", gamma), 2);
            var b = Convert.ToInt32(string.Join("", epsilon), 2);

            var res = a * b;

            return res.ToString();
        }

        public override string SolveProblem2(IEnumerable<char[]> input)
        {
            var raw2 = input.Select(m => string.Join("", m));
            var raw1 = input.Select(m => string.Join("", m));
            int length = raw2.First().Length;

            string searchArg = "";
            int bitToSearch = 0;


            var c02scruber = "";
            var oxygenGeneratorRating = "";

            do
            {
                raw2 = raw2.Where(r => string.Join("", r).StartsWith(searchArg));

                if (raw2.Count() == 1)
                {
                    //raw2;
                    oxygenGeneratorRating = raw2.First();
                    break;
                }

                var result = raw2
                    .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                    .GroupBy(i => i.index, i => i.item)
                    .Select(g => g.ToList())
                    .ToList();

                var zeros = result[bitToSearch].Count(f => int.Parse(f.ToString()) == 0);//.Dump();
                var ones = result[bitToSearch].Count(f => int.Parse(f.ToString()) == 1);//.Dump();

                if (ones >= zeros)
                {
                    searchArg += "1";
                }
                else
                {
                    searchArg += "0";
                }

                bitToSearch += 1;
            } while (bitToSearch <= length);

            searchArg = "";
            bitToSearch = 0;
            do
            {
                raw1 = raw1.Where(r => string.Join("", r).StartsWith(searchArg));

                if (raw1.Count() == 1)
                {
                    c02scruber = raw1.First();
                    break;
                }

                var result = raw1
                    .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                    .GroupBy(i => i.index, i => i.item)
                    .Select(g => g.ToList())
                    .ToList();

                var zeros = result[bitToSearch].Count(f => int.Parse(f.ToString()) == 0);//.Dump();
                var ones = result[bitToSearch].Count(f => int.Parse(f.ToString()) == 1);//.Dump();

                if (ones >= zeros)
                {
                    searchArg += "0";
                }
                else
                {
                    searchArg += "1";
                }

                bitToSearch += 1;
            } while (bitToSearch < length);

            var a = Convert.ToInt32(string.Join("", c02scruber), 2);
            var b = Convert.ToInt32(string.Join("", oxygenGeneratorRating), 2);


            var res = a * b;

            return res.ToString();
        }
    }
}
