using System;
using System.Linq;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Models.ApiAuthentication;
using Luno.Models.Event;
using Luno.Models.User;
using Luno.Test.LunoClient.Extensions;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class UserClientTests
	{
		public readonly string[] FirstNameCollection = { "Alex", "George", "Ryan", "Hannah", "Shad", "Jade", "James", "Kaelan", "Laura", "Simion", "Robin", "Simon" };
		public readonly string[] LastNameCollection = { "Forbes-Reed", "Miller", "Licchelli", "Mayes", "Mugal", "Stanger", "Billingham", "Fouwels", "Corlett", "Putina", "Johnson", "Tabor" };
		
		[Fact]
		public async Task CreateAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var firstName = FirstNameCollection.GetRandom(random);
			var lastName = LastNameCollection.GetRandom(random);

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = firstName,
				LastName = lastName,
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			});
			var deletionResponse = await client.User.DeleteAsync(createdUser.Id);

			Assert.True(createdUser.FirstName == firstName);
			Assert.True(createdUser.LastName == lastName);
			Assert.True(deletionResponse.Success);
		}

		[Fact]
		public async Task CreateGetAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			});
			var queriedUser = await client.User.GetAsync<Profile>(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(queriedUser.Id == createdUser.Id);
		}

		[Fact]
		public async Task CreateUpdateAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./",
				Profile = new Profile
				{
					Field1 = "data a",
					Field2 = "data b"
				}
			});
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
		public async Task CreateUpdateAndDeleteUserDestructiveTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./",
				Profile = new Profile
				{
					Field1 = "data a",
					Field2 = "data b"
				}
			});
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
		public async Task CreateUserCreateEventAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			var @event = new CreateEvent<EventStorage> { Name = "Purchased Ticket", Details = new EventStorage { TickedId = Guid.NewGuid() } };
			var createdEvent = await client.User.CreateEventAsync<EventStorage, Profile>(createdUser.Id, @event);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(createdEvent.Name == @event.Name);
			Assert.True(createdEvent.Details.TickedId == @event.Details.TickedId);
		}

		[Fact]
		public async Task CreateLoginAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			var loginResponse = await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(loginResponse.Session.User.Id == createdUser.Id);
		}

		[Fact]
		public async Task CreateLoginGetSessionsAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.User.GetSessionsAsync<SessionStorage, Profile>(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(sessions.List.Count == 3);
		}

		[Fact]
		public async Task CreateLoginDeleteSessionsAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			await client.User.LoginAsync<Profile, SessionStorage>(user.Email, user.Password);
			var sessions = await client.User.GetSessionsAsync<SessionStorage, Profile>(createdUser.Id);
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);
			
			Assert.True(sessionDeletionResponse.Count == sessions.List.Count);
		}

		[Fact]
		public async Task CreateUserCreateSessionDeleteSessionAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			});
			var session = await client.User.CreateSessionAsync<SessionStorage, Profile>(createdUser.Id, expand: new[] { "user" });
			var sessionDeletionResponse = await client.User.DeleteSessionsAsync(createdUser.Id);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(sessionDeletionResponse.Count == 1);
		}

		[Fact]
		public async Task CreateUserValidatePasswordAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			var validatePasswordResponse = await client.User.ValidatePassword(createdUser.Id, user.Password);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
		}

		[Fact]
		public async Task CreateUserChangeAndValidatePasswordAndDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			};
			var createdUser = await client.User.CreateAsync(user);
			var newPassword = "12345qwerty[]'#";
			var changePasswordResponse = await client.User.ChangePassword(createdUser.Id, newPassword);
			var validatePasswordResponse = await client.User.ValidatePassword(createdUser.Id, newPassword);
			await client.User.DeleteAsync(createdUser.Id);

			Assert.True(validatePasswordResponse.Success);
			Assert.True(changePasswordResponse.Success);
		}

		[Fact]
		public async Task CreateUserCreateApiAuthenticationDeleteUserTestAsync()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			var random = new Random();

			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var createdUser = await client.User.CreateAsync(new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = LastNameCollection.GetRandom(random),
				Email = $"test.{random.Next(10000, 99999)}@outlook.com",
				Username = $"test.{random.Next(10000, 99999)}",
				Password = "12345qwerty,./"
			});
			var apiAuthentication = await client.User.CreateApiAuthenticationAsync<ApiAuthenticationStorage, Profile>(createdUser.Id, new CreateApiAuthentication<ApiAuthenticationStorage> { Details = new ApiAuthenticationStorage { Access = "ultra swag" } });
			var apiAuthentications = await client.User.GetAllApiAuthenticationsAsync<ApiAuthenticationStorage, Profile>(createdUser.Id, new[] { "user" });
			await client.User.DeleteAsync(createdUser.Id);

			Assert.NotNull(apiAuthentications.List.First(a => a.Key == apiAuthentication.Key));
			Assert.True(apiAuthentications.List.First(a => a.Key == apiAuthentication.Key).Details.Access == "ultra swag");
		}
	}
}
