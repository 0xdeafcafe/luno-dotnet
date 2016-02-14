using System.Collections.Generic;
using System.Threading.Tasks;

namespace Luno.Interfaces
{
	public interface IAnalyticsClient
	{
		Task<Dictionary<string, int>> GetUserAnalytics(string[] days = null);
	}
}
