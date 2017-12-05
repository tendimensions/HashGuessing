using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HashGuessing
{
	public class Components
	{
		public List<string> Values { get; set; }
		public string HashToFind { get; set; }

		public Components()
		{
			Values = new List<string>();
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
	}
}
