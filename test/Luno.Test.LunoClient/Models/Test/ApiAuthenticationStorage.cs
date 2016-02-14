using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Luno.Test.LunoClient.Models.Test
{
	public class ApiAuthenticationStorage
	{
		[JsonProperty("access")]
		public string Access { get; set; }
	}
}
