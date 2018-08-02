namespace ServiceNow.DataStructures.Strategies.HashGenerator
{
    /// <summary>
    /// A strategy pattern interface to determine how to generate hash codes
    /// </summary>
    internal class ObjectFrameworkHashGenerator : IHashGenerator
    {
        /// <summary>
        /// Uses the framework's implementation of GetHashCode() to determine the key object's hashcode to determine which bucket the object belongs to
        /// Overrideable through inhertience if a specific hash algorithm is desired instead
        /// </summary>
        /// <param name="key">The key to be hashed</param>
        /// <returns>The calculated hashcode of the key parameter</returns>
        public int GenerateHash(object key)
        {
            return key.GetHashCode();
        }
    }
}
