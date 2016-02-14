using System;
using System.Threading.Tasks;
using Luno.Models.User;
using Luno.Test.LunoClient.Helpers;
using Luno.Test.LunoClient.Models.Test;
using Xunit;

namespace Luno.Test.LunoClient
{
	public class SessionClientTests
	{
		public static readonly Random Random = new Random(0xdead);

		[Fact]
		public async Task Get_Sessions_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var sessions = await client.Session.GetAllAsync<SessionStorage, Profile>();
		}

		[Fact]
		public async Task Get_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var sessionA = await client.User.CreateSessionAsync<SessionStorage, Profile>(user.Id);
			var sessionB = await client.Session.GetAsync<SessionStorage, Profile>(sessionA.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.True(sessionA.Key == sessionB.Key);
		}

		[Fact]
		public async Task Create_Session_And_Update_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.User.CreateSessionAsync<SessionStorage, Profile>(user.Id, new CreateSession<SessionStorage> { Details = new SessionStorage { Test1 = "swag" } });
			session.Details = new SessionStorage { Test2 = "sample" };
			await client.Session.UpdateAsync(session.Id, session);
			var updatedSession = await client.Session.GetAsync<SessionStorage, Profile>(session.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.Null(session.Details.Test1);
			Assert.True(updatedSession.Details.Test2 == "sample");
		}
	}
}
