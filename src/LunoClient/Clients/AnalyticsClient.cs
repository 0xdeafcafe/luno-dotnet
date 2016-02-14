using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;

namespace Luno.Clients
{
	public class AnalyticsClient
		: ApiClient, IAnalyticsClient
	{
		public AnalyticsClient(ApiKeyConnection connection)
			: base(connection)
		{ }
	}
}
