using System;
using System.Threading.Tasks;
using Luno.Exceptions;
using Luno.Models.Session;
using Luno.Models.User;
using LunoClient.Test.Helpers;
using LunoClient.Test.Models.Test;
using Xunit;

namespace LunoClient.Test
{
	public class SessionClientTests
	{
		public static readonly Random Random = new Random();

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
			var sessionA = await client.Session.CreateAsync<SessionStorage, Profile>(new CreateSession<SessionStorage> { UserId = user.Id });
			var sessionB = await client.Session.GetAsync<SessionStorage, Profile>(sessionA.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.True(sessionA.Key == sessionB.Key);
		}

		[Fact]
		public async Task Create_Annonymous_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var session = await client.Session.CreateAsync<SessionStorage, Profile>();

			Assert.Null(session.User);
			Assert.NotNull(session.Id);
		}

		[Fact]
		public async Task Create_Session_And_Update_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.Session.CreateAsync<SessionStorage, Profile>(new CreateSession<SessionStorage> { UserId = user.Id, Details = new SessionStorage { Test1 = "swag" } });
			session.Details = new SessionStorage { Test2 = "sample" };
			await client.Session.UpdateAsync(session.Id, session);
			var updatedSession = await client.Session.GetAsync<SessionStorage, Profile>(session.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.Null(session.Details.Test1);
			Assert.True(updatedSession.Details.Test2 == "sample");
		}

		[Fact]
		public async Task Create_And_Delete_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.Session.CreateAsync<SessionStorage, Profile>(new CreateSession<SessionStorage> { UserId = user.Id });
			var sessionDeletionResponse = await client.Session.DeleteAsync(session.Id);
			await client.User.DeactivateAsync(user.Id);

			Assert.True(sessionDeletionResponse.Success);
		}

		[Fact]
		public async Task Create_Validate_And_Delete_Session_Test_Async()
		{
			var client = new Luno.LunoClient(Factory.GenerateApiKeyConnection());
			var user = await client.User.CreateAsync(Factory.GenerateCreateUser(Random));
			var session = await client.Session.CreateAsync<SessionStorage, Profile>(new CreateSession<SessionStorage> { UserId = user.Id });
			var validateValidSession = await client.Session.ValidateAsync(session);
			var sessionDeletionResponse = await client.Session.DeleteAsync(session.Id);
			try
			{
				await client.Session.ValidateAsync(session);
			}
			catch (LunoApiException ex)
			{
				Assert.True(ex.Code == "session_not_found");
				Assert.True(validateValidSession.User.Id == user.Id);
			}
			finally
			{
				await client.User.DeactivateAsync(user.Id);
			}
		}
	}
}
