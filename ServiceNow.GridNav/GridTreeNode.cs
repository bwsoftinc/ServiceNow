using System.Collections.Generic;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Represents the rectangular grid as a tree structure to facilitate tree traversal
    /// Each grid coordate is represented as a tree node and each of the neighboring nodes
    /// is added to the adjoining nodes collection
    /// </summary>
    public class GridTreeNode
    {
        /// <summary>
        /// The value at the grid coordinate
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// An indicator whether this is the final node as denoted by the bottom right most coordinate in the grid representation
        /// </summary>
        public bool TargetNode { get; set; }

        /// <summary>
        /// Whether this node has been visited alread during traversal
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// All the neighboring nodes
        /// </summary>
        public List<GridTreeNode> AdjacentNodes { get; set; } = new List<GridTreeNode>();
    }
}

