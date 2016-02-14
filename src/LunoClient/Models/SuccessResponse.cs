using Newtonsoft.Json;

namespace Luno.Models
{
	public class SuccessResponse
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("count")]
		public int? Count { get; set; }
	}
}
