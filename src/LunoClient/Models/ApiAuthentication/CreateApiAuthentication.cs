using System;
using Newtonsoft.Json;

namespace Luno.Models.ApiAuthentication
{
	public class CreateApiAuthentication<TApiAuthentication>
	{
		[JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
		public string UserId { get; set; }

		[JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key { get; set; }
		
		[JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
		public string Secret { get; set; }

		[JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public TApiAuthentication Details { get; set; }
	}
}
