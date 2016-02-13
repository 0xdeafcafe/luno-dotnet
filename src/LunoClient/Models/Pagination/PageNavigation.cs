using Newtonsoft.Json;

namespace Luno.Models.Pagination
{
	public class PageNavigation
	{
		[JsonProperty("next")]
		public PageDatum Next { get; set; }

		[JsonProperty("previous")]
		public PageDatum Previous { get; set; }
	}
}
