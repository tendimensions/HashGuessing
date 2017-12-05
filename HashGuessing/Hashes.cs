using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HashGuessing
{
	public class Hashes
	{

		public static string Md5Hash(string toHash)
		{
			var hasher = MD5.Create();
			var computedHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(toHash));

			var result = new StringBuilder();
			foreach (var b in computedHash)
			{
				result.Append(b.ToString("x2"));
			}

			return result.ToString();
		}

		public static string Sha256Hash(string toHash)
		{
			var hasher = SHA256.Create();
			var computedHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(toHash));

			var result = new StringBuilder();
			foreach (var b in computedHash)
			{
				result.Append(b.ToString("x2"));
			}

			return result.ToString();

		}

		public static string Sha1Hash(string toHash)
		{
			var hasher = SHA1.Create();
			var computedHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(toHash));

			var result = new StringBuilder();
			foreach (var b in computedHash)
			{
				result.Append(b.ToString("x2"));
			}

			return result.ToString();

		}

		public static string Sha512Hash(string toHash)
		{
			var hasher = SHA512.Create();
			var computedHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(toHash));

			var result = new StringBuilder();
			foreach (var b in computedHash)
			{
				result.Append(b.ToString("x2"));
			}

			return result.ToString();

		}


	}
}
