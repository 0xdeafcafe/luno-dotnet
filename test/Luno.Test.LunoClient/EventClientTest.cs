using System;
using System.Linq;
using System.Threading.Tasks;
using Luno.Models.Event;
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

		[Fact]
		public async Task Create_And_Delete_Event_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var @event = await client.User.CreateEventAsync<EventStorage, Profile>(user.Id, new CreateEvent<EventStorage> { Name = "Unit Test Example Event" });
			var deletedEvent = await client.Events.DeleteAsync(@event.Id);

			Assert.True(deletedEvent.Success);
		}
	}
}
