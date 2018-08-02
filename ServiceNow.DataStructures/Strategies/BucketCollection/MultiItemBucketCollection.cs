using System;
using ServiceNow.DataStructures.Strategies.Bucket;
using ServiceNow.DataStructures.Strategies.HashGenerator;
using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Strategies.BucketCollection
{
    /// <summary>
    /// The bucket impelementation using a generic bucket type to be determined by the caller
    /// When a hash key collision occurs the same bucket index is reused to place the KeyValue pairs
    /// Be sure to use a TBucket implementation that can accommidate more than one item
    /// Note: does not support null keys
    /// </summary>
    internal class MultiItemBucketCollection<TBucket> : IBucketCollection where TBucket : IMultiItemBucket, new()
    {
        /// <summary>
        /// The implementation used to determine a hash code of the key
        /// </summary>
        private IHashGenerator hash;

        /// <summary>
        /// The implementation used to determine if two keys are equal
        /// </summary>
        private IKeyEqualityComparer comparer;

        /// <summary>
        /// The collection of buckets to place items in
        /// </summary>
        public IBucket[] Buckets { get; private set; }
        
        /// <summary>
        /// The size of the bucket collection for initialization or clearing the buckets
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Determines which bucket index in the buckets array should contain the key
        /// </summary>
        /// <param name="key">the key to compute the bucket index</param>
        /// <returns>positive integer</returns>
        private int GetBucketIndex(object key) => (hash.GenerateHash(key) & 0x7FFFFFFF) % Capacity;

        /// <summary>
        /// Create a new bucket collection
        /// </summary>
        /// <param name="size">the number of buckets</param>
        /// <param name="hash">the hashing implementation</param>
        /// <param name="comparer">the equality comparer implementation</param>
        public MultiItemBucketCollection(int size, IHashGenerator hash, IKeyEqualityComparer comparer)
        {
            this.Capacity = size;
            this.hash = hash;
            this.comparer = comparer;
            Clear();
        }

        /// <summary>
        /// Initializes the buckets to empty
        /// </summary>
        public void Clear()
        {
            Buckets = (IBucket[])Array.CreateInstance(typeof(TBucket), Capacity);

            for (var i = 0; i < Capacity; i++)
            {
                var val = (IBucket)Activator.CreateInstance(typeof(TBucket), comparer);
                val.HashCode = i;
                Buckets[i] = val;
            }
        }

        /// <summary>
        /// Determines if a key exists within these buckets
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            var index = GetBucketIndex(key);
            return ((IMultiItemBucket)Buckets[index]).ContainsKey(key);
        }

        /// <summary>
        /// Removes the key and value from this hashtable, throws expcetion if key is null or not found
        /// </summary>
        /// <param name="key">the key to lookup to remove</param>
        public void Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            var index = GetBucketIndex(key);
            ((IMultiItemBucket)Buckets[index]).Remove(key);
        }

        /// <summary>
        /// Adds a new key value pair to the hashtable, throws exception on null key or if key already exists
        /// </summary>
        /// <param name="key">the key to hash</param>
        /// <param name="value">the value to store</param>
        public void Add(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            var index = GetBucketIndex(key);
            ((IMultiItemBucket)Buckets[index]).Add(key, value);
        }

        /// <summary>
        /// Retrieves the value for a given key from the hashtable, throws exception in key is null or if key is not found
        /// </summary>
        /// <param name="key">they key to lookup</param>
        /// <returns>the value for the key</returns>
        public object Get(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            var index = GetBucketIndex(key);
            return ((IMultiItemBucket)Buckets[index]).Get(key);
        }
    }
}
