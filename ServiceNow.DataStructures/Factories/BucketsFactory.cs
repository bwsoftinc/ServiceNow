using ServiceNow.DataStructures.BucketStrategies;
using ServiceNow.DataStructures.EqualityComparerStrategies;
using ServiceNow.DataStructures.HashGeneratorStrategies;

namespace ServiceNow.DataStructures.Factories
{
    public class BucketsFactory
    {
        public static IBuckets MakeBuckets<TBucket>(int length, IHashGenerator hash, IKeyEqualityComparer comparer) where TBucket : IBucket
        {
            if(typeof(IMultiItemBucket).IsAssignableFrom(typeof(TBucket))
                return new MultiItemBuckets<LinkedListBucket>(length, hash, comparer);


            return new SingleItemBuckets<>(length, hash, comparer);
        }
    }
}
