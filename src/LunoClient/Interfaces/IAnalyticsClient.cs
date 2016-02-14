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
		/// <summary>
		/// How many new users have joined? Get the number of users created within the specified periods, defaulting to 7 days, 28 days and the total number
		/// </summary>
		/// <param name="days">The number of days to fetch the numbers for, e.g. 7 will fetch data for the past 7 days</param>
		Task<Dictionary<string, int>> GetUserAnalyticsAsync(string[] days = null);

		/// <summary>
		/// How many sessions have been created? Get the number of sessions created within the specified periods, defaulting to 7 days, 28 days and the total number
		/// </summary>
		/// <param name="days">The number of days to fetch the numbers for, e.g. 7 will fetch data for the past 7 days</param>
		Task<Dictionary<string, int>> GetSessionAnalyticsAsync(string[] days = null);

		/// <summary>
		/// How many events have been triggered? Get the number of events created within the specified periods, defaulting to 7 days, 28 days and the total number
		/// </summary>
		/// <param name="days">The number of days to fetch the numbers for, e.g. 7 will fetch data for the past 7 days</param>
		Task<Dictionary<string, int>> GetEventAnalyticsAsync(string[] days = null);

		/// <summary>
		/// Get a list of all events triggered, including how many times they've been triggered, and when they were last triggered
		/// </summary>
		Task<AnalyticsListResponse<EventAnalytic>> GetEventAnalyticsListAsync();

		/// <summary>
		/// Get a timeline of how many times events have been triggered over a period
		/// </summary>
		/// <param name="distinct">Whether each individual user should only count once per interval.</param>
		/// <param name="group">The time period to group each interval by</param>
		/// <param name="from">When to start fetching data from</param>
		/// <param name="to">When to fetch data up until</param>
		/// <param name="userId">Filter the events by a single user ID</param>
		/// <param name="name">Event name to filter the timeline by</param>
		Task<AnalyticsTimelineResponse> GetEventAnalyticsTimelineAsync(bool distinct = false, AnalyticsTimelineGroup group = AnalyticsTimelineGroup.Day, DateTime? from = null, DateTime? to = null, string userId = null, string name = null);
	}
}
