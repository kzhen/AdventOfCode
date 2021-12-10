using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TBC = System.Collections.Generic.IEnumerable<string>;

namespace AdventOfCode._2021
{
    public class Day10 : PuzzleBase<TBC>
    {
        private Dictionary<char, int> lookup = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
             { '(', 3 },
            { '[', 57 },
            { '{', 1197 },
            { '<', 25137 }
        };

        private Dictionary<char, char> map = new Dictionary<char, char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        private char[] openers = new char[] { '(', '[', '{', '<' };


        public Day10() : base(10, 2021, "26397", "288957") { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input;
        }

        public override string SolveProblem1(TBC input)
        {
            Dictionary<char, long> score = new Dictionary<char, long>();
            foreach (var line in input) //only complete lines
            {
                Stack<char> lineSoFar = new Stack<char>();

                foreach (var character in line)
                {
                    if (lineSoFar.Count == 0)
                    {
                        lineSoFar.Push(character);
                        continue;
                    }

                    if (openers.Contains(character))
                    {
                        lineSoFar.Push(character);
                        continue;
                    }

                    if (map[lineSoFar.Peek()].Equals(character))
                    {
                        lineSoFar.Pop();
                    }
                    else
                    {
                        //character is the illegal char!
                        var expected = lookup[character];

                        if (score.ContainsKey(character))
                        {
                            score[character] += expected;
                        }
                        else
                        {
                            score.Add(character, expected);
                        }

                        break;
                    }
                }
            }

            long result = 0;
            foreach (var item in score)
            {
                result += item.Value;
            }

            return result.ToString();
        }

        public override string SolveProblem2(TBC input)
        {
            var scores = new List<long>();

            foreach (var line in input) //only complete lines
            {
                bool corrupt = false;
                Stack<char> lineSoFar = new Stack<char>();

                foreach (var character in line)
                {
                    if (lineSoFar.Count == 0)
                    {
                        lineSoFar.Push(character);
                        continue;
                    }

                    if (openers.Contains(character))
                    {
                        lineSoFar.Push(character);
                        continue;
                    }

                    if (map[lineSoFar.Peek()].Equals(character))
                    {
                        lineSoFar.Pop();
                    }
                    else
                    {
                        corrupt = true;
                        break;
                    }
                }

                if (corrupt)
                {
                    continue;
                }

                var newMap = new Dictionary<char, int>()
                {
                    { ')', 1 },
                    { ']', 2 },
                    { '}', 3 },
                    { '>', 4 },
                };

                if (lineSoFar.Count > 0)
                {
                    long score = 0;
                    List<char> commpletionString = new List<char>();
                    do
                    {
                        var top = lineSoFar.Pop();
                        var shouldActuallyBe = map[top];
                        var value = newMap[shouldActuallyBe];

                        score = ((score * 5)) + value;

                        //lineSoFar.Push(shouldActuallyBe);
                        commpletionString.Add(shouldActuallyBe);


                    } while (lineSoFar.Count > 0);

                    scores.Add(score);
                }
            }

            var ordered = scores.OrderBy(i => i).ToList();

            var count = ordered.Count();

            var a = Math.DivRem(count, 2, out var res);

            var middle = a + res;


            //237311081 too low


            return ordered.ToList()[middle - 1].ToString();
        }
    }
}
