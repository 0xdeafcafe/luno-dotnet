using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class Profile
	{
		[JsonProperty("field_1")]
		public string Field1 { get; set; }

		[JsonProperty("field_2")]
		public string Field2 { get; set; }
	}
}
