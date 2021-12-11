using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AdventOfCode._2020
{
    public class Day5 : PuzzleBase<IEnumerable<string>>
    {
        int rowsOnPlane = 128;
        int columnsOnPlane = 8;

        public Day5(ITestOutputHelper output) : base(5, 2020, "820", "", output) { }
        public override IEnumerable<string> ParseInput(IEnumerable<string> input)
        {
            return input;
        }

        public override string SolveProblem1(IEnumerable<string> input)
        {
            int maxSeatId = 0;

            foreach (var boardingPass in input)
            {
                int rowNumber = 0;
                int columnNumber = 0;

                int rowLower = 0;
                int rowUpper = rowsOnPlane - 1;


                for (int i = 0; i < 7; i++)
                {
                    var indicator = boardingPass[i];
                    if (indicator == 'F')
                    {
                        rowUpper -= ((rowUpper - rowLower)+1)/2;//(rowsOnPlane / ((i+1) * 2));
                    }
                    else if (indicator == 'B')
                    {
                        rowLower += ((rowUpper - rowLower)+1)/2;
                    }
                }

                rowNumber = rowUpper;

                int columnLower = 0;
                int columnUpper = columnsOnPlane - 1;

                for (int i = 0; i < 3; i++)
                {
                    var indicator = boardingPass[7+i];
                    if (indicator == 'L')
                    {
                        columnUpper -= ((columnUpper - columnLower)+1)/2;
                    }
                    else if (indicator == 'R')
                    {
                        columnLower += ((columnUpper - columnLower)+1)/2;
                    }
                }
                columnNumber = columnUpper;

                var result = (rowNumber * 8) + columnNumber;

                if (result > maxSeatId)
                {
                    maxSeatId = result;
                }
            }


            return maxSeatId.ToString();
        }

        public override string SolveProblem2(IEnumerable<string> input)
        {
            var seats = new string[128, 8];
            var seats2 = new Dictionary<Position, string>();

            foreach (var boardingPass in input)
            {
                int rowNumber = 0;
                int columnNumber = 0;

                int rowLower = 0;
                int rowUpper = rowsOnPlane - 1;


                for (int i = 0; i < 7; i++)
                {
                    var indicator = boardingPass[i];
                    if (indicator == 'F')
                    {
                        rowUpper -= ((rowUpper - rowLower)+1)/2;//(rowsOnPlane / ((i+1) * 2));
                    }
                    else if (indicator == 'B')
                    {
                        rowLower += ((rowUpper - rowLower)+1)/2;
                    }
                }

                rowNumber = rowUpper;

                int columnLower = 0;
                int columnUpper = columnsOnPlane - 1;

                for (int i = 0; i < 3; i++)
                {
                    var indicator = boardingPass[7+i];
                    if (indicator == 'L')
                    {
                        columnUpper -= ((columnUpper - columnLower)+1)/2;
                    }
                    else if (indicator == 'R')
                    {
                        columnLower += ((columnUpper - columnLower)+1)/2;
                    }
                }
                columnNumber = columnUpper;

                seats[rowNumber, columnNumber] = "Reserved";
                seats2.Add(new Position(rowNumber, columnNumber), "Reserved");
            }

            bool foundReservedSeatYet = false;
            var foundWinner = false;
            var rowNumber2 = 0; //79;
            var columnNumber2 = 0; //4;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (string.IsNullOrWhiteSpace(seats[i, j]))
                    {
                        if (foundReservedSeatYet)
                        {
                            rowNumber2 = i;
                            columnNumber2 = j;
                            foundWinner = true;
                            break;
                        }
                        continue;
                    }
                    foundReservedSeatYet = true;
                }
                if (foundWinner)
                {
                    break;
                }
            }


            var seatId = (rowNumber2 * 8) + columnNumber2;

            return seatId.ToString();
        }
    }
}
