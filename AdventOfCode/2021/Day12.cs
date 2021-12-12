using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using TBC = AdventOfCode._2021.Graph;

namespace AdventOfCode._2021
{
    public record Node(string name, int visited)
    {
        public bool IsSmall => name.ToLower().Equals(name);
        public bool IsStart => name.Equals("start");
        public bool IsEnd => name.Equals("end");
    }

    public record GraphVector(Node a, Node b);

    public record Graph(List<Node> Nodes, List<GraphVector> Vectors);

    public class Day12 : PuzzleBase<TBC>
    {
        public Day12(ITestOutputHelper outputHelper) : base(12, 2021, "10", "", outputHelper) { }
        public override TBC ParseInput(IEnumerable<string> input)
        {
            var nodes = new List<Node>();
            var vectors = new List<GraphVector>();

            foreach (var line in input)
            {
                var split = line.Split("-");
                var a = new Node(split[0], 0);
                var b = new Node(split[1], 0);

                nodes.Add(a);
                nodes.Add(b);

                vectors.Add(new GraphVector(a, b));
            }

            return new Graph(nodes.Distinct().ToList(), vectors);
            //return input.Select(int.Parse);
        }

        public override string SolveProblem1(TBC input)
        {
            int distinctPaths = 0;

            foreach (var startingNode in input.Nodes.Where(n => n.IsStart))
            {
                var visited = new LinkedList<Node>();
                visited.AddFirst(startingNode);
                dfs(input, visited);
                distinctPaths += paths;

                //LinkedList<>
                //var paths = dfs()
            }


            return distinctPaths.ToString();
        }

        private void dfs(TBC input, LinkedList<Node> visited)
        {
            var neighbours = GetNeighbours(input.Vectors, visited.Last());

            foreach (var node in neighbours)
            {
                var hasBeenVisited = visited.Any(n => n == node && node.IsSmall);

                if (hasBeenVisited)
                {
                    continue;
                }

                if (node.IsEnd)
                {
                    //visited.AddLast(node);
                    OutputHelper.WriteLine("path...");
                    continue;
                }

                visited.AddLast(node);
                dfs(input, visited);
                visited.RemoveLast();
            }

            
        }

        private LinkedList<Node> GetNeighbours(List<GraphVector> vectors, Node node)
        {
            LinkedList<Node> neighbours = new LinkedList<Node>();

            var v1 = vectors.Where(a => a.a == node && !a.b.IsStart).Select(v => v.b);
            var v2 = vectors.Where(a => a.b == node && !a.a.IsStart).Select(v => v.a);

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

        private void dfs(TBC input, LinkedList<Node> visited, LinkedList<Node> visitedTwice)
        {
            var neighbours = GetNeighbours(input.Vectors, visited.Last());

            foreach (var node in neighbours)
            {
                var hasBeenVisited = visited.Any(n => n == node && node.IsSmall);
                var hasBeenVisitedTwice = visitedTwice.Any(n => n == node && node.IsSmall);


                if (hasBeenVisited && hasBeenVisitedTwice)
                {
                    continue;
                }

                if (node.IsEnd)
                {
                    //visited.AddLast(node);
                    OutputHelper.WriteLine("path...");
                    continue;
                }

                visited.AddLast(node);
                dfs(input, visited, visitedTwice);
                visited.RemoveLast();
            }
        }

        public override string SolveProblem2(TBC input)
        {
            int distinctPaths = 0;

            foreach (var startingNode in input.Nodes.Where(n => n.IsStart))
            {
                var visitedOnce = new LinkedList<Node>();
                var visitedTwice = new LinkedList<Node>();
                visitedOnce.AddFirst(startingNode);
                dfs(input, visitedOnce, visitedTwice);

                //LinkedList<>
                //var paths = dfs()
            }


            return distinctPaths.ToString();
        }
    }
}
