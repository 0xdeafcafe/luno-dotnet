using System;
using Luno.Models.User;
using Newtonsoft.Json;

namespace Luno.Models.Event
{
	public class Event<TEvent, TUser>
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("details")]
		public TEvent Details { get; set; }

		[JsonProperty("user")]
		public User<TUser> User { get; set; }

	}
}
