using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests.HashTable
{
    [TestClass]
    public class ComplexHashTableContainsTests
    {
        [TestMethod]
        public void Rejects_Null_Key()
        {
            var ht = new ComplexHashTable();
            var valid = true;

            try
            {
                ht.ContainsKey(null);
            }
            catch (ArgumentNullException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Finds_Key_In_Bucket()
        {
            var ht = new ComplexHashTable();
            ht.Add(1, 1);
            ht.Add(2, 2);

            Assert.IsTrue(ht.ContainsKey(1));
        }

        [TestMethod]
        public void Finds_Key_In_Overloaded_Bucket()
        {
            //all go into same bucket
            var ht = new ComplexHashTable(1);
            ht.Add(1, 1);
            ht.Add(2, 2);

            Assert.IsTrue(ht.ContainsKey(1));
        }

        [TestMethod]
        public void Does_Not_Find_Nonexistant_Key_In_Bucket()
        {
            var ht = new ComplexHashTable();
            ht.Add(1, 1);

            Assert.IsFalse(ht.ContainsKey(2));
        }

        [TestMethod]
        public void Does_Not_Find_Nonexistant_Key_In_Overloaded_Bucket()
        {
            //all go into same bucket
            var ht = new ComplexHashTable(1);
            ht.Add(0, 1);
            ht.Add(0f, 2);

            Assert.IsFalse(ht.ContainsKey(0m));
        }
    }
}
