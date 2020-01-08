using System;

#nullable enable

namespace NetStandard20.Utilities
{
    public static class ObjectExtensions
    {
        public static T With<T>(this T value, Action<T> action) where T : class
        {
            action(value);

            return value;
        }
    }
}