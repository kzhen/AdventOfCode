using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AdventOfCode._2020
{
    public class Day25 : PuzzleBase<long[]>
    {
        public Day25(ITestOutputHelper output) : base(25, 2020, "14897079", "", output)
        {

        }

        public override long[] ParseInput(IEnumerable<string> input)
        {
            return input.Select(long.Parse).ToArray();
        }

        public override string SolveProblem1(long[] input)
        {
            var publicKeyCard = input[0];
            var publicKeyDoor = input[1];

            var cardSubjectNumber = 7;
            var doorSubjectNumber = 7;

            int doorLoopSize = GetLoopSize(publicKeyDoor);
            int cardLoopSize = GetLoopSize(publicKeyCard);



            //long cardEncryptionKey = Transform(publicKeyCard, doorLoopSize);
            long doorEncryptionKey = Transform(publicKeyDoor, cardLoopSize);

            return doorEncryptionKey.ToString(); ;
        }

        private long Transform(long subjectNumber, int cardLoopSize)
        {
            long result = 1;
            for (int i = 0; i < cardLoopSize; i++)
            {
                var tempResult = result * subjectNumber;
                result = (tempResult % 20201227);
            }

            return result;
        }

        public int TransformUntil(long subjectNumber, long target)
        {
            long result = 1;
            int loopSize = 0;
            do
            {
                var tempResult = result * subjectNumber;
                result = (tempResult % 20201227);

                loopSize++;
            } while (result != target);


            return loopSize;
        }

        private int GetLoopSize(long publicKeyDoor)
        {
            var loopSize = TransformUntil(7, publicKeyDoor);

            return loopSize;

            for (int i = 1; i < 100000; i++)
            {
                var res = Transform(7, i);
                if (res == publicKeyDoor)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("oops");
        }

        public override string SolveProblem2(long[] input)
        {
            throw new NotImplementedException();
        }
    }
}
