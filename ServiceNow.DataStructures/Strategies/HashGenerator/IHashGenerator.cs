namespace ServiceNow.DataStructures.Strategies.HashGenerator
{
    /// <summary>
    /// A strategy pattern interface to determine how to generate hash codes
    /// </summary>
    public interface IHashGenerator
    {
        /// <summary>
        /// Computes a hash code integer value from an object
        /// </summary>
        /// <param name="key">the value to compute the hashcode of</param>
        /// <returns>the hash code</returns>
        int GenerateHash(object key);
    }
}
