using System;
using System.Threading.Tasks;
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
			var userAnalytics = await client.Analytics.GetUserAnalyticsAsync(new[] { "total", "4" });

			Assert.True(userAnalytics.ContainsKey("total"));
			Assert.True(userAnalytics.ContainsKey("4_days"));
		}

		[Fact]
		public async Task Get_Session_Analytics_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var sessionAnalytics = await client.Analytics.GetSessionAnalyticsAsync(new[] { "total", "5" });

			Assert.True(sessionAnalytics.ContainsKey("total"));
			Assert.True(sessionAnalytics.ContainsKey("5_days"));
		}

		[Fact]
		public async Task Get_Events_Analytics_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var eventAnalytics = await client.Analytics.GetEventAnalyticsAsync(new[] { "total", "11" });

			Assert.True(eventAnalytics.ContainsKey("total"));
			Assert.True(eventAnalytics.ContainsKey("11_days"));
		}

		[Fact]
		public async Task Get_Events_Analytics_List_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var eventAnalyticsList = await client.Analytics.GetEventAnalyticsListAsync();
		}

		[Fact]
		public async Task Get_Events_Analytics_Timeline_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var eventAnalyticsTimeline = await client.Analytics.GetEventAnalyticsTimelineAsync();
		}
	}
}
