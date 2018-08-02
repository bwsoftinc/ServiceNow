using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.GridNav;

namespace ServiceNow.Tests.GridNav
{
    [TestClass]
    public class GridNavRunner

    {
        [TestMethod]
        public void Eight_By_Eight_Grid()
        {
            var grid = new Grid(8, 8, new long[] {
                    4,    8,  100, -1000,     4,    8,  100, -1000,
                   70,  -10, 2000,    70,    70,  -10, 2000,    70,
                   -5,  -21,   -6,     8,    -5,  -21,   -6,     8,
                10000,  -20,   15,    21, 10000,  -20,   15,    21,
                    4,    8,  100, -1000,     4,    8,  100, -1000,
                   70,  -10, 2000,    70,    70,  -10, 2000,    70,
                   -5,  -21,   -6,     8,    -5,  -21,   -6,     8,
                10000,  -20,   15,    21, 10000,  -20,   15,    21
            });

            var adapter = new GridGraphProxiedAdapter(grid);
            var nodes = adapter.GetGraph(0, 0, 7, 7);

            var result = TreeTraverser.OrderedMaxPath(nodes.startNode, nodes.endNode, adapter.nodes);

            Assert.AreEqual(47799, result);
        }

        [TestMethod]
        public void Four_By_Four_Grid()
        {
            var grid = new Grid(4, 4, new long[] {
                    4,    8,  100, -1000,
                   70,  -10, 2000,    70,
                   -5,  -21,   -6,     8,
                10000,  -20,   15,    21
            });

            var adapter = new GridGraphProxiedAdapter(grid);
            var nodes = adapter.GetGraph(0, 0, 3, 3);
            var result = TreeTraverser.BrutForceMaxPathDFS(nodes.startNode, nodes.endNode, 0);

            Assert.IsTrue(result.terminal);
            Assert.AreEqual(result.sum, 12270);
        }

        [TestMethod]
        public void Three_By_Three_Grid()
        {
            var grid = new Grid(3, 3, new long[] {
                    4,    8,  100,
                   70,  -10, 2000,
                   -5,  -21,   -6
            });

            var adapter = new GridGraphProxiedAdapter(grid);
            var nodes = adapter.GetGraph(0, 0, 2, 2);
            var result = TreeTraverser.BrutForceMaxPathDFS(nodes.startNode, nodes.endNode, 0);


            var adapter2 = new GridGraphProxiedAdapter(grid);
            var nodes2 = adapter2.GetGraph(0, 0, 2, 2);
            var result2 = TreeTraverser.OrderedMaxPath(nodes2.startNode, nodes2.endNode, adapter2.nodes);

            Assert.IsTrue(result.terminal);
            Assert.AreEqual(result.sum, result2);
        }

        [TestMethod]
        public void Two_By_Two_Grid()
        {
            var grid = new Grid(2, 2, new long[] {
                    4,   -8, 
                   70,   10
            });
            var adapter = new GridGraphProxiedAdapter(grid);
            var nodes = adapter.GetGraph(0, 0, 1, 1);
            var result = TreeTraverser.BrutForceMaxPathDFS(nodes.startNode, nodes.endNode, 0);
            
            var adapter2 = new GridGraphProxiedAdapter(grid);
            var nodes2 = adapter2.GetGraph(0, 0, 1, 1);
            var result2 = TreeTraverser.OrderedMaxPath(nodes2.startNode, nodes2.endNode, adapter2.nodes);

            Assert.IsTrue(result.terminal);
            Assert.AreEqual(result2, result.sum);
        }

        [TestMethod]
        public void One_By_One_Grid()
        {
            var grid = new Grid(1, 1, new long[] {
                    4
            });

            var adapter = new GridGraphProxiedAdapter(grid);
            var nodes = adapter.GetGraph(0, 0, 0, 0);

            var result = TreeTraverser.BrutForceMaxPathDFS(nodes.startNode, nodes.endNode, 0);

            Assert.IsTrue(result.terminal);
            Assert.AreEqual(4, result.sum);
        }
    }
}
