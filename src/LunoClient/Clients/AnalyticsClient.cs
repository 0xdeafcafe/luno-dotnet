using System.Collections.Generic;
using System.Threading.Tasks;
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

		public async Task<Dictionary<string, int>> GetUserAnalytics(string[] days = null)
		{
			if (days == null)
				days = new[] { "total", "7", "28" };

			return await HttpConnection.GetAsync<Dictionary<string, int>>("/analytics/users", new Dictionary<string, string> { { "days", string.Join(",", days) } });
		}
	}
}
