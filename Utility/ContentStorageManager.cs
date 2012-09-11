using System.Collections.Generic;


namespace ParasiteP1.Utility
{
    public class DumbException : System.Exception
    {
        public DumbException() { }
        public DumbException(string message) : base(message) { }
    }
    /// <summary>
    /// Used for storing and retrieving content.
    /// </summary>
    public static class ContentStorageManager
    {
        static Dictionary<string, object> storageDict = new Dictionary<string, object>();

        /// <summary>
        /// Retrieve an object from storage.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <param name="key">The index of the object.</param>
        /// <returns>the object at index key.</returns>
        public static T Get<T>(string key)
        {
            key = key.ToUpper();
            if (storageDict.ContainsKey(key))
            {
                return (T)storageDict[key]; 
            }
            throw new DumbException("You mispelled it!: " + key);

            
        }

        /// <summary>
        /// Stores an object at an index.
        /// </summary>
        /// <typeparam name="T">The type of the object to be stored.</typeparam>
        /// <param name="key">The index to store the object at.</param>
        /// <param name="item">The object itself.</param>
        public static void Store<T>(string key, T item)
        {
            key = key.ToUpper();
            storageDict[key] = item;
        }

        /// <summary>
        /// Clears all content.
        /// </summary>
        public static void Clear()
        {
            storageDict.Clear();
        }

        /// <summary>
        /// Clears the content at an index.
        /// </summary>
        /// <param name="key">The index to clear content from.</param>
        public static void Clear(string key)
        {
            key = key.ToUpper();
            if (storageDict.ContainsKey(key))
            {
                storageDict.Remove(key);
            }
        }
    }
}
