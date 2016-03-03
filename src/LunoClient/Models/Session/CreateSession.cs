using System;
using Luno.Converters;
using Newtonsoft.Json;

namespace Luno.Models.Session
{
	public class CreateSession<T>
	{
		[JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
		public string UserId { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		/// <summary>
		/// Secure session key.
		/// </summary>
		/// <remarks>
		/// Defaults to a 32-byte cryptographically secure hexadecimal string
		/// </remarks>
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("expires")]
		[JsonConverter(typeof(JsonDateTimeConverter))]
		public DateTime? Expires { get; set; } = DateTime.UtcNow.AddDays(14);

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public T Details { get; set; }
	}
}
