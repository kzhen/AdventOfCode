using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using TBC = System.Collections.Generic.IEnumerable<AdventOfCode._2021.GraphVector>;

namespace AdventOfCode._2021
{
    public record Node(string name, bool IsSmall)
    {
        public bool IsStart => name.Equals("start");
        public bool IsEnd => name.Equals("end");
    }

    public record GraphVector(Node From, Node To);

    public class Day12 : PuzzleBase<TBC>
    {
        public Day12(ITestOutputHelper outputHelper) : base(12, 2021, "10", "36", outputHelper) { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            return input.Select(l => l.Split("-")).Select(m => new GraphVector(new Node(m[0], m[0].ToLower().Equals(m[0])), new Node(m[1], m[1].ToLower().Equals(m[1]))));
        }

        public override string SolveProblem1(TBC input)
        {
            var startingNode = new Node("start", true);

            var visited = new LinkedList<Node>();
            visited.AddFirst(startingNode);
            var distinctPaths = Dfs(input, visited, 0, false);
            
            return distinctPaths.ToString();
        }

        private int Dfs(TBC input, LinkedList<Node> visited, int pathsToEndFoundSoFar, bool isPart2)
        {
            var pathsToEnd = 0;
            var neighbours = GetNeighbours(input, visited.Last());

            foreach (var node in neighbours)
            {
                var hasBeenVisited = visited.Any(n => n == node && node.IsSmall);

                if (hasBeenVisited)
                {
                    if (isPart2)
                    {
                        var anyVisitedTwice = visited.Where(m => m.IsSmall).GroupBy(m => m.name).Any(m => m.Count() == 2);

                        if (anyVisitedTwice)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                if (node.IsEnd)
                {
                    pathsToEnd++;
                    //printPath(visited, node);
                    continue;
                }

                visited.AddLast(node);
                var pathsFound = Dfs(input, visited, pathsToEndFoundSoFar, isPart2);
                pathsToEnd += pathsFound;
                visited.RemoveLast();
            }

            return pathsToEndFoundSoFar + pathsToEnd;
        }
        private void printPath(LinkedList<Node> visited, Node lastVisited)
        {
            visited.AddLast(lastVisited);
            StringBuilder cb = new StringBuilder();
            foreach (var node in visited)
            {
                cb.Append(node.name + " ");
            }
            Console.WriteLine(cb.ToString());
            visited.RemoveLast();
        }

        private LinkedList<Node> GetNeighbours(IEnumerable<GraphVector> vectors, Node node)
        {
            LinkedList<Node> neighbours = new LinkedList<Node>();

            var v1 = vectors.Where(a => a.From == node && !a.To.IsStart).Select(v => v.To);
            var v2 = vectors.Where(a => a.To == node && !a.From.IsStart).Select(v => v.From);

            foreach (var item in v1)
            {
                neighbours.AddLast(item);
            }

            foreach (var item in v2)
            {
                neighbours.AddLast(item);
            }

            return neighbours;
        }

        public override string SolveProblem2(TBC input)
        {
            var startingNode = new Node("start", true);

            var visited = new LinkedList<Node>();
            visited.AddFirst(startingNode);
            var distinctPaths = Dfs(input, visited, 0, true);

            return distinctPaths.ToString();
        }
    }
}
