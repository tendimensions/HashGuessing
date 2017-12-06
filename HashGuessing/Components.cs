using System;
using System.Collections.Generic;
using System.Text;

namespace HashGuessing
{
	public class Components
	{
		public List<string> Values { get; set; }
		public string HashToFind { get; set; }
		public int PermutationsCount { get; set; }

		private IEnumerable<IEnumerable<string>> _permutations;


		public Components()
		{
			Values = new List<string>();
		}

		public void Initialize()
		{
			if ((Values.Count < 1) || string.IsNullOrEmpty(HashToFind))
			{
				throw new ArgumentException("Missing values in one or both text files.");
			}

			// As a check to make sure we've reached all possible combinations
			PermutationsCount = Factorial(Values.Count);

			_permutations = Values.Permutate();

		}

		public IEnumerable<IEnumerable<string>> GetPermutations()
		{
			return _permutations;
		}

		public IEnumerable<string> NextPermutation()
		{
			return _permutations.GetEnumerator().MoveNext() ? _permutations.GetEnumerator().Current : null;
		}

		private int Factorial(int valuesCount)
		{
			if (valuesCount == 1) return valuesCount;
			return Factorial(valuesCount - 1) * valuesCount;
		}


		public string GetAllValues()
		{
			var result = new StringBuilder();
			foreach (var s in Values)
			{
				result.Append(s);
			}

			return result.ToString();
		}


		public (string, string) FindHash()
		{
			var permutations = GetPermutations();
			var cursor = permutations.GetEnumerator();

			while (cursor.MoveNext())
			{
				var newString = new StringBuilder();
				foreach (var s in cursor.Current)
				{
					newString.Append(s);
				}
				Console.WriteLine($"Computing hashes for string: [{newString}]");

				var stringToMatch = newString.ToString();

				if (HashToFind.Equals(Hashes.Md5Hash(stringToMatch)))
				{
					return (stringToMatch, "MD5");
				}

				if (HashToFind.Equals(Hashes.Sha1Hash(stringToMatch)))
				{
					return (stringToMatch, "SHA1");
				}

				if (HashToFind.Equals(Hashes.Sha256Hash(stringToMatch)))
				{
					return (stringToMatch, "SHA256");
				}

				if (HashToFind.Equals(Hashes.Sha512Hash(stringToMatch)))
				{
					return (stringToMatch, "SHA512");
				}
			}

			return (null, null);
		}
	}
}
