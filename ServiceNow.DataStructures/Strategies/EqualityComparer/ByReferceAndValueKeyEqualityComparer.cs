using System;

namespace ServiceNow.DataStructures.Strategies.EqualityComparer
{
    /// <summary>
    /// A strategy pattern implementation to determine equality of two keys by first comparing their equality by reference and then by value
    /// Does not allow nulls, and throws an exception if one of the two keys to compare are null.
    /// </summary>
    internal class ByReferenceAndValueKeyEqualityComparer : IKeyEqualityComparer
    {
        /// <summary>
        /// Uses the frameworks equality comparer to determine if two objects are equal, first by reference (faster) and then by value
        /// Used to determine if two keys are equal in case of a hash collision
        /// Overrideable through inheritence if a specific equality comparison is desired
        /// </summary>
        /// <returns>Whether the keys are equal</returns>
        public bool AreKeysEqual(object key1, object key2)
        {
            if (key1 == null || key2 == null)
                throw new ArgumentNullException();

            return ReferenceEquals(key1, key2) || key1.Equals(key2);
        }
    }
}
