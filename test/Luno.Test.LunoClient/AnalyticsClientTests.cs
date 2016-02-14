using System;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Test.LunoClient.Helpers;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class AnalyticsClientTests
	{
		public static readonly Random Random = new Random(0xdead);

		[Fact]
		public async Task Get_User_Analytics_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var userAnalytics = await client.Analytics.GetUserAnalytics(new[] { "total", "4" });

			Assert.True(userAnalytics.ContainsKey("total"));
			Assert.True(userAnalytics.ContainsKey("4_days"));
		}

		[Fact]
		public async Task Get_Session_Analytics_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var userAnalytics = await client.Analytics.GetSessionAnalytics(new[] { "total", "4" });

			Assert.True(userAnalytics.ContainsKey("total"));
			Assert.True(userAnalytics.ContainsKey("4_days"));
		}
	}
}
