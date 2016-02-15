using System;
using System.Linq;
using System.Threading.Tasks;
using Luno.Test.LunoClient.Helpers;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class EventClientTest
	{
		public static readonly Random Random = new Random();

		[Fact]
		public async Task Get_Events_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var events = await client.Events.GetAllAsync<EventStorage, Profile>();
		}

		[Fact]
		public async Task Get_Event_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var events = await client.Events.GetAllAsync<EventStorage, Profile>();
			var @event = await client.Events.GetAsync<EventStorage, Profile>(events.List.First().Id);
		}
	}
}
