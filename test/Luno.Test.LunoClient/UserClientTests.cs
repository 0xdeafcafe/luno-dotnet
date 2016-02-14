using System;
using System.Linq;
using System.Threading.Tasks;
using Luno.Models.ApiAuthentication;
using Luno.Models.Event;
using Luno.Test.LunoClient.Helpers;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class UserClientTests
	{
		public static readonly Random Random = new Random(0xbeef);

		[Fact]
		public async Task Create_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var deletionResponse = await client.User.DeleteAsync(createdUser.Id);

			Assert.True(createdUser.FirstName == user.FirstName);
			Assert.True(createdUser.LastName == user.LastName);
			Assert.True(deletionResponse.Success);
		}

		[Fact]
		public async Task Create_Get_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var queriedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(queriedUser.Id == createdUser.Id);
		}

		[Fact]
		public async Task Create_Update_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			createdUser.FirstName = "UpdatedAlex";
			createdUser.LastName = "UpdatedForbes-Reed";
			createdUser.Profile = new Profile { Field3 = "swag" };
			await client.User.UpdateAsync(createdUser.Id, createdUser, false);
			var updatedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeleteAsync(updatedUser.Id);

			Assert.True(updatedUser.FirstName == createdUser.FirstName);
			Assert.True(updatedUser.LastName == createdUser.LastName);
			Assert.True(updatedUser.Profile.Field3 == createdUser.Profile.Field3);
		}

		[Fact]
		public async Task Create_Update_And_Delete_User_Destructive_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random, new Profile { Field1 = "sample data" }));
			createdUser.FirstName = "UpdatedAlex";
			createdUser.LastName = "UpdatedForbes-Reed";
			createdUser.Profile = new Profile { Field3 = "swag" };
			await client.User.UpdateAsync(createdUser.Id, createdUser, true);
			var updatedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeleteAsync(updatedUser.Id);

			Assert.True(updatedUser.FirstName == createdUser.FirstName);
			Assert.True(updatedUser.LastName == createdUser.LastName);
			Assert.True(updatedUser.Profile.Field3 == createdUser.Profile.Field3);
			Assert.Null(updatedUser.Profile.Field1);
		}

		[Fact]
		public async Task Create_User_Create_Event_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var @event = new CreateEvent<EventStorage> { Name = "Purchased Ticket", Details = new EventStorage { TickedId = Guid.NewGuid() } };
			var createdEvent = await client.User.CreateEventAsync<EventStorage, Profile>(createdUser.Id, @event);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(createdEvent.Name == @event.Name);
			Assert.True(createdEvent.Details.TickedId == @event.Details.TickedId);
		}

		[Fact]
		public async Task Create_User_Create_Event_Get_Events_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.User.CreateEventAsync<EventStorage, Profile>(createdUser.Id, new CreateEvent<EventStorage> { Name = "Purchased Ticket 1", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			await client.User.CreateEventAsync<EventStorage, Profile>(createdUser.Id, new CreateEvent<EventStorage> { Name = "Purchased Ticket 2", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			await client.User.CreateEventAsync<EventStorage, Profile>(createdUser.Id, new CreateEvent<EventStorage> { Name = "Purchased Ticket 3", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			var events = await client.User.GetEventsAsync<EventStorage, Profile>(createdUser.Id, expand: new[] { "user" });
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(events.List.Count == 4); // This includes the "user created" event, so it will be one more than the number of events we create
		}

		[Fact]
		public async Task Create_Login_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var loginResponse = await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(loginResponse.Session.User.Id == createdUser.Id);
		}

		[Fact]
		public async Task Create_Login_Get_Sessions_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.User.GetSessionsAsync<SessionStorage, Profile>(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(sessions.List.Count == 3);
		}

		[Fact]
		public async Task Create_Login_Delete_Sessions_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.User.GetSessionsAsync<SessionStorage, Profile>(createdUser.Id);
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(sessionDeletionResponse.Count == sessions.List.Count);
		}

		[Fact]
		public async Task Create_User_Create_Session_Delete_Session_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.User.CreateSessionAsync<SessionStorage, Profile>(createdUser.Id, expand: new[] { "user" });
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(sessionDeletionResponse.Count == 1);
		}

		[Fact]
		public async Task Create_User_Validate_Password_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var validatePasswordResponse = await client.User.ValidatePassword(createdUser.Id, user.Password);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
		}

		[Fact]
		public async Task Create_User_Change_And_Validate_Password_And_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var newPassword = "12345qwerty[]'#";
			var changePasswordResponse = await client.User.ChangePassword(createdUser.Id, newPassword);
			var validatePasswordResponse = await client.User.ValidatePassword(createdUser.Id, newPassword);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
			Assert.True(changePasswordResponse.Success);
		}

		[Fact]
		public async Task Create_User_Create_Api_Authentication_Delete_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var apiAuthentication = await client.User.CreateApiAuthenticationAsync<ApiAuthenticationStorage, Profile>(createdUser.Id, new CreateApiAuthentication<ApiAuthenticationStorage> { Details = new ApiAuthenticationStorage { Access = "ultra swag" } });
			var apiAuthentications = await client.User.GetAllApiAuthenticationsAsync<ApiAuthenticationStorage, Profile>(createdUser.Id, new[] { "user" });
			await client.User.DeleteAsync(createdUser.Id);

			Assert.NotNull(apiAuthentications.List.First(a => a.Key == apiAuthentication.Key));
			Assert.True(apiAuthentications.List.First(a => a.Key == apiAuthentication.Key).Details.Access == "ultra swag");
		}
	}
}
