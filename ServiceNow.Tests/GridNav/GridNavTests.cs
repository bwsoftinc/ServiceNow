using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.GridNav;

namespace ServiceNow.Tests
{
    [TestClass]
    public class GridNavTests
    {
        //[TestMethod]
        public void Four_By_Four_Grid()
        {
            var grid = new Grid(4, 4, new long[] {
                    4,    8,  100, -1000,
                   70,  -10, 2000,    70,
                   -5,  -21,   -6,     8,
                10000,  -20,   15,    21
            });

            var node = GridTreeBuilder.BuildTree(grid);
            var sum = TreeTraverser.TraverseDFS(node);

            Assert.AreEqual(sum, 0);
        }

        [TestMethod]
        public void Two_By_Two_Grid()
        {
            var grid = new Grid(2, 2, new long[] {
                    4,    8, 
                   70,  -10
            });

            var node = GridTreeBuilder.BuildTree(grid);
            var sum = TreeTraverser.TraverseDFS(node);

            Assert.AreEqual(sum, 0);
        }

        [TestMethod]
        public void One_By_One_Grid()
        {
            var grid = new Grid(1, 1, new long[] {
                    4
            });

            var node = GridTreeBuilder.BuildTree(grid);
            var sum = TreeTraverser.TraverseDFS(node);

            Assert.AreEqual(sum, 4);
        }
    }
}
