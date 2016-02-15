using Newtonsoft.Json;

namespace Luno.Models.Session
{
	public class ValidateSession<T>
	{
		[JsonProperty("key")]
		public string Key { get; set; }
		
		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("user_agent")]
		public string UserAgent { get; set; }

		[JsonProperty("details")]
		public T Details { get; set; }
	}
}
