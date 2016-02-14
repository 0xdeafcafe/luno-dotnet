using System.Collections.Generic;
using Luno.Models.Analytics;
using Newtonsoft.Json;

namespace Luno.Models
{
	public class AnalyticsListResponse<T>
	{
		[JsonProperty("list")]
		public List<T> List { get; set; }

		[JsonProperty("all")]
		public EventAnalyticsOverview All { get; set; }
	}
}
