using System;
using Newtonsoft.Json;

namespace Luno.Models.Analytics
{
	public class EventAnalytic
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("last")]
		public DateTime Last { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
