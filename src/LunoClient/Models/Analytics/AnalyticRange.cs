using System;
using Newtonsoft.Json;

namespace Luno.Models.Analytics
{
	public class AnalyticRange
	{
		[JsonProperty("from")]
		public DateTime From { get; set; }

		[JsonProperty("to")]
		public DateTime To { get; set; }
	}
}
