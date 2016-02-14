using System;
using System.Threading.Tasks;
using Luno.Connections;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class AnalyticsClientTests
	{
		public static readonly Random Random = new Random(0xdead);

		[Fact]
		public async Task Get_User_Analytics_Test_Async()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var userAnalytics = await client.Analytics.GetUserAnalytics(new[] { "total", "4" });

			Assert.True(userAnalytics.ContainsKey("total"));
			Assert.True(userAnalytics.ContainsKey("4_days"));
		}
	}
}
