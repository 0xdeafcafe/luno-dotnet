using System;
using System.Threading.Tasks;
using Luno.Test.LunoClient.Helpers;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class ApiAuthenticationClientTest
	{
		public static readonly Random Random = new Random();
		
		[Fact]
		public async Task Create_User_Api_Authentication_And_Get_Api_Authentication_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var apiAuth = await client.User.CreateApiAuthenticationAsync<ApiAuthenticationStorage, Profile>(user.Id);
			var gottenApiAuth = await client.ApiAuthentication.GetAsync<ApiAuthenticationStorage, Profile>(apiAuth.Key);

			Assert.True(user.Id == gottenApiAuth.User.Id);
			Assert.True(apiAuth.Key == gottenApiAuth.Key);
		}
	}
}
