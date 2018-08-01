using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests
{
    [TestClass]
    public class BWHashTableGetTests
    {
        [TestMethod]
        public void Rejects_Null_Keys()
        {
            var ht = new SimpleHashTable();
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
        public void Rejects_Missing_Key_In_Overloaded_Bucket()
        {
            //all go into same bucket
            var ht = new SimpleHashTable(1);
            ht.Add(1, 1);
            var valid = true;

            try
            {
                ht.Get(2);
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
            var ht = new SimpleHashTable();
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
        public void Finds_Correct_Value_In_Bucket()
        {
            var ht = new SimpleHashTable();
            ht.Add(1, 1);
            ht.Add(50, 50);

            Assert.AreEqual(ht.Get(1), 1);
        }

        [TestMethod]
        public void Finds_Correct_Value_In_Overloaded_Bucket()
        {
            var ht = new SimpleHashTable(1);
            ht.Add(1, 1);
            ht.Add(2, 2);

            Assert.AreEqual(ht.Get(2), 2);
        }
    }
}
