using System;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Models;
using Luno.Models.User;
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
			var response = await client.TestAsync<PaginationResponse<User>>();

			Assert.True(true);
		}
	}
}
