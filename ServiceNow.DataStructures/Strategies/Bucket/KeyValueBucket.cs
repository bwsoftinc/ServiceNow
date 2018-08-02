using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Strategies.Bucket
{
    /// <summary>
    /// This is a single item bucket that holds a key and value and implements the single item bucket interface
    /// </summary>
    public class KeyValueBucket : KeyValue, ISingleItemBucket
    {
        public bool HasCollision { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int HashCode { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IKeyEqualityComparer comparer => throw new System.NotImplementedException();
    }
}
