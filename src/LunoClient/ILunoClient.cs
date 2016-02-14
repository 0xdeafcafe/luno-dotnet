using Luno.Interfaces;

namespace Luno
{
	public interface ILunoClient
	{
		IAnalyticsClient Analytics { get; }

		IUsersClient User { get; }
	}
}
