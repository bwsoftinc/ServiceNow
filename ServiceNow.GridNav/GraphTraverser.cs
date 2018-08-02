using System.Collections.Generic;
using System.Linq;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Traverses the graph to find a path with the maximum sum of vertex values
    /// Each vertex can only be visited once during a path traveral
    /// This lends the traversal method to that of a depth first search
    /// </summary>
    public class TreeTraverser
    {
        /// <summary>
        /// Explores the tree recursively using dfs method and keeps track of the max sum of node values along any path between start and end nodes
        /// Remarks: this method is slow for modestly sized graphs, but good to validate the the results of the other algo in testing
        /// </summary>
        /// <param name="node">The starting point node, with rest of graph attached</param>
        /// <returns>the largest sum of vertext values along the path to the final node indicated to be the target node</returns>
        public static (long sum, bool terminal) BrutForceMaxPathDFS(GraphNode startNode, GraphNode endNode, long sum)
        {
            startNode.Visited = true;
            var total = sum + startNode.Value;

            if (ReferenceEquals(startNode, endNode))
                return (total, true);

            var terminal = false;
            var maxSum = long.MinValue;

            foreach (var n in startNode.AdjacentNodes)
            {
                if (n.Visited)
                    continue;

                var branchSum = BrutForceMaxPathDFS(n, endNode, total);

                if (branchSum.terminal && branchSum.sum > maxSum)
                {
                    maxSum = branchSum.sum;
                    terminal = true;
                }

                n.Visited = false;
            }

            return (maxSum, terminal);
        }

        /// <summary>
        /// Sorts a graph topologically onto a stack based on if it has been visited already
        /// </summary>
        /// <param name="node">the node with verticies</param>
        /// <param name="stack">the stack to put the sorted results</param>
        private static void SortGraphTopologically(GraphNode node, Stack<GraphNode> stack)
        {
            node.Visited = true;

            foreach(var n in node.AdjacentNodes)
                if (!n.Visited)
                    SortGraphTopologically(n, stack);

            stack.Push(node);
        }

        /// <summary>
        /// Make use of "Longest Path in a Directed Acyclic Graph" algorithm to build sort the nodes of the graph topologically
        /// into a stack and then iterates the stack to determine the max distance of each node from the start node
        /// </summary>
        /// <param name="startNode">The node to start the traversal from</param>
        /// <param name="endNode">The node at which to end</param>
        /// <param name="nodes">the nodes in the graph</param>
        /// <returns></returns>
        public static long OrderedMaxPath(GraphNode startNode, GraphNode endNode, GraphNode[] nodes)
        {
            var stack = new Stack<GraphNode>();

            foreach (var n in nodes)
            {
                n.Value *= -1;
                n.Distance = long.MaxValue;
                if(!n.Visited)
                    SortGraphTopologically(n, stack);
            }

            foreach (var n in nodes)
                n.Visited = false;

            startNode.Distance = 0;

            foreach(var u in stack.AsEnumerable())
            {
                u.Visited = true;
                foreach(var v in u.AdjacentNodes.Where(a => !a.Visited))
                {
                    var dist = u.Distance + v.Value;

                    if (dist < v.Distance)
                        v.Distance = dist;
                }
            }

            return -1 * (endNode.Distance + startNode.Value);
        }
    }
}

