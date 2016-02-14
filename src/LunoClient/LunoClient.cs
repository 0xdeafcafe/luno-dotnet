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
			Analytics = new AnalyticsClient(connection);
			User = new UsersClient(connection);
		}
		
		public IAnalyticsClient Analytics { get; private set; }

		public IUsersClient User { get; private set; }
	}
}
