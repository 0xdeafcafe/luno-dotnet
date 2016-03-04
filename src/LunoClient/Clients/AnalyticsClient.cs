using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Enums;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Analytics;

namespace Luno.Clients
{
	public class AnalyticsClient
		: ApiClient, IAnalyticsClient
	{
		internal AnalyticsClient(ApiKeyConnection connection)
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

		public async Task<AnalyticsTimelineResponse> GetEventAnalyticsTimelineAsync(bool distinct = false, AnalyticsTimelineGroup group = AnalyticsTimelineGroup.Day, bool roundRange = false, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?), string userId = null, string name = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(distinct), distinct.ToString().ToLowerInvariant());
			additionalParams.Add(nameof(group), group.ToString().ToLowerInvariant());
			additionalParams.Add(nameof(roundRange), roundRange.ToString().ToLowerInvariant());
			if (from != null) additionalParams.Add(nameof(from), $"{from?.ToString("yyyy-MM-ddTHH:mm:ss.fff")}Z");
			if (to != null) additionalParams.Add(nameof(to), $"{to?.ToString("yyyy-MM-ddTHH:mm:ss.fff")}Z");
			if (userId != null) additionalParams.Add("user_id", userId);
			if (name != null) additionalParams.Add(nameof(name), name);

			return await HttpConnection.GetAsync<AnalyticsTimelineResponse>("/analytics/events/timeline", additionalParams);
		}
	}
}
