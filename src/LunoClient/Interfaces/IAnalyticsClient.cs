using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Enums;
using Luno.Models;
using Luno.Models.Analytics;

namespace Luno.Interfaces
{
	public interface IAnalyticsClient
	{
		Task<Dictionary<string, int>> GetUserAnalyticsAsync(string[] days = null);

		Task<Dictionary<string, int>> GetSessionAnalyticsAsync(string[] days = null);

		Task<Dictionary<string, int>> GetEventAnalyticsAsync(string[] days = null);

		Task<AnalyticsListResponse<EventAnalytic>> GetEventAnalyticsListAsync();

		Task<AnalyticsTimelineResponse> GetEventAnalyticsTimelineAsync(bool distinct = false, AnalyticsTimelineGroup group = AnalyticsTimelineGroup.Day, DateTime? from = null, DateTime? to = null, string userId = null, string name = null);
	}
}
