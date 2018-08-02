using System;
using ServiceNow.DataStructures.Factories;
using ServiceNow.DataStructures.Strategies.Bucket;
using ServiceNow.DataStructures.Strategies.HashGenerator;
using ServiceNow.DataStructures.Strategies.BucketCollection;
using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures
{
    /// <summary>
    /// Simple Hashtable provides a method of storing, retrieving (and deleting) objects by a known key to access a related paired value
    /// The simple implementation uses a fixed bucket collection size, with hashing algoritm to determine which bucket index to place and item
    /// Hash collisions are handled by storing multiple items in the same bucket and using equality comparer to determine matching keys
    /// Note: is not thread safe, assumes consumer will provide thread safety during access
    /// </summary>
    public class SimpleHashTable
    {
        #region Internal storage

        /// <summary>
        /// Internal storage of the HashTable keys and values
        /// </summary>
        protected IBucketCollection buckets;

        /// <summary>
        /// The maximum prime number that is still a valid size of an array
        /// </summary>
        protected const int maxSize = 0x7FEFFFFD;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializs the hashtable with the provided size, default 4049
        /// Initializes default ObjectFrameworkHashGenerator and ByReferenceAndValueKeyEqualityComparer strategies
        /// </summary>
        /// <param name="size">The size of the hashtable to initialize</param>
        public SimpleHashTable(int size = 4049) : this(size, new ObjectFrameworkHashGenerator()) { }

        /// <summary>
        /// Initializs the hashtable with the provided size
        /// Initializes provided IHashGenerator implementation and default ByReferenceAndValueKeyEqualityComparer strategy
        /// </summary>
        /// <param name="size">The size of the hashtable to initialize</param>
        /// <param name="keyhash">the IHashGenerator implementation to use</param>
        public SimpleHashTable(int size, IHashGenerator keyhash) : this(size, keyhash, new ByReferenceAndValueKeyEqualityComparer()) { }

        /// <summary>
        /// Initializs the hashtable with the provided size
        /// Initializes provided IHashGenerator and IKeyEqualityComparer Implementations
        /// </summary>
        /// <param name="size">the size of the hashtable to initialize</param>
        /// <param name="keyhash">the IHashGenerator implementation to use</param>
        /// <param name="comparer">the IKeyEqualityComparer implementation to use</param>
        public SimpleHashTable(int size, IHashGenerator keyhash, IKeyEqualityComparer comparer)
        {
            if (size <= 0 || size > maxSize)
                throw new ArgumentOutOfRangeException("Initial size must be within range 0 < size < " + (maxSize + 1));

            if (comparer == null || keyhash == null)
                throw new ArgumentNullException("Key comparer and key hash implementations cannot be null");

            buckets = BucketCollectionFactory.MakeBuckets<IMultiItemBucket>(size, keyhash, comparer);
        }

        #endregion

        #region Accessor Methods

        /// <summary>
        /// Adds a new key value pair to the hashtable, throws exception on null key or if key already exists
        /// </summary>
        /// <param name="key">the key to hash</param>
        /// <param name="value">the value to store</param>
        public void Add(object key, object value) => buckets.Add(key, value);

        /// <summary>
        /// Retrieves the value for a given key from the hashtable, throws exception in key is null or if key is not found
        /// </summary>
        /// <param name="key">they key to lookup</param>
        /// <returns>the value for the key</returns>
        public object Get(object key) => buckets.Get(key);

        /// <summary>
        /// Removes the item with the specified key from the hashtable, throws exception if key is null or not found
        /// </summary>
        /// <param name="key">the key of the item to remove</param>
        public void Remove(object key) => buckets.Remove(key);

        /// <summary>
        /// Gets or sets the value in the hashtable for a given key
        /// getter throws exception if key is null or not found
        /// setter throws exception in key is null or already exists
        /// </summary>
        /// <param name="key">they key to lookup or set</param>
        /// <returns>the value for the key</returns>
        public object this[object key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        /// <summary>
        /// Determines if the hashtable contians an item with the provided key
        /// </summary>
        /// <param name="key">they key to look for</param>
        /// <returns>whether or not the key was found</returns>
        public bool ContainsKey(object key) => buckets.ContainsKey(key);

        /// <summary>
        /// Empties the hash table
        /// </summary>
        public void Clear() => buckets.Clear();

        #endregion
    }
}
