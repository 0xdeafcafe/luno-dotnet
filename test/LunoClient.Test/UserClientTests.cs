﻿using System;
using System.Threading.Tasks;
using Luno.Enums;
using Luno.Models.Event;
using Luno.Models.Session;
using LunoClient.Test.Helpers;
using LunoClient.Test.Models.Test;
using Xunit;

namespace LunoClient.Test
{
	public class UserClientTests
	{
		public static readonly Random Random = new Random();

		[Fact]
		public async Task Create_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var deletionResponse = await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(createdUser.FirstName == user.FirstName);
			Assert.True(createdUser.LastName == user.LastName);
			Assert.True(deletionResponse.Success);
		}

		[Fact]
		public async Task Create_Get_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var queriedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(queriedUser.Id == createdUser.Id);
		}

		[Fact]
		public async Task Create_Get_By_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var gottenByIdUser = await client.User.GetByAsync<Profile>(UserSearchField.Id, createdUser.Id);
			var gottenByEmailUser = await client.User.GetByAsync<Profile>(UserSearchField.Email, createdUser.Email);
			var gottenByUsernameUser = await client.User.GetByAsync<Profile>(UserSearchField.Username, createdUser.Username);
			await client.User.DeactivateAsync(createdUser.Id);
		}

		[Fact]
		public async Task Create_Update_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			createdUser.FirstName = "UpdatedAlex";
			createdUser.LastName = "UpdatedForbes-Reed";
			createdUser.Profile = new Profile { Field3 = "swag" };
			await client.User.UpdateAsync(createdUser.Id, createdUser, false);
			var updatedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeactivateAsync(updatedUser.Id);

			Assert.True(updatedUser.FirstName == createdUser.FirstName);
			Assert.True(updatedUser.LastName == createdUser.LastName);
			Assert.True(updatedUser.Profile.Field3 == createdUser.Profile.Field3);
		}

		[Fact]
		public async Task Create_Update_And_Deactivate_User_Destructive_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random, new Profile { Field1 = "sample data" }));
			createdUser.FirstName = "UpdatedAlex";
			createdUser.LastName = "UpdatedForbes-Reed";
			createdUser.Profile = new Profile { Field3 = "swag" };
			await client.User.UpdateAsync(createdUser.Id, createdUser, true);
			var updatedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeactivateAsync(updatedUser.Id);

			Assert.True(updatedUser.FirstName == createdUser.FirstName);
			Assert.True(updatedUser.LastName == createdUser.LastName);
			Assert.True(updatedUser.Profile.Field3 == createdUser.Profile.Field3);
			Assert.Null(updatedUser.Profile.Field1);
		}

		[Fact]
		public async Task Create_User_Create_Event_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var @event = new CreateEvent<EventStorage> { UserId = createdUser.Id, Name = "Purchased Ticket", Details = new EventStorage { TickedId = Guid.NewGuid() } };
			var createdEvent = await client.Event.CreateAsync<EventStorage, Profile>(@event);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(createdEvent.Name == @event.Name);
			Assert.True(createdEvent.Details.TickedId == @event.Details.TickedId);
		}

		[Fact]
		public async Task Create_User_Create_Event_Get_Events_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.Event.CreateAsync<EventStorage, Profile>(new CreateEvent<EventStorage> { UserId = createdUser.Id, Name = "Purchased Ticket 1", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			await client.Event.CreateAsync<EventStorage, Profile>(new CreateEvent<EventStorage> { UserId = createdUser.Id, Name = "Purchased Ticket 2", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			await client.Event.CreateAsync<EventStorage, Profile>(new CreateEvent<EventStorage> { UserId = createdUser.Id, Name = "Purchased Ticket 3", Details = new EventStorage { TickedId = Guid.NewGuid() } });
			var events = await client.Event.GetAllAsync<EventStorage, Profile>(userId: createdUser.Id, expand: new[] { "user" });
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(events.List.Count == 4); // This includes the "user created" event, so it will be one more than the number of events we create
		}

		[Fact]
		public async Task Create_Login_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var loginResponse = await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(loginResponse.Session.User.Id == createdUser.Id);
		}

		[Fact]
		public async Task Create_Login_With_Session_Details_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var loginResponse = await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password, new CreateSession<SessionStorage>
			{
				Ip = "192.168.1.69",
				Details = new SessionStorage
				{
					Test1 = "swag"
				}
			});
			var session = await client.Session.GetAsync<SessionStorage, Profile>(loginResponse.Session.Id);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(loginResponse.Session.User.Id == createdUser.Id);
			Assert.True(session.Ip == "192.168.1.69");
			Assert.True(session.Details.Test1 == "swag");
		}

		[Fact]
		public async Task Create_Login_Get_Sessions_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.Session.GetAllAsync<SessionStorage, Profile>(createdUser.Id);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(sessions.List.Count == 3);
		}

		[Fact]
		public async Task Create_Login_Deactivate_Sessions_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.Session.GetAllAsync<SessionStorage, Profile>(createdUser.Id);
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(sessionDeletionResponse.Count == sessions.List.Count);
		}

		[Fact]
		public async Task Create_User_Create_Session_Deactivate_Session_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var createdUser = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.Session.CreateAsync<SessionStorage, Profile>(new CreateSession<SessionStorage> { UserId = createdUser.Id }, expand: new[] { "user" });
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(sessionDeletionResponse.Count == 1);
		}

		[Fact]
		public async Task Create_User_Validate_Password_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var validatePasswordResponse = await client.User.ValidatePasswordAsync(createdUser.Id, user.Password);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
		}

		[Fact]
		public async Task Create_User_Change_And_Validate_Password_And_Deactivate_User_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var newPassword = "12345qwerty[]'#";
			var changePasswordResponse = await client.User.ChangePasswordAsync(createdUser.Id, newPassword);
			var validatePasswordResponse = await client.User.ValidatePasswordAsync(createdUser.Id, newPassword);
			await client.User.DeactivateAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
			Assert.True(changePasswordResponse.Success);
		}

		[Fact]
		public async Task Create_User_Change_And_Validate_Password_And_Deactivate_User_2_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = Factory.GenerateCreateUser(Random);
			var createdUser = await client.User.CreateAsync(user);
			var newPassword = "12345qwerty[]'#";
			var changePasswordResponse = await client.User.ChangePasswordAsync(createdUser.Id, newPassword);
			changePasswordResponse = await client.User.ChangePasswordAsync(createdUser.Id, "67890uiop[]@~", currentPassword: newPassword);
			await client.User.DeactivateAsync(createdUser.Id);
			
			Assert.True(changePasswordResponse.Success);
		}
	}
}
