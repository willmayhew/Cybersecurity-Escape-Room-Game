﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zxcvbn
{
    /// <summary>
    /// Useful shared Linq extensions
    /// </summary>
    static class LinqExtensions
    {
        /// <summary>
        /// Used to group elements by a key function, but only where elements are adjacent
        /// </summary>
        /// <param name="keySelector">Function used to choose the key for grouping</param>
        /// <param name="source">THe enumerable being grouped</param>
        /// <returns>An enumerable of <see cref="AdjacentGrouping{TKey, TElement}"/> </returns>
        /// <typeparam name="TKey">Type of key value used for grouping</typeparam>
        /// <typeparam name="TSource">Type of elements that are grouped</typeparam>
        public static IEnumerable<AdjacentGrouping<TKey, TSource>> GroupAdjacent<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var prevKey = default(TKey);
            var prevStartIndex = 0;
            var prevInit = false;
            var itemsList = new List<TSource>();

            var i = 0;
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (prevInit)
                {
                    if (!prevKey.Equals(key))
                    {
                        yield return new AdjacentGrouping<TKey, TSource>(key, itemsList, prevStartIndex, i - 1);

                        prevKey = key;
                        itemsList = new List<TSource>();
                        itemsList.Add(item);
                        prevStartIndex = i;
                    }
                    else
                    {
                        itemsList.Add(item);
                    }
                }
                else
                {
                    prevKey = key;
                    itemsList.Add(item);
                    prevInit = true;
                }

                i++;
            }

            if (prevInit) yield return new AdjacentGrouping<TKey, TSource>(prevKey, itemsList, prevStartIndex, i - 1); ;
        }

        /// <summary>
        /// A single grouping from the GroupAdjacent function, includes start and end indexes for the grouping in addition to standard IGrouping bits
        /// </summary>
        /// <typeparam name="TElement">Type of grouped elements</typeparam>
        /// <typeparam name="TKey">Type of key used for grouping</typeparam>
        public class AdjacentGrouping<TKey, TElement> :  IGrouping<TKey, TElement>, IEnumerable<TElement>
        {
            /// <summary>
            /// The key value for this grouping
            /// </summary>
            public TKey Key
            {
                get;
                private set;
            }

            /// <summary>
            /// The start index in the source enumerable for this group (i.e. index of first element)
            /// </summary>
            public int StartIndex
            {
                get;
                private set;
            }

            /// <summary>
            /// The end index in the enumerable for this group (i.e. the index of the last element)
            /// </summary>
            public int EndIndex
            {
                get;
                private set;
            }

            private IEnumerable<TElement> m_groupItems;

            internal AdjacentGrouping(TKey key, IEnumerable<TElement> groupItems, int startIndex, int endIndex)
            {
                this.Key = key;
                this.StartIndex = startIndex;
                this.EndIndex = endIndex;
                m_groupItems = groupItems;
            }

            private AdjacentGrouping() { }

            IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
            {
                return m_groupItems.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return m_groupItems.GetEnumerator();
            }
        }
    }
}
