using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Strategies.Bucket
{
    /// <summary>
    /// This is a single item bucket that holds a key and value and implements the single item bucket interface
    /// </summary>
    public class KeyValueBucket : KeyValue, ISingleItemBucket
    {
        public KeyValueBucket() : this(new ByReferenceAndValueKeyEqualityComparer()) { }

        public KeyValueBucket(IKeyEqualityComparer comparer) { }

        public bool HasCollision { get; set; }

        public int HashCode { get; set; }

        public IKeyEqualityComparer comparer { get; private set; }
    }
}
