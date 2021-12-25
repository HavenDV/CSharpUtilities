#nullable enable

namespace Net45.Utilities;

/// <summary>
/// Use it instead Array.Empty in .Net Framework 4.5.
/// </summary>
/// <typeparam name="T"></typeparam>
internal static class Array<T>
{
    /// <summary>
    /// Zero-length array.
    /// </summary>
#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NET40 || NET45 || NET452
    public static T[] Empty { get; } = new T[0];
#else
    public static T[] Empty => System.Array.Empty<T>();
#endif
}
