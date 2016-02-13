using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Helpers;

namespace Luno
{
	public class LunoClient
		: ApiClient
	{
		public LunoClient(ApiKeyConnection connection)
			: base(connection)
		{

		}

		public async Task<T> TestAsync<T>()
		{
			return await HttpConnection.GetAsync<T>("/users");
		}
	}
}
