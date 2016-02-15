using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class ApiAuthenticationStorage
	{
		[JsonProperty("access")]
		public string Access { get; set; }

		[JsonProperty("secondary_access")]
		public string SecondaryAccess { get; set; }
	}
}
