using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HashGuessing
{
	public class Program
	{

		public static void Main(string[] args)
		{

			var myApp = new Program();
			var components = myApp.Initialize(args);


			Console.WriteLine("Go for a walk, this may take a while.");

			var hasher = SHA256.Create();

			var fullString = components.GetAllValues();
			Console.WriteLine($"Computing hash for string: [{fullString}]");
			var hashedBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(fullString));
			var stringBuilder = new StringBuilder();
			foreach (var hexByte in hashedBytes)
			{
				stringBuilder.Append(hexByte.ToString("x2"));
			}
			Console.WriteLine(stringBuilder);

			Console.WriteLine("End of program");
			Console.ReadKey();
		}

		public Components Initialize(string[] args)
		{
			var components = new Components();

			if (args.Length != 2)
			{
				Console.WriteLine("usage: HashGuessing [filename with hash components] [filename with hash to match]");
				Console.ReadKey();
				return null;
			}

			var fileComponents = new FileInfo(args[0]);
			var fileHash = new FileInfo(args[1]);

			if (!fileComponents.Exists || !fileHash.Exists)
			{
				Console.WriteLine("Unable to find one of the input files!");
				Console.ReadKey();
				return null;
			}

			var streamReader = new StreamReader(fileComponents.Name);

			while (!streamReader.EndOfStream)
			{
				components.Values.Add(streamReader.ReadLine());
			}

			streamReader = new StreamReader(fileHash.Name);
			components.HashToFind = streamReader.ReadToEnd();

			Console.WriteLine("Starting program with these components: ");
			Console.WriteLine(components.GetAllValues());

			Console.WriteLine("Looking for this hash: ");
			Console.WriteLine(components.HashToFind);

			return components;
		}
	}
}
