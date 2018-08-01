namespace ServiceNow.DataStructures.BucketStrategies
{
    /// <summary>
    /// An interface to define strategies of how to store and access a collection of buckets
    /// </summary>
    public interface IBuckets
    {
        /// <summary>
        /// The internal storage of buckets
        /// </summary>
        IBucket[] Buckets { get; }

        /// <summary>
        /// The size of the buckets array
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// How to add a new key value pair to the buckets
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(object key, object value);

        /// <summary>
        /// How to get a value for a given key from the buckets
        /// </summary>
        /// <param name="key">the key to lookup</param>
        /// <returns>the value for the looked up key</returns>        
        object Get(object key);

        /// <summary>
        /// Reinitializes the backing storage to empty
        /// </summary>
        void Clear();

        /// <summary>
        /// Removes an item from the buckets
        /// </summary>
        /// <param name="key">the key to lookup the item to remove</param>
        void Remove(object key);

        /// <summary>
        /// Looks to see if the buckets have an item with the given key
        /// </summary>
        /// <param name="key">they key to lookup</param>
        /// <returns>whether the key was found</returns>
        bool ContainsKey(object key);
    }
}
