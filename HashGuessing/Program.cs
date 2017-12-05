using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HashGuessing
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("usage: HashGuessing [filename with hash components] [filename with hash to match]");
				Console.ReadKey();
				return;
			}

			var fileComponents = new FileInfo(args[0]);
			var fileHash = new FileInfo(args[1]);

			if (!fileComponents.Exists || !fileHash.Exists)
			{
				Console.WriteLine("Unable to find one of the input files!");
				Console.ReadKey();
				return;
			}

			var streamReader = new StreamReader(fileComponents.Name);

			var components = new List<string>();
			while (!streamReader.EndOfStream)
			{
				components.Add(streamReader.ReadLine());
			}

			string hashToFind;
			streamReader = new StreamReader(fileHash.Name);
			hashToFind = streamReader.ReadToEnd();

			Console.WriteLine("Starting program with these components: ");
			foreach (string s in components)
			{
				Console.WriteLine(s);
			}

			Console.WriteLine("Looking for this hash: ");
			Console.WriteLine(hashToFind);

			Console.WriteLine("Go for a walk, this may take a while.");

			var hasher = SHA256.Create();

			var fullString = new StringBuilder();
			foreach (var line in components)
			{
				fullString.Append(line);
			}

			Console.WriteLine($"Computing hash for string: [{fullString}]");
			var hashedBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(fullString.ToString()));
			var stringBuilder = new StringBuilder();
			foreach (var hexByte in hashedBytes)
			{
				stringBuilder.Append(hexByte.ToString("x2"));
			}
			Console.WriteLine(stringBuilder);

			Console.WriteLine("End of program");
			Console.ReadKey();
		}
	}
}
