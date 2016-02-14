using System;
using Luno.Models.User;
using Newtonsoft.Json;

namespace Luno.Models.ApiAuthentication
{
	public class ApiAuthentication<TApiAuthentication, TUser>
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("secret")]
		public string Secret { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("details")]
		public TApiAuthentication Details { get; set; }

		[JsonProperty("user")]
		public User<TUser> User { get; set; }

	}
}
