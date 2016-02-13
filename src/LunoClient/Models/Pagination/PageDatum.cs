using Newtonsoft.Json;

namespace Luno.Models.Pagination
{
	public class PageDatum
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
