using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public record Instruction(string opCode, int val);
    public class Day8 : PuzzleBase<Dictionary<int, Instruction>>
    {
        public Day8() : base(8, 2020, "5", "8")
        {

        }
        public override Dictionary<int, Instruction> ParseInput(IEnumerable<string> input)
        {
            var res = input.Select((i, j) => new { Pos = j, Inst = new Instruction(i.Split(" ")[0], int.Parse(i.Split(" ")[1])) });
            return res.ToDictionary(k => k.Pos, v => v.Inst);
        }

        public override string SolveProblem1(Dictionary<int, Instruction> input)
        {
            int pos = 0;
            int accumulator = 0;
            List<int> visitedLines = new List<int>();
            do
            {
                if (visitedLines.Contains(pos))
                {
                    break;
                }

                var instruction = input[pos];
                visitedLines.Add(pos);

                switch (instruction.opCode)
                {
                    case "acc":
                        accumulator += instruction.val;
                        pos++;
                        break;
                    case "nop":
                        pos++;
                        break;
                    case "jmp":
                        pos += instruction.val;
                        break;
                    default:
                        break;
                }



            } while (true);

            //9177528 too high
            return accumulator.ToString();
        }

        public override string SolveProblem2(Dictionary<int, Instruction> input)
        {
            var exitOnLine = input.Count;

            foreach (var nopOrJump in input.Where(kvp => kvp.Value.opCode.Equals("nop") || kvp.Value.opCode.Equals("jmp")))
            {
                int pos = 0;
                int accumulator = 0;
                List<int> visitedLines = new List<int>();

                do
                {
                    if (visitedLines.Contains(pos))
                    {
                        break;
                    }

                    var currentInstruction = input[pos];

                    if (pos == nopOrJump.Key)
                    {
                        //switch
                        var invertedOpCode = currentInstruction.opCode.Equals("jmp") ? "nop" : "jmp";
                        currentInstruction = currentInstruction with { opCode = invertedOpCode };
                    }

                    visitedLines.Add(pos);

                    switch (currentInstruction.opCode)
                    {
                        case "acc":
                            accumulator += currentInstruction.val;
                            pos++;
                            break;
                        case "nop":
                            pos++;
                            break;
                        case "jmp":
                            pos += currentInstruction.val;
                            break;
                        default:
                            break;
                    }


                    if (pos == exitOnLine)
                    {
                        return accumulator.ToString();
                    }

                } while (true);

            }

            return "ooops".ToString();
        }
    }
}
