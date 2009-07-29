using System;
using System.Collections.Generic;

namespace _2HourGame {
    static class LinqExtensions {
        // http://blogs.msdn.com/ericlippert/archive/2009/05/07/zip-me-up.aspx
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector) {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            return ZipIterator(first, second, resultSelector);
        }

        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector) {
            using (IEnumerator<TFirst> e1 = first.GetEnumerator()) {
                using (IEnumerator<TSecond> e2 = second.GetEnumerator()) {
                    while (e1.MoveNext() && e2.MoveNext()) {
                        yield return resultSelector(e1.Current, e2.Current);
                    }
                }
            }
        }

        public static IEnumerable<TResult> Zip3<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second,  IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector) {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (third == null) throw new ArgumentNullException("third");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            return Zip3Iterator(first, second, third, resultSelector);
        }

        private static IEnumerable<TResult> Zip3Iterator<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector) {
            using (IEnumerator<TFirst> e1 = first.GetEnumerator()) {
                using (IEnumerator<TSecond> e2 = second.GetEnumerator()) {
                    using (IEnumerator<TThird> e3 = third.GetEnumerator()) {
                        while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext()) {
                            yield return resultSelector(e1.Current, e2.Current, e3.Current);
                        }
                    }
                }
            }
        }
    }
}
