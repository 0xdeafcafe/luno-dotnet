using System;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Models.User;
using Luno.Test.LunoClient.Extensions;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class SimpleTests
	{
		public readonly string[] FirstNameCollection = { "Alex", "George", "Ryan", "Hannah", "Shad", "Jade", "James", "Kaelan", "Emma", "Simion", "Robin", "Simon" };
		public readonly string[] LastNameCollection = { "Forbes-Reed", "Miller", "Licchelli", "Mayes", "Mugal", "Stanger", "Billingham", "Fouwels", "Corlette", "Putina", "Johnson", "Tabor" };

		[Fact]
		public async Task SimpleTest1()
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
			var allUsers = await client.User.GetAllAsync<Profile>();

			Assert.True(true);
		}
	}
}
