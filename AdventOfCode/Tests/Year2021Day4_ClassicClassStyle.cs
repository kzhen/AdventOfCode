using Runner._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Year2021Day4_ClassicClassStyle
    {
        private static List<List<string>> _cardRows = new List<List<string>> {
            new List<string> {"22","13","17","11","0"},
            new List<string> {"8","2","23","4","24"},
            new List<string> {"21","9","14","16","7"},
            new List<string> {"6","10","3","18","5"},
            new List<string> {"1","12","20","15","19"}
        };


        [Fact]
        public void Grid_HasAFullRow()
        {
            var bingoCard = GetBingoCard(_cardRows);
            var input = new List<int> { 22, 13, 17, 11, 0 };

            var hasFullRow = HasFullRowOrColumn(bingoCard, input);

            Assert.True(hasFullRow);
        }

        [Fact]
        public void Grid_HasAFullColumn()
        {
            var bingoCard = GetBingoCard(_cardRows);
            var input = new List<int> { 13, 2, 9, 10, 12 };

            var hasFullRow = HasFullRowOrColumn(bingoCard, input);

            Assert.True(hasFullRow);
        }

        private bool HasFullRowOrColumn(BingoCard bingoCard, List<int> input)
        {
            var winnerFound = false;
            for (int i = 0; i < input.Count; i++)
            {
                var justCalledNumber = input[i];

                    var coordinates = bingoCard.NumberMap[justCalledNumber].Split(",");
                    var column = int.Parse(coordinates[0]);
                    var row = int.Parse(coordinates[1]);
                    bingoCard.AllNumbers.Remove(justCalledNumber);

                    bingoCard.CompletedCard[column, row] = justCalledNumber;
                    if (CheckGrid(bingoCard, column, row))
                    {
                        winnerFound = true;
                        break;
                    }
            }

            return winnerFound;
        }

        private bool CheckGrid(BingoCard bingoCard, int column, int row)
        {
            //check column
            var countOfFilled = 0;
            for (int i = 0; i < 5; i++)
            {
                if (bingoCard.CompletedCard[column, i] >= 0)
                {
                    countOfFilled += 1;
                }

                if (countOfFilled == 5)
                {
                    return true;
                }
            }

            //check row
            countOfFilled = 0;
            for (int i = 0; i < 5; i++)
            {
                if (bingoCard.CompletedCard[i, row] >= 0)
                {
                    countOfFilled += 1;
                }

                if (countOfFilled == 5)
                {
                    return true;
                }
            }

            return false;
        }


        private static BingoCard GetBingoCard(List<List<string>> cardRows)
        {
            var bingoCard = new BingoCard
            {
                NumberMap = new Dictionary<int, string>(),
                AllNumbers = new List<int>(),
                CompletedCard = new[,] { { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 } }
            };

            for (int column = 0; column < 5; column++)
            {
                for (int row = 0; row < 5; row++)
                {
                    var b = int.Parse(cardRows[column][row]);
                    bingoCard.NumberMap.Add(b, $"{column},{row}");
                    bingoCard.AllNumbers.Add(b);
                }
            }

            return bingoCard;
        }
    }
}
