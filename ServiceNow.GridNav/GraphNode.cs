using System.Collections.Generic;
using System.Diagnostics;

namespace ServiceNow.GridNav
{
    /// <summary>
    /// Represents the rectangular grid as a tree structure to facilitate tree traversal
    /// Each grid coordate is represented as a tree node and each of the neighboring nodes
    /// is added to the adjoining nodes collection
    /// </summary>
    [DebuggerDisplay("Distance = {Distance}, Value = {Value}")]
    public class GraphNode
    {
        /// <summary>
        /// The value at the grid coordinate
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// The vertext distance of this node to the end
        /// </summary>
        public long Distance { get; set; }

        /// <summary>
        /// Whether this node has been visited alread during traversal
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// All the neighboring nodes
        /// </summary>
        public List<GraphNode> AdjacentNodes { get; set; } = new List<GraphNode>();
    }
}

