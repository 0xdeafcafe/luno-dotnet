using Newtonsoft.Json;

namespace LunoClient.Test.Models.Test
{
	public class ApiAuthenticationStorage
	{
		[JsonProperty("access")]
		public string Access { get; set; }

		[JsonProperty("secondary_access")]
		public string SecondaryAccess { get; set; }
	}
}
