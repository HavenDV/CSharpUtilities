using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable enable

namespace NetStandard20.Extensions
{
    /// <summary>
    /// Extensions that work with <see langword="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves the string between the starting fragment and the ending.
        /// The first available fragment is retrieved.
        /// <para/>Returns <see langword="null"/> if nothing is found.
        /// <para/>If <paramref name="end"/> is not specified, the end is the end of the string.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.1]]>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string? Extract(this string text, string start, string? end = null, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));

            var index1 = text.IndexOf(start, comparison ?? StringComparison.Ordinal);
            if (index1 < 0)
            {
                return null;
            }

            index1 += start.Length;
            if (end == null)
            {
                return text.Substring(index1);
            }

            var index2 = text.IndexOf(end, index1, comparison ?? StringComparison.Ordinal);
            if (index2 < 0)
            {
                return null;
            }

            return text.Substring(index1, index2 - index1);
        }

        /// <summary>
        /// Retrieves the strings between the starting fragment and the ending.
        /// All available fragments are retrieved.
        /// <para/>Returns empty <see cref="List{T}"/> if nothing is found.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.1]]>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IEnumerable<(int Start, int Length)> ExtractAllFragments(this string text, string start, string end, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            var index2 = -end.Length;
            while (true)
            {
                var index1 = text.IndexOf(start, index2 + end.Length, comparison ?? StringComparison.Ordinal);
                if (index1 < 0)
                {
                    yield break;
                }

                index1 += start.Length;
                index2 = text.IndexOf(end, index1, comparison ?? StringComparison.Ordinal);
                if (index2 < 0)
                {
                    yield break;
                }

                yield return (index1, index2 - index1);
            }
        }

        /// <summary>
        /// Retrieves the strings between the starting fragment and the ending.
        /// All available fragments are retrieved.
        /// <para/>Returns empty <see cref="List{T}"/> if nothing is found.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.4]]>
        /// <![CDATA[Dependency: ExtractAllFragments(this string text, string start, string end, StringComparison? comparison = null)]]> <br/>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IEnumerable<string> ExtractAll(this string text, string start, string end, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            return text
                .ExtractAllFragments(start, end, comparison)
                .Select(index => text.Substring(index.Start, index.Length));
        }

        /// <summary>
        /// Replaces the strings between the starting fragment and the ending.
        /// All available fragments will be replaced.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.1]]>
        /// <![CDATA[Dependency: ExtractAllFragments(this string text, string start, string end, StringComparison? comparison = null)]]> <br/>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string text, string start, string end, string value, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            var fix = 0;
            foreach (var fragment in text.ExtractAllFragments(start, end, comparison))
            {
                var startIndex = fragment.Start + fix - start.Length;
                var length = start.Length + fragment.Length + end.Length;

                text = text.Remove(startIndex, length);
                text = text.Insert(startIndex, value);

                fix += value.Length - length;
            }

            return text;
        }

        /// <summary>
        /// Converts text to lines using <see cref="StringReader"/>.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IList<string> ToLines(string text)
        {
            var lines = new List<string>();

            using var reader = new StringReader(text);
            for (string? line; (line = reader.ReadLine()) != null;)
            {
                lines.Add(line);
            }

            return lines;
        }
    }
}
