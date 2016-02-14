using Newtonsoft.Json;

namespace Luno.Models.User
{
	internal class UpdateSession<T>
	{
		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details")]
		public T Details { get; set; }
	}
}
