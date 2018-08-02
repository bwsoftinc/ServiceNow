namespace ServiceNow.DataStructures.Strategies.Bucket
{
    /// <summary>
    /// Interface representation of a single bucket slot that supports only one item in the bucket
    /// </summary>
    public interface ISingleItemBucket : IBucket
    {
        /// <summary>
        /// Whether there is a collision with this single item in this Bucket
        /// </summary>
        bool HasCollision { get; set; }
    }
}
