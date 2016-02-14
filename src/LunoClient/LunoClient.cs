using Luno.Abstracts;
using Luno.Clients;
using Luno.Connections;
using Luno.Interfaces;

namespace Luno
{
	public class LunoClient
		: ApiClient, ILunoClient
	{
		public LunoClient(ApiKeyConnection connection)
			: base(connection)
		{
			User = new UsersClient(connection);
		}
		
		public IUsersClient User { get; private set; }
	}
}
