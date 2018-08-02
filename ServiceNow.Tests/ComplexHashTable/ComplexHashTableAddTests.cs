using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests.HashTable
{
    [TestClass]
    public class ComplexHashTableAddTests
    {
        [TestMethod]
        public void Does_Expand()
        {
            var ht = new ComplexHashTable(3);

            ht.Add(1, 1);
            ht.Add(2, 1);
            ht.Add(3, 1);
            ht.Add(4, 1);
        }

        [TestMethod]
        public void Handles_Collisions_Expand()
        {
            var ht = new ComplexHashTable(3);

            ht.Add(0, 1);
            ht.Add(0f, 2);
        }

        [TestMethod]
        public void Rejects_Null_Keys()
        {
            var ht = new ComplexHashTable();
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
            var ht = new ComplexHashTable();
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
