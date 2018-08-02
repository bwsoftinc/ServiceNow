using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Strategies.Bucket
{
    /// <summary>
    /// Interface representation of a single bucket slot for a particular hash code in a bucket collection
    /// </summary>
    public interface IBucket
    {
        /// <summary>
        /// The hashcode that identifies this bucket
        /// </summary>
        int HashCode { get; set;  }

        /// <summary>
        /// How to compare key equality
        /// </summary>
        IKeyEqualityComparer comparer { get; }
    }
}
