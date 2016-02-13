using Newtonsoft.Json;

namespace Luno.Models.Error
{
	public class Extra
	{
		[JsonProperty("param")]
		public string Param { get; set; }

		[JsonProperty("in")]
		public string In { get; set; }

		[JsonProperty("received")]
		public string Received { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }
	}
}
