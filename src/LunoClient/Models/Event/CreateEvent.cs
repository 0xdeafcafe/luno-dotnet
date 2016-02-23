using System;
using Newtonsoft.Json;

namespace Luno.Models.Event
{
	public class CreateEvent<TEvent>
	{
		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Timestamp { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public TEvent Details { get; set; }
	}
}
