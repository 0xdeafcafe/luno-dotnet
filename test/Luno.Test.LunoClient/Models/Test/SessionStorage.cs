﻿using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class SessionStorage
	{
		[JsonProperty("test_1")]
		public string Test1 { get; set; }

		[JsonProperty("test_2")]
		public string Test2 { get; set; }
	}
}
