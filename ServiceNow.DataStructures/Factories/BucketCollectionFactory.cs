using ServiceNow.DataStructures.Strategies.Bucket;
using ServiceNow.DataStructures.Strategies.HashGenerator;
using ServiceNow.DataStructures.Strategies.BucketCollection;
using ServiceNow.DataStructures.Strategies.EqualityComparer;

namespace ServiceNow.DataStructures.Factories
{
    public class BucketCollectionFactory
    {
        public static IBucketCollection MakeBuckets<TBucket>(int length, IHashGenerator hash, IKeyEqualityComparer comparer) where TBucket : IBucket
        {
            if(typeof(IMultiItemBucket).IsAssignableFrom(typeof(TBucket)))
                return new MultiItemBucketCollection<LinkedListBucket>(length, hash, comparer);

            else if (typeof(ISingleItemBucket).IsAssignableFrom(typeof(TBucket)))
                return new SingleItemBucketCollection<KeyValueBucket>(length, hash, comparer);

            return null;
        }
    }
}
