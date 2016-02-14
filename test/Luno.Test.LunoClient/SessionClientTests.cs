using System;
using System.Threading.Tasks;
using Luno.Test.LunoClient.Helpers;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class SessionClientTests
	{
		public static readonly Random Random = new Random(0xdead);

		[Fact]
		public async Task Get_Sessions_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var sessions = await client.Session.GetAsync<SessionStorage, Profile>();
		}
	}
}
