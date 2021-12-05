using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AdventOfCode
{
    public abstract class PuzzleBase<T>
    {
        public PuzzleBase(int day, int year, string sampleProblemSolutionOne, string sampleProblemSolutionTwo)
        {
            this.Day = day;
            this.Year = year;
            this.SampleProblemSolutionOne = sampleProblemSolutionOne;
            this.SampleProblemSolutionTwo = sampleProblemSolutionTwo;
        }

        public int Day { get; }
        public int Year { get; }
        public string SampleProblemSolutionOne { get; }
        public string SampleProblemSolutionTwo { get; }

        private IEnumerable<string> ReadInput(bool useSampleData)
        {
            var fileName = (useSampleData) ? $"Day{Day}-sample" : $"Day{Day}";
            string filePath = $"./input/{Year}/{fileName}.txt";

            var input = File.ReadAllLines(filePath);

            return input;
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestProblem1(bool isSampleData)
        {
            var input = ReadInput(isSampleData);
            var parsed = ParseInput(input);

            var solution = SolveProblem1(parsed);

            if (isSampleData)
            {
                Assert.Equal(SampleProblemSolutionOne, solution);
            }
            else
            {
                Console.WriteLine($"The solution is: {solution}");
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestProblem2(bool isSampleData)
        {
            var input = ReadInput(isSampleData);
            var parsed = ParseInputForSecondProblem(input);

            var solution = SolveProblem2(parsed);

            if (isSampleData)
            {
                Assert.Equal(SampleProblemSolutionTwo, solution);
            }
            else
            {
                Console.WriteLine($"The solution is: {solution}");
            }
        }

        public abstract string SolveProblem1(T input);

        public abstract string SolveProblem2(T input);

        public abstract T ParseInput(IEnumerable<string> input);
        protected virtual T ParseInputForSecondProblem(IEnumerable<string> input)
        {
            return ParseInput(input);
        }
    }
}
