using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class SessionStorage
	{
		[JsonProperty("test")]
		public string Test { get; set; }
	}
}
