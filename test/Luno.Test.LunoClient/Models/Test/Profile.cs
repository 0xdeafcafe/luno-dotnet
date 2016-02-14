using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class Profile
	{
		[JsonProperty("field_1", NullValueHandling = NullValueHandling.Ignore)]
		public string Field1 { get; set; }

		[JsonProperty("field_2", NullValueHandling = NullValueHandling.Ignore)]
		public string Field2 { get; set; }

		[JsonProperty("field_3", NullValueHandling = NullValueHandling.Ignore)]
		public string Field3 { get; set; }
	}
}
