using System;
using Newtonsoft.Json;

namespace Luno.Models.Analytics
{
	public class TimelineEntry
	{
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }
	}
}
