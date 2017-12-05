using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashGuessing
{
	public static class Extensions
	{
		public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
		{
			return k == 0 ? new[] { new T[0] } :
				elements.SelectMany((element, index) =>
					elements.Skip(index + 1).Combinations(k - 1).Select(combination => (new[] { element }).Concat(combination)));
		}

		public static IEnumerable<IEnumerable<T>> CombinationsExpanded<T>(this IEnumerable<T> elements, int k)
		{
			if (k == 0) return new[] { new T[0] };

			return elements.SelectMany((element, index) =>
				elements.Skip(index + 1).Combinations(k - 1).Select(combination => (new[] { element }).Concat((combination))));

		}


		public static IEnumerable<IEnumerable<T>> Permutate<T>(this IEnumerable<T> source)
		{
			var xs = source.ToArray();
			return
				xs.Length == 1
					? new[] { xs }
					: (
						from n in Enumerable.Range(0, xs.Length)
						let cs = xs.Skip(n).Take(1)
						let dss = Permutate<T>(xs.Take(n).Concat(xs.Skip(n + 1)))
						from ds in dss
						select cs.Concat(ds)
					).Distinct(new EnumerableEqualityComparer<T>());
		}

		private class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
		{
			public bool Equals(IEnumerable<T> a, IEnumerable<T> b)
			{
				return a.SequenceEqual(b);
			}

			public int GetHashCode(IEnumerable<T> t)
			{
				return t.Take(1).Aggregate(0, (a, x) => a ^ x.GetHashCode());
			}
		}
	}
}
