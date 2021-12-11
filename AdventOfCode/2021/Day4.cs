using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Abstractions;
using TBC = AdventOfCode._2021.BingoGame;

namespace AdventOfCode._2021
{
    public class BingoGame
    {
        public List<int> Numbers { get; set; }
        public List<BingoCard> Cards { get; set; }
    }
    public class BingoCard
    {
        public int[,] Card { get; set; }
        public int[,] CompletedCard { get; set; }
        public Dictionary<int, string> NumberMap { get; set; }
        public List<int> AllNumbers { get; set; }
        public bool HasCompletedRowOrColumn { get; internal set; }
    }

    public class Day4 : PuzzleBase<TBC>
    {
        public Day4(ITestOutputHelper output) : base(4, 2021, "4512", "1924", output) { }

        public override TBC ParseInput(IEnumerable<string> input)
        {
            var game = new BingoGame()
            {
                Cards = new List<BingoCard>()
            };

            var baseCard = new[,] { { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 } };

            var line1 = input.First().Split(",");

            game.Numbers = line1.Select(m => int.Parse(m)).ToList();

            var numberOfCards = input.Skip(2).Count() / 5;

            for (int i = 0; i < numberOfCards; i++)
            {
                var bingoCard = new BingoCard
                {
                    Card = new int[5, 5],
                    NumberMap = new Dictionary<int, string>(),
                    AllNumbers = new List<int>(),
                    CompletedCard = new[,] { { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 }, { -1, -1, -1, -1, -1 } }
                };
                //var cardRows = input.Skip(2 + (i * 6)).Take(5).ToList();
                var cardRows = input.Skip(2 + (i * 6)).Take(5).Select(i => i.Split(" ")).ToList().Select(r => r.ToList().Where(r => !string.IsNullOrWhiteSpace(r)).ToList()).ToList();

                if (cardRows.Count == 0)
                {
                    break;
                }

                for (int column = 0; column < 5; column++)
                {
                    for (int row = 0; row < 5; row++)
                    {
                        var b = int.Parse(cardRows[column][row]);
                        bingoCard.Card[column, row] = b;
                        bingoCard.NumberMap.Add(b, $"{column},{row}");
                        bingoCard.AllNumbers.Add(b);
                    }

                }
                game.Cards.Add(bingoCard);
            }



            return game;
        }

        public override string SolveProblem1(TBC input)
        {

            var winnerFound = false;
            var totalScore = 0;
            for (int i = 0; i < input.Numbers.Count; i++)
            {


                var justCalledNumber = input.Numbers[i];
                foreach (var bingoCard in input.Cards.Where(c => c.NumberMap.ContainsKey(justCalledNumber)))
                {
                    var coordinates = bingoCard.NumberMap[justCalledNumber].Split(",");
                    var column = int.Parse(coordinates[0]);
                    var row = int.Parse(coordinates[1]);
                    bingoCard.AllNumbers.Remove(justCalledNumber);

                    bingoCard.CompletedCard[column, row] = justCalledNumber;
                    if (HasFullRowOrColumn(bingoCard, column, row))
                    {
                        winnerFound = true;
                        var remainingNumbers = bingoCard.AllNumbers.Sum();
                        totalScore = remainingNumbers * justCalledNumber;
                        break;
                    }
                }

                if (winnerFound)
                {
                    break;
                }
            }



            return totalScore.ToString();
        }

        private bool HasFullRowOrColumn(BingoCard bingoCard, int column, int row)
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

        public override string SolveProblem2(TBC input)
        {
            var totalScore = 0;
            var allCalledNumbersSoFar = new List<int>();
            for (int i = 0; i < input.Numbers.Count; i++)
            {
                var winnerFound = false;

                var unfinishedCardCount = input.Cards.Where(c => !c.HasCompletedRowOrColumn).Count();
                var justCalledNumber = input.Numbers[i];
                var cardsToCheck = input.Cards.Where(c => !c.HasCompletedRowOrColumn).Where(c => c.NumberMap.ContainsKey(justCalledNumber));

                allCalledNumbersSoFar.Add(justCalledNumber);

                var lastCheckedCard = default(BingoCard);
                foreach (var bingoCard in cardsToCheck)
                {
                    lastCheckedCard = bingoCard;
                    var coordinates = bingoCard.NumberMap[justCalledNumber].Split(",");
                    var column = int.Parse(coordinates[0]);
                    var row = int.Parse(coordinates[1]);
                    bingoCard.AllNumbers.Remove(justCalledNumber);

                    bingoCard.CompletedCard[column, row] = justCalledNumber;
                    if (HasFullRowOrColumn(bingoCard, column, row))
                    {
                        winnerFound = true;
                        bingoCard.HasCompletedRowOrColumn = true;


                    }
                }

                if (unfinishedCardCount == 1 && winnerFound)
                {
                    //13717 to low
                    var remainingNumbers = lastCheckedCard.AllNumbers.Sum();
                    totalScore = (remainingNumbers) * justCalledNumber;
                    break;
                }
            }



            return totalScore.ToString();
        }
    }
}
