using ServiceNow.DataStructures.EqualityComparerStrategies;

namespace ServiceNow.DataStructures.BucketStrategies
{
    /// <summary>
    /// Interface representation of a single bucket slot that supports more than one item in the bucket
    /// Contrast a single bucket slot that only supports one item in the bucket then there would not be a need for these multi item bucket interface members
    /// </summary>
    public interface IMultiItemBucket : IBucket
    {
        /// <summary>
        /// Does this bucket slot have the exact key
        /// </summary>
        /// <param name="key">The key to check for</param>
        /// <returns>Whether the key was found in this bucket</returns>
        bool ContainsKey(object key);

        /// <summary>
        /// How to compare key equality
        /// </summary>
        IKeyEqualityComparer comparer { get; }

        /// <summary>
        /// Retrieve the related value for the key from this bucket
        /// </summary>
        /// <param name="key">The key to lookup</param>
        /// <returns>The related value for the key</returns>
        object Get(object key);

        /// <summary>
        /// Adds a new key value item to this bucket
        /// </summary>
        /// <param name="key">The key to be added</param>
        /// <param name="value">The value to be added</param>
        void Add(object key, object value);

        /// <summary>
        /// Removes a value from this bucket that has the specified key
        /// </summary>
        /// <param name="key">The key to look for</param>
        void Remove(object key);
    }
}
