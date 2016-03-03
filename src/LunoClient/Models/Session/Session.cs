using System;
using Luno.Models.User;
using Newtonsoft.Json;

namespace Luno.Models.Session
{
	public class Session<TSession, TUser>
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("expires")]
		public DateTime? Expires { get; set; }

		[JsonProperty("last_access")]
		public DateTime? LastAccess { get; set; }

		[JsonProperty("access_count")]
		public int AccessCount { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details")]
		public TSession Details { get; set; }

		[JsonProperty("user")]
		public User<TUser> User { get; set; }
	}
}
