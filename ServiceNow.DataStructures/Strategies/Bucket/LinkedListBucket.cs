using System;
using System.Linq;
using System.Collections.Generic;
using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Strategies.Bucket
{
    /// <summary>
    /// A strategy implementation of the IBucket interface defining how a key value item shall be stored and retrieved in a bucket
    /// LinkedListBucket support multiple items in the bucket by storing and retrieving from a 
    /// </summary>
    public class LinkedListBucket : IMultiItemBucket
    {
        #region Data members

        /// <summary>
        /// How key equality is determined in this bucket
        /// </summary>
        public IKeyEqualityComparer comparer { get; private set; }

        /// <summary>
        /// The hashcode of this bucket
        /// </summary>
        public int HashCode { get; set; }

        /// <summary>
        /// Where the keys and values are stored within this bucket, a linked list implementation
        /// </summary>
        public List<KeyValue> Values { get; private set; } = new List<KeyValue>();

        #endregion

        #region Constructors

        /// <summary>
        /// A new instance of the Linked List Bucket with default hashcode 0, and default equality comparer ByReferenceAndValueKeyEqualityComparer
        /// </summary>
        public LinkedListBucket() : this(new ByReferenceAndValueKeyEqualityComparer()) { }

        /// <summary>
        /// A new instance of the Linked List Bucket with provided equality comparer
        /// </summary>
        /// <param name="comparer">An IKeyEqualityComparer implementation</param>
        public LinkedListBucket(IKeyEqualityComparer comparer) => this.comparer = comparer;

        #endregion

        #region Accessors

        /// <summary>
        /// Looks for a key in this bucket 
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>Whether the key was found</returns>
        public bool ContainsKey(object key) => Values.Any(v => comparer.AreKeysEqual(v.Key, key));

        /// <summary>
        /// Puts a new key and value into this bucket, throws exception if this key is alredy here
        /// </summary>
        /// <param name="key">the key that belongs in the bucket slot</param>
        /// <param name="value">the value to belongs to the key</param>
        public void Add(object key, object value)
        {
            if (Values.Any(v => comparer.AreKeysEqual(v.Key, key)))
                throw new ArgumentException(nameof(key) + " already exists");

            Values.Add(new KeyValue { Key = key, Value = value });
        }

        /// <summary>
        /// Gets the value for the given key from this bucket slot, throws exception if the key does not exist
        /// </summary>
        /// <param name="key">they key to find the value</param>
        /// <returns>the value for the key</returns>
        public object Get(object key)
        {
            var item = Values.FirstOrDefault(v => comparer.AreKeysEqual(v.Key, key))
                ?? throw new ArgumentException(nameof(key) + " does not exist");

            return item.Value;
        }

        /// <summary>
        /// Removes a key and value from this bucket slot, throws exception if the key does not exist
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            var item = Values.FirstOrDefault(v => comparer.AreKeysEqual(v.Key, key))
                ?? throw new ArgumentException(nameof(key) + " does not exist");

            Values.Remove(item);
        }

        #endregion
    }
}