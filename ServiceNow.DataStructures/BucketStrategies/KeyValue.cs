namespace ServiceNow.DataStructures.BucketStrategies
{
    /// <summary>
    /// The basic data structure for a key and value pair in the hash table
    /// </summary>
    public class KeyValue
    {
        /// <summary>
        /// a key object of the key value pair
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// a value object of the key value pair
        /// </summary>
        public object Value { get; set; }
    }
}
