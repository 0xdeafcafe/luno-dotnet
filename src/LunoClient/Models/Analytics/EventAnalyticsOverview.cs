using System;
using Newtonsoft.Json;

namespace Luno.Models.Analytics
{
	public class EventAnalyticsOverview
	{
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty("range")]
		public AnalyticRange Range { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }
	}
}
