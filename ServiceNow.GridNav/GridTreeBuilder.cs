using System.Linq;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Builds a Tree representation of the grid board
    /// </summary>
    public class GridTreeBuilder
    {
        /// <summary>
        /// Builds a tree structure of nodes to represent the grid borad with verticies to
        /// neighboring grid coordinates and returns the vertex node, aka the top left most coordinate in the grid
        /// </summary>
        /// <param name="grid">the grid representation of the</param>
        /// <returns>the vertex node of the tree structure</returns>
        public static GridTreeNode BuildTree(Grid grid)
        {
            var nodes = new GridTreeNode[grid.Numbers.Length];

            for (var i = 0; i < grid.Numbers.Length; i++)
                nodes[i] = new GridTreeNode { Value = grid.Numbers[i] };

            //this is the last node of the grid, the bottom right where we want to end up finally
            nodes[nodes.Length - 1].TargetNode = true;

            for (var x = 0; x < grid.Width; x++)
            {
                for(var y = 0; y < grid.Height; y++)
                {
                    //find index in our array where the current nodes is based on x, y corrdinates
                    var index = (y * grid.Width) + x;
                    var node = nodes[index];

                    //get north, south, east and west neighbors if they exist
                    var n = y > 0 ? nodes[((y - 1) * grid.Width) + x] : null;
                    var s = y + 1 < grid.Height ? nodes[((y + 1) * grid.Width) + x] : null;
                    var e = x + 1 < grid.Width ? nodes[(y * grid.Width) + x + 1] : null;
                    var w = x > 0? nodes[(y * grid.Width) + x - 1] : null;

                    //get north-east, north-west, south-east and south-west neighbors if they exist
                    var ne = y > 0 && x + 1 < grid.Height ? nodes[((y - 1) * grid.Width) + x + 1] : null;
                    var nw = y > 0 && x > 0 ? nodes[((y - 1) * grid.Width) + x - 1] : null;
                    var se = y + 1 < grid.Height && x + 1 < grid.Height ? nodes[((y + 1) * grid.Width) + x + 1] : null;
                    var sw = y + 1 < grid.Height && x > 0 ? nodes[((y + 1) * grid.Width) + x - 1] : null;

                    //add all the non null neighbors to the node's collection of adjacent nodes
                    node.AdjacentNodes.AddRange(new[] { n, s, e, w, ne, nw, se, sw }.Where(neighbor => neighbor != null));
                }
            }

            //a pointer to the tree vertex where we are starting, the top left position in the grid
            return nodes[0];
        }
    }
}


