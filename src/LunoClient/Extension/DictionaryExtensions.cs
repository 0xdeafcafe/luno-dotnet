using System;
using System.Collections.Generic;
using System.Linq;

namespace Luno.Extension
{
	public static class DictionaryExtensions
	{
		public static string ToQueryString(this IDictionary<string, string> dictionary)
		{
			var queryStringArray = dictionary
				.Select(d => $"{Uri.EscapeDataString(d.Key)}={Uri.EscapeDataString(d.Value)}").ToArray();

			return string.Join("&", queryStringArray);
		}
	}
}
