using AdventOfCode.Helpers;
using Runner._2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Tests
{
    /// <summary>
    /// This is directly inspired by my co-worker Alex, you can see his full solution in GitLab
    /// https://gitlab.com/amdavies/advent-of-code/
    /// </summary>
    public class Year2021Day4_ModernRecordStyle
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

            var hasFullColumn = HasFullRowOrColumn(bingoCard, input);

            Assert.True(hasFullColumn);
        }

        private bool HasFullRowOrColumn(BingoBoard bingoBoard, List<int> usedNumbers)
        {
            for (int i = 0; i < 5; i++)
            {
                int xCount = bingoBoard.grid.Count(kvp => kvp.Key.x == i && usedNumbers.Contains(kvp.Value));
                int yCount = bingoBoard.grid.Count(kvp => kvp.Key.y == i && usedNumbers.Contains(kvp.Value));

                if (xCount is not 5 && yCount is not 5)
                    continue;

                return true;
            }
            return false;
        }

        private static BingoBoard GetBingoCard(List<List<string>> cardRows)
        {
            Dictionary<Position, int> grid = new Dictionary<Position, int>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    grid.Add(new Position(i, j), int.Parse(cardRows[i][j]));
                }
            }

            return new(grid);
        }
    }
    public record BingoBoard(Dictionary<Position, int> grid);
}
