using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceNow.DataStructures;

namespace ServiceNow.Tests.HashTable
{
    [TestClass]
    public class ComplexHashTableRemoveTests
    {
        [TestMethod]
        public void Rejects_Null_Key()
        {
            var ht = new ComplexHashTable();
            var valid = true;

            try
            {
                ht.Remove(null);
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
            var ht = new ComplexHashTable(1);
            ht.Add(1, 1);
            var valid = true;

            try
            {
                ht.Remove(2);
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
                ht.Remove(1);
            }
            catch (ArgumentException)
            {
                valid = false;
            }

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Leaves_Correct_Value_In_Bucket_After_Remove()
        {
            var ht = new ComplexHashTable();
            ht.Add(1, 1);
            ht.Add(50, 50);
            ht.Remove(50);

            Assert.AreEqual(ht.Get(1), 1);
        }

        [TestMethod]
        public void Removes_Correct_Value_In_Bucket()
        {
            var valid = false;
            var ht = new ComplexHashTable();
            ht.Add(1, 1);
            ht.Add(50, 50);
            ht.Remove(50);

            try
            {
                ht.Get(50);
            }
            catch (ArgumentException)
            {
                valid = true;
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Leaves_Correct_Value_In_Overloaded_Bucket_After_Remove()
        {
            var ht = new ComplexHashTable(1);
            ht.Add(1, 1);
            ht.Add(2, 2);
            ht.Remove(1);

            Assert.AreEqual(ht.Get(2), 2);
        }

        [TestMethod]
        public void Removes_Correct_Value_In_Overloaded_Bucket()
        {
            var valid = false;
            var ht = new ComplexHashTable(1);
            ht.Add(1, 1);
            ht.Add(2, 2);
            ht.Remove(1);

            try
            {
                ht.Get(1);
            }
            catch (ArgumentException)
            {
                valid = true;
            }

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Clear_Removes_Items()
        {
            var valid = false;
            var ht = new ComplexHashTable(1);
            ht.Add(1, 1);
            ht.Add(2, 2);
            ht.Clear();

            try
            {
                ht.Get(1);
            }
            catch (ArgumentException)
            {
                valid = true;
            }

            Assert.IsTrue(valid);

            valid = false;
            try
            {
                ht.Get(2);
            }
            catch (ArgumentException)
            {
                valid = true;
            }

            Assert.IsTrue(valid);
        }
    }
}
