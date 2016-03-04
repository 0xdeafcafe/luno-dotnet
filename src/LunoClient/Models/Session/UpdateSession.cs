using System;
using Newtonsoft.Json;

namespace Luno.Models.User
{
	internal class UpdateSession<T>
	{
		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("expires")]
		public DateTime? Expires { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details")]
		public T Details { get; set; }
	}
}
