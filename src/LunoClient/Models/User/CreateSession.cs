using Newtonsoft.Json;

namespace Luno.Models.User
{
	public class CreateSession<T>
	{
		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public T Details { get; set; }
	}
}
