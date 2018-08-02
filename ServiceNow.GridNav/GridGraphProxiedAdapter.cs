using System;
using System.Linq;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Turns a grid board into a graph structure
    /// </summary>
    public class GridGraphProxiedAdapter
    {
        /// <summary>
        /// Wrapper storage of the grid
        /// </summary>
        private Grid grid;

        private GraphNode[] _nodes;

        /// <summary>
        /// A place to cache the top left node of the graph
        /// </summary>
        public GraphNode[] nodes {
            get {
                //here's the proxy pattern both deferred execution and caching
                if (_nodes == null)
                    _nodes = buildGraph(grid);

                return _nodes;
            }
        }

        /// <summary>
        /// Takes the grid that should be transformed into a graph
        /// </summary>
        /// <param name="grid"></param>
        public GridGraphProxiedAdapter(Grid grid)
        {
            this.grid = grid;
        }

        /// <summary>
        /// Returns the indicated start node of the grid as a graph node with fully built graph structure attached
        /// Top left most grid coordinate is 0,0 and coordinates increase going right and down
        /// </summary>
        /// <param name="startX">the grid's x coordinate of the equivalent node to be returned</param>
        /// <param name="startY">the grid's y coordinate of the equivalent node to be returend</param>
        /// <param name="endX">the grid's x coordinate of the node to be marked as the end node</param>
        /// <param name="endY">the grid's y coordinate of the node to be marked as the end node</param>
        /// <returns></returns>
        public (GraphNode startNode, GraphNode endNode) GetGraph(int startX, int startY, int endX, int endY)
        {
            if (startX < 0 || startX >= grid.Width || startY >= grid.Height || startY < 0)
                throw new ArgumentException();

            if (endX < 0 || endX >= grid.Width || endY >= grid.Height || endY < 0)
                throw new ArgumentException();

            //clear all flags
            foreach (var n in nodes)
                n.Visited = false;

            //return the start and end nodes
            return (nodes[(startY * grid.Width) + startX], nodes[(endY * grid.Width) + endX]);
        }

        /// <summary>
        /// Builds a graph structure of nodes to represent the grid borad with verticies to neighboring
        /// coordinates and returns the vertex node: the top left most coordinate in the grid
        /// </summary>
        /// <param name="grid">the grid representation of the</param>
        /// <returns>the vertex node of the tree structure</returns>
        private static GraphNode[] buildGraph(Grid grid)
        {
            var nodes = new GraphNode[grid.Numbers.Length];

            for (var i = 0; i < grid.Numbers.Length; i++)
                nodes[i] = new GraphNode { Value = grid.Numbers[i] };

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

            return nodes;
        }
    }
}


