using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests.HashTable
{
    [TestClass]
    public class ComplexHashTableGetTests
    {
        [TestMethod]
        public void Rejects_Null_Keys()
        {
            var ht = new ComplexHashTable();
            var valid = true;

            try
            {
                ht.Get(null);
            }
            catch (ArgumentNullException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Rejects_Missing_Key_In_Collision()
        {
            //all go into same bucket
            var ht = new ComplexHashTable(1);
            ht.Add(0, 1);
            var valid = true;

            try
            {
                ht.Get(0f);
            }
            catch (ArgumentException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Rejects_Missing_Bucket()
        {
            var ht = new ComplexHashTable();
            var valid = true;

            try
            {
                ht.Get(1);
            }
            catch (ArgumentException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Finds_Correct_Value()
        {
            var ht = new ComplexHashTable();
            ht.Add(1, 1);
            ht.Add(50, 50);

            Assert.AreEqual(ht.Get(1), 1);
        }

        [TestMethod]
        public void Finds_Correct_Value_In_Collision()
        {
            var ht = new ComplexHashTable();
            ht.Add(0, 1);
            ht.Add(0f, 2);

            Assert.AreEqual(ht.Get(0f), 2);
        }
    }
}
