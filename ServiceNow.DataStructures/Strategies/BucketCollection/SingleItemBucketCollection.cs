using System;
using System.Linq;
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
    internal class SingleItemBucketCollection<TBucket> : IBucketCollection where TBucket : ISingleItemBucket, new()
    {

        #region internal storage

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
        /// The capacity to reach before resizing the buckets
        /// </summary>
        public float LoadFactor { get; private set; }

        /// <summary>
        /// The size of the bucket collection for initialization or clearing the buckets
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// How many collisions there are
        /// </summary>
        private int occupancy { get; set; }

        /// <summary>
        /// How many set items there are
        /// </summary>
        private int count { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// Create a new bucket collection
        /// </summary>
        /// <param name="size">the number of buckets</param>
        /// <param name="hash">the hashing implementation</param>
        /// <param name="comparer">the equality comparer implementation</param>
        public SingleItemBucketCollection(int size, float load, IHashGenerator hash, IKeyEqualityComparer comparer)
        {
            LoadFactor = load;
            Capacity = size;
            this.hash = hash;
            this.comparer = comparer;
            Clear();
        }

        #endregion

        #region maintenence methods

        private static readonly int[] primes = new int[] {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369, 976369, 1982627,
            4026031, 8175383, 16601593, 33712729, 68460391, 139022417, 282312799, 573292817, 1164186217, 2146435069
        };

        /// <summary>
        /// expand the bucket collection to the next prime size in the primes list of primes
        /// also rehashes and replaces all items into the new bucket collection
        /// </summary>
        private void resize()
        {
            var nextPrime = primes.Where(p => p > Capacity).FirstOrDefault();

            if (nextPrime == 0)
                throw new IndexOutOfRangeException("Hashtable cannot grow any larger");

            var buckets = Clear(nextPrime, comparer);
            occupancy = 0;

            foreach(ISingleItemBucket b in Buckets)
            {
                if (b.Key != null)
                    place(buckets, b.Key, b.Value);
            }

            Buckets = buckets;
        }

        /// <summary>
        /// internal method to add a new key value pair to the buckets
        /// can throw an exception if they key already exists in the buckets
        /// assumes there is sufficent room for the new item in the buckets
        /// uses optimal algorithm to skip to next bucket if occupied
        /// </summary>
        /// <param name="buckets">the bucket collection to insert into</param>
        /// <param name="key">the item key</param>
        /// <param name="value">the item value</param>
        private void place(IBucket[] buckets, object key, object value)
        {
            const int prime = 101;
            var h = hash.GenerateHash(key) & 0x7FFFFFFF;
            var uh = (uint)h;
            var up = (uint)buckets.Length;
            var skip = ((uh * prime) % (up - 1)) + 1;
            var index = (int)(uh % up);

            //primes make this impossible to endless loop
            while (true)
            {
                var nb = (ISingleItemBucket)buckets[index];

                if (nb.Key == null)
                {
                    nb.Value = value;
                    nb.Key = key;
                    nb.HashCode = h;
                    count++;
                    return;
                }

                if (!nb.HasCollision)
                {
                    if (comparer.AreKeysEqual(nb.Key, key))
                        throw new ArgumentException("Cannot place duplicate key in hashtable");

                    nb.HasCollision = true;
                    occupancy++;
                }

                index = (int)((index + skip) % up);
            }
        }

        /// <summary>
        /// Initializes the buckets to empty
        /// </summary>
        public void Clear()
        {
            Buckets = Clear(Capacity, comparer);
            occupancy = 0;
            count = 0;
        }

        /// <summary>
        /// Makes an empty bucket array
        /// </summary>
        /// <param name="size">The number of items in the array</param>
        /// <param name="comparer">The key equality comparer interface to set in the buckets</param>
        /// <returns>An IBucket array</returns>
        private static IBucket[] Clear(int size, IKeyEqualityComparer comparer)
        {
            var buckets = (IBucket[])Array.CreateInstance(typeof(TBucket), size);

            for (var i = 0; i < size; i++)
            {
                var val = (IBucket)Activator.CreateInstance(typeof(TBucket), comparer);
                buckets[i] = val;
            }

            return buckets;
        }

        #endregion

        #region accessor methods

        /// <summary>
        /// Determines if a key exists within these buckets
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>Whether the key was found</returns>
        public bool ContainsKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            return keyIndex(Buckets, key) > -1;
        }

        private int keyIndex(IBucket[] buckets, object key)
        {
            const int prime = 101;
            var h = hash.GenerateHash(key) & 0x7FFFFFFF;
            var uh = (uint)h;
            var up = (uint)buckets.Length;
            var skip = ((uh * prime) % (up - 1)) + 1;
            var index = (int)(uh % up);

            //primes make this impossible to endless loop
            while (true)
            {
                var nb = (ISingleItemBucket)buckets[index];

                if (nb.Key == null)
                    return -1;

                if (comparer.AreKeysEqual(nb.Key, key))
                    return index;

                index = (int)((index + skip) % up);
            }
        }

        /// <summary>
        /// Removes the key and value from this buckets, throws expcetion if key is null or not found
        /// </summary>
        /// <param name="key">the key to lookup to remove</param>
        public void Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            const int prime = 101;
            var h = hash.GenerateHash(key) & 0x7FFFFFFF;
            var uh = (uint)h;
            var up = (uint)Buckets.Length;
            var skip = ((uh * prime) % (up - 1)) + 1;
            var index = (int)(uh % up);

            //primes make this impossible to endless loop
            while (true)
            {
                var nb = (ISingleItemBucket)Buckets[index];

                if (nb.Key == null && !nb.HasCollision)
                    throw new ArgumentException(nameof(key) + " not found");

                if (comparer.AreKeysEqual(nb.Key, key))
                {
                    nb.Key = null;
                    nb.Value = null;
                    return;
                }

                index = (int)((index + skip) % up);
            }
        }

        /// <summary>
        /// Adds a new key value pair to the buckets, throws exception on null key or if key already exists
        /// </summary>
        /// <param name="key">the key to hash</param>
        /// <param name="value">the value to store</param>
        public void Add(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            if (count >= (Capacity * LoadFactor))
                resize();

            place(Buckets, key, value);
        }

        /// <summary>
        /// Retrieves the value for a given key from the buckets, throws exception in key is null or if key is not found
        /// </summary>
        /// <param name="key">they key to lookup</param>
        /// <returns>the value for the key</returns>
        public object Get(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key) + " cannot be null");

            var index = keyIndex(Buckets, key);

            if (index < 0)
                throw new ArgumentException(nameof(key) + " does not exist");

            return ((ISingleItemBucket)Buckets[index]).Value;
        }


        /// <summary>
        /// Operator accessor wrappers over Get and Add Methods
        /// </summary>
        /// <param name="key">The key of the item</param>
        /// <returns>The value for the key</returns>
        public object this[object key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        #endregion
    }
}
