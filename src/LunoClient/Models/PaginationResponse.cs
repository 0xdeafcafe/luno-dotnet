using System.Collections.Generic;
using Luno.Models.Pagination;
using Newtonsoft.Json;

namespace Luno.Models
{
	public class PaginationResponse<T>
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("list")]
		public List<T> List { get; set; }

		[JsonProperty("page")]
		public PageNavigation Page { get; set; }
	}
}
