using System;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Models.User;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class SimpleTests
	{
		[Fact]
		public async Task SimpleTest1()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");
			
			var connection = new ApiKeyConnection(key, secret);
			var client = new Luno.LunoClient(connection);
			var user = await client.User.CreateAsync(new CreateUser<Profile>
			{
				Name = "Alexander Forbes-Reed",
				FirstName = "Alexander",
				LastName = "Forbes-Reed",
				Email = "alexforbesreed@outlook.com",
				Username = "0xdeafcafe",
				Password = "12345qwerty,./",
				Profile = new Profile
				{
					Field1 = "data a",
					Field2 = "data b"
				}
			});
			var response = await client.User.GetAllAsync<Profile>();

			Assert.True(true);
		}
	}
}
