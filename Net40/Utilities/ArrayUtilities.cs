#nullable enable

namespace Net40.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Empty<T>()
        {
#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NET40 || NET45 || NET452
            return new T[0];
#else
            return System.Array.Empty<T>();
#endif
        }
    }
}
