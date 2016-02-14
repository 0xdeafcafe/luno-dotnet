using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Analytics;

namespace Luno.Clients
{
	public class AnalyticsClient
		: ApiClient, IAnalyticsClient
	{
		public AnalyticsClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<Dictionary<string, int>> GetUserAnalyticsAsync(string[] days = null)
		{
			if (days == null)
				days = new[] { "total", "7", "28" };

			return await HttpConnection.GetAsync<Dictionary<string, int>>("/analytics/users", new Dictionary<string, string> { { "days", string.Join(",", days) } });
		}

		public async Task<Dictionary<string, int>> GetSessionAnalyticsAsync(string[] days = null)
		{
			if (days == null)
				days = new[] { "total", "7", "28" };

			return await HttpConnection.GetAsync<Dictionary<string, int>>("/analytics/sessions", new Dictionary<string, string> { { "days", string.Join(",", days) } });
		}

		public async Task<Dictionary<string, int>> GetEventAnalyticsAsync(string[] days = null)
		{
			if (days == null)
				days = new[] { "total", "7", "28" };

			return await HttpConnection.GetAsync<Dictionary<string, int>>("/analytics/events", new Dictionary<string, string> { { "days", string.Join(",", days) } });
		}

		public async Task<AnalyticsListResponse<EventAnalytic>> GetEventAnalyticsListAsync()
		{
			return await HttpConnection.GetAsync<AnalyticsListResponse<EventAnalytic>>("/analytics/events/list");
		}
	}
}
