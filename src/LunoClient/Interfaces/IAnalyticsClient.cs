using System.Collections.Generic;
using System.Threading.Tasks;

namespace Luno.Interfaces
{
	public interface IAnalyticsClient
	{
		Task<Dictionary<string, int>> GetUserAnalytics(string[] days = null);

		Task<Dictionary<string, int>> GetSessionAnalytics(string[] days = null);

		Task<Dictionary<string, int>> GetEventAnalytics(string[] days = null);
	}
}
