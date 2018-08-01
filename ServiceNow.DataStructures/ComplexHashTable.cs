using System;
using ServiceNow.DataStructures.Factories;
using ServiceNow.DataStructures.BucketStrategies;
using ServiceNow.DataStructures.HashGeneratorStrategies;
using ServiceNow.DataStructures.EqualityComparerStrategies;

namespace ServiceNow.DataStructures
{
    /// <summary>
    /// Complex Hashtable provides a method of storing, retrieving (and deleting) objects by a known key to access a related paired value
    /// The complex implementation uses a bucket collection that can grow and rehash the items into the expanded bucket collection
    /// Accessor and storage implementations are inherited from simplehashtable, but the buckets factory will create a different type of IBuckets
    /// Note: is not thread safe, assumes consumer will provide thread safety during access
    /// </summary>
    public class ComplexHashTable : SimpleHashTable
    {
        #region Constructors

        /// <summary>
        /// Initializs the hashtable with the provided size, default 4049
        /// Initializes default ObjectFrameworkHashGenerator and ByReferenceAndValueKeyEqualityComparer strategies
        /// </summary>
        /// <param name="size">The size of the hashtable to initialize</param>
        public ComplexHashTable(int size = 4049) : this(size, new ObjectFrameworkHashGenerator()) { }

        /// <summary>
        /// Initializs the hashtable with the provided size
        /// Initializes provided IHashGenerator implementation and default ByReferenceAndValueKeyEqualityComparer strategy
        /// </summary>
        /// <param name="size">The size of the hashtable to initialize</param>
        /// <param name="keyhash">the IHashGenerator implementation to use</param>
        public ComplexHashTable(int size, IHashGenerator keyhash) : this(size, keyhash, new ByReferenceAndValueKeyEqualityComparer()) { }

        /// <summary>
        /// Initializs the hashtable with the provided size
        /// Initializes provided IHashGenerator and IKeyEqualityComparer Implementations
        /// </summary>
        /// <param name="size">the size of the hashtable to initialize</param>
        /// <param name="keyhash">the IHashGenerator implementation to use</param>
        /// <param name="comparer">the IKeyEqualityComparer implementation to use</param>
        public ComplexHashTable(int size, IHashGenerator keyhash, IKeyEqualityComparer comparer)
        {
            if (size <= 0 || size > maxSize)
                throw new ArgumentOutOfRangeException("Initial size must be within range 0 < size < " + (maxSize + 1));

            if (comparer == null || keyhash == null)
                throw new ArgumentNullException("Key comparer and key hash implementations cannot be null");

            buckets = BucketsFactory.MakeBuckets<IMultiItemBucket>(size, keyhash, comparer);
        }

        #endregion
    }
}
