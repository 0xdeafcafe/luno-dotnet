using System;
using Newtonsoft.Json;

namespace Luno.Models.Analytics
{
	public class EventAnalyticsOverview
	{
		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("last")]
		public DateTime Last { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
