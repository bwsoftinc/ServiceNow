namespace ServiceNow.GridNav
{
    /// <summary>
    /// Traverses the tree to find a path with the maximum sum of vertex values
    /// Each vertex can only be visited once during a path traveral
    /// This lends the traversal method to that of a depth first search
    /// </summary>
    public class TreeTraverser
    {
        /// <summary>
        /// Explores the tree recursively using dfs method
        /// Keeps track of visited node along recursive path traversal
        /// Upon return from recursive call unsets the visited node's visited flag
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static long TraverseDFS(GridTreeNode node)
        {
            return traverseDFS(node, 0).sum;
        }

        public static (long sum, bool terminal) traverseDFS(GridTreeNode node, long sum)
        { 
            var maxSum = sum + node.Value;

            if (node.TargetNode)
                return (maxSum, true);

            var terminal = false;
            foreach(var n in node.AdjacentNodes)
            {
                if (n.Visited)
                    continue;

                n.Visited = true;

                var branchSum = traverseDFS(n, maxSum);

                if (branchSum.terminal && branchSum.sum + maxSum > maxSum)
                {
                    maxSum = sum + branchSum.sum;
                    terminal = true;
                }

                n.Visited = false;
            }

            return (maxSum, terminal);
        }
    }
}

