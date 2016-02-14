using System.Collections.Generic;
using Luno.Models.Analytics;
using Newtonsoft.Json;

namespace Luno.Models
{
	public class AnalyticsTimelineResponse
	{
		[JsonProperty("timeline")]
		public IList<TimelineEntry> Timeline { get; set; }

		[JsonProperty("total")]
		public int Total { get; set; }
	}
}
