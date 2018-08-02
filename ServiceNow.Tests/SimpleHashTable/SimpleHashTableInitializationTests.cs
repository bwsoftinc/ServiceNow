using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests
{
    [TestClass]
    public class SimpleHashTableInitializationTests
    {
        [TestMethod]
        public void Rejects_Negative_Initialization_Size()
        {
            var valid = true;

            try
            {
                new SimpleHashTable(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Rejects_Greater_Than_Max_Prime_Initialization_Size()
        {
            var valid = true;
            var maxprime = 0x7FEFFFFD;

            try
            {
                new SimpleHashTable(maxprime + 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }
    }
}
