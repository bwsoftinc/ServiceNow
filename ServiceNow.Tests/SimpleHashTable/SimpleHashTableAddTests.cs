using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests
{
    [TestClass]
    public class SimpleHashTableAddTests
    {
        [TestMethod]
        public void Rejects_Null_Keys()
        {
            var ht = new SimpleHashTable();
            var valid = true;

            try
            {
                ht.Add(null, 1);
            }
            catch (ArgumentNullException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Rejects_Duplicate_Keys()
        {
            var ht = new SimpleHashTable();
            ht.Add(1, 1);

            var valid = true;

            try
            {
                ht.Add(1, 1);
            }
            catch (ArgumentException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }
    }
}
