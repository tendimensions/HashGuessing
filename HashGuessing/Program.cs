using System;
using System.IO;

namespace HashGuessing
{
	public class Program
	{

		public static void Main(string[] args)
		{

			var myApp = new Program();
			var components = myApp.Initialize(args);

			if (components == null) return;

			Console.WriteLine($"Here we go... we're working on {components.PermutationsCount} different strings.");


			var matchingString = components.FindHash();

			if (string.IsNullOrEmpty(matchingString.Item1) || string.IsNullOrEmpty(matchingString.Item2))
			{
				Console.WriteLine("Not found!");
			}
			else
			{
				Console.WriteLine($"Holy shit, we found a match! String: {matchingString.Item1} produced our hash using {matchingString.Item2}");
			}

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
			foreach (var s in components.Values)
			{
				Console.WriteLine(s);
			}

			Console.WriteLine();
			Console.WriteLine("Looking for this hash: ");
			Console.WriteLine(components.HashToFind);

			Console.WriteLine();
			components.Initialize();

			return components;
		}
	}
}
