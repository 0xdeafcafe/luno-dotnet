using Newtonsoft.Json;

namespace LunoClient.Test.Models.Test
{
	public class Profile
	{
		[JsonProperty("field_1")]
		public string Field1 { get; set; }

		[JsonProperty("field_2")]
		public string Field2 { get; set; }

		[JsonProperty("field_3")]
		public string Field3 { get; set; }
	}
}
