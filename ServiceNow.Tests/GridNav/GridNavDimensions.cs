using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.GridNav;

namespace ServiceNow.Tests
{
    [TestClass]
    public class GridNavDimensions
    {
        [TestMethod]
        public void Only_Accepts_Positive_Dimensions()
        {
            var valid = true;
            try
            {
                var grid = new Grid(0, 1, new long[] { 4 });
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
            valid = true;

            try
            {
                var grid = new Grid(4, -5, new long[] { 4 });
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }


            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Allows_Rectangular_Dimensions()
        {
            var valid = true;

            try
            {
                var grid = new Grid(8, 2, new long[] {
                        4,    8,  100, -1000,
                       70,  -10, 2000,    70,
                       -5,  -21,   -6,     8,
                    10000,  -20,   15,    21
                });
            }
            catch
            {
                valid = false;
            }

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Denies_Long_MinValue()
        {
            var valid = true;
            try
            {
                var grid = new Grid(1, 1, new long[] { long.MinValue });
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }


        [TestMethod]
        public void Denies_Undefined_Grid_Values()
        {
            var valid = true;
            try
            {
                var grid = new Grid(2, 2, null);
            }
            catch (ArgumentNullException)
            {
                valid = false;
            }
            
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Denies_Unmatched_Dimensions_With_Values()
        {
            var valid = true;

            try
            {
                var grid = new Grid(2, 1, new long[] { 4 });
            }
            catch (ArgumentException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }
    }
}
