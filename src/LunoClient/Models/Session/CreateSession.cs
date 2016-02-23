using System;
using Newtonsoft.Json;

namespace Luno.Models.Session
{
	public class CreateSession<T>
	{
		[JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
		public string UserId { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("expires")]
		public DateTime? Expires { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public T Details { get; set; }
	}
}
