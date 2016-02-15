using System;
using System.Threading.Tasks;
using Luno.Models.ApiAuthentication;
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
		
		[Fact]
		public async Task Create_Update_And_Delete_Api_Authentication_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var apiAuth = await client.User.CreateApiAuthenticationAsync<ApiAuthenticationStorage, Profile>(user.Id, new CreateApiAuthentication<ApiAuthenticationStorage> { Details = new ApiAuthenticationStorage { Access = "full" } });
			await client.ApiAuthentication.UpdateAsync(apiAuth.Key, new ApiAuthenticationStorage { Access = "semi" });
			var updatedApiAuth = await client.ApiAuthentication.GetAsync<ApiAuthenticationStorage, Profile>(apiAuth.Key);
			//await client.ApiAuthentication.DeleteAsync(apiAuth.Key); TODO: this
			await client.User.DeactivateAsync(user.Id);

			Assert.True(apiAuth.Details.Access != updatedApiAuth.Details.Access);
		}

		[Fact]
		public async Task Create_Update_And_Delete_Api_Authentication_Destructive_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var apiAuth = await client.User.CreateApiAuthenticationAsync<ApiAuthenticationStorage, Profile>(user.Id, new CreateApiAuthentication<ApiAuthenticationStorage> { Details = new ApiAuthenticationStorage { Access = "full" } });
			await client.ApiAuthentication.UpdateAsync(apiAuth.Key, new ApiAuthenticationStorage { SecondaryAccess = "full" }, destructive: true);
			var updatedApiAuth = await client.ApiAuthentication.GetAsync<ApiAuthenticationStorage, Profile>(apiAuth.Key);
			//await client.ApiAuthentication.DeleteAsync(apiAuth.Key); TODO: this
			await client.User.DeactivateAsync(user.Id);

			Assert.Null(updatedApiAuth.Details.Access);
			Assert.True(updatedApiAuth.Details.SecondaryAccess == "full");
		}
	}
}
