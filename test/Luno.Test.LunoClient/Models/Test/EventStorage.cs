using System;
using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class EventStorage
	{
		[JsonProperty("ticket_id")]
		public Guid TickedId { get; set; }
	}
}
