using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luno.Models
{
	public class ErrorResponse
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("status")]
		public int Status { get; set; }

		[JsonProperty("extra")]
		public Dictionary<string, string> Extra { get; set; }
	}
}
