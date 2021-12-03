using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runner._2021
{

    public class Day1 : PuzzleBase<IEnumerable<int>>
    {
        public Day1() : base(day: 1, 2021, sampleProblemSolutionOne: "7", sampleProblemSolutionTwo: "5") { }

        public override IEnumerable<int> ParseInput(IEnumerable<string> input)
        {
            return input.Select(i => int.Parse(i));
        }

        public override string SolveProblem1(IEnumerable<int> input)
        {
            var lines = input.ToList();

            int increments = 0;
            int currentVal = lines[0];

            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[i] > currentVal)
                {
                    increments++;
                }
                currentVal = lines[i];
            }

            return increments.ToString();
        }

        public override string SolveProblem2(IEnumerable<int> input)
        {
            var multiples = Window(input, 3).Select(v => v.Sum()).ToList();

            int increments = 0;
            int previous = multiples[0];
            for (int i = 1; i < multiples.Count(); i++)
            {
                if (multiples[i] > previous)
                {
                    increments++;
                }

                previous = multiples[i];
            }

            return increments.ToString();
        }

        #region Helpers
        //https://github.com/morelinq/MoreLINQ/blob/master/MoreLinq/Window.cs
        public static IEnumerable<IList<TSource>> Window<TSource>(IEnumerable<TSource> source, int size)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));

            return _(); IEnumerable<IList<TSource>> _()
            {
                using var iter = source.GetEnumerator();

                // generate the first window of items
                var window = new TSource[size];
                int i;
                for (i = 0; i < size && iter.MoveNext(); i++)
                    window[i] = iter.Current;

                if (i < size)
                    yield break;

                while (iter.MoveNext())
                {
                    // generate the next window by shifting forward by one item
                    // and do that before exposing the data
                    var newWindow = new TSource[size];
                    Array.Copy(window, 1, newWindow, 0, size - 1);
                    newWindow[size - 1] = iter.Current;

                    yield return window;
                    window = newWindow;
                }

                // return the last window.
                yield return window;
            }
        }
        #endregion
    }
}
