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
			ApiAuthentication = new ApiAuthenticationClient(connection);
			Event = new EventClient(connection);
			Session = new SessionClient(connection);
			User = new UsersClient(connection);
		}
		
		public IAnalyticsClient Analytics { get; private set; }

		public IApiAuthenticationClient ApiAuthentication { get; private set; }

		public IEventClient Event { get; private set; }

		public ISessionClient Session { get; private set; }

		public IUsersClient User { get; private set; }
	}
}
