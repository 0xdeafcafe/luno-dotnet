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
			var events = await client.Event.GetAllAsync<EventStorage, Profile>();
		}

		[Fact]
		public async Task Get_Event_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var events = await client.Event.GetAllAsync<EventStorage, Profile>();
			var @event = await client.Event.GetAsync<EventStorage, Profile>(events.List.First().Id);
		}

		[Fact]
		public async Task Create_Update_And_Delete_Event_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var @event = await client.User.CreateEventAsync<EventStorage, Profile>(user.Id, new CreateEvent<EventStorage> { Name = "Unit Test Example Event" });
			await client.Event.UpdateAsync(@event.Id, new EventStorage { TickedId = Guid.NewGuid() });
			var updatedEvent = await client.Event.GetAsync<EventStorage, Profile>(@event.Id);
			var deletedEvent = await client.Event.DeleteAsync(@event.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.True(@event.Details.TickedId != updatedEvent.Details.TickedId);
		}

		[Fact]
		public async Task Create_Update_And_Delete_Event_Destructive_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var @event = await client.User.CreateEventAsync<EventStorage, Profile>(user.Id, new CreateEvent<EventStorage> { Name = "Unit Test Example Event", Details = new EventStorage { SecondField = "sample" } });
			await client.Event.UpdateAsync(@event.Id, new EventStorage { TickedId = Guid.NewGuid() }, destructive: true);
			var updatedEvent = await client.Event.GetAsync<EventStorage, Profile>(@event.Id);
			var deletedEvent = await client.Event.DeleteAsync(@event.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.Null(updatedEvent.Details.SecondField);
		}

		[Fact]
		public async Task Create_And_Delete_Event_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var @event = await client.User.CreateEventAsync<EventStorage, Profile>(user.Id, new CreateEvent<EventStorage> { Name = "Unit Test Example Event" });
			var deletedEvent = await client.Event.DeleteAsync(@event.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.True(deletedEvent.Success);
		}
	}
}
