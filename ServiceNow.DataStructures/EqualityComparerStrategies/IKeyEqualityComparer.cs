namespace ServiceNow.DataStructures.EqualityComparerStrategies
{
    /// <summary>
    /// A strategy pattern interface to determine how equality of keys is to be compared
    /// </summary>
    public interface IKeyEqualityComparer
    {
        bool AreKeysEqual(object key1, object key2);
    }
}
