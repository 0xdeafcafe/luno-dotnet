using System;
using System.Threading.Tasks;
using Luno.Connections;
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
				Password = "12345qwerty,./",
				Profile = new Profile
				{
					Field1 = "data a",
					Field2 = "data b"
				}
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
				Password = "12345qwerty,./",
				Profile = new Profile
				{
					Field1 = "data a",
					Field2 = "data b"
				}
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
	}
}
