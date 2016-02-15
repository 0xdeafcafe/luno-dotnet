using Luno.Interfaces;

namespace Luno
{
	public interface ILunoClient
	{
		IAnalyticsClient Analytics { get; }

		IEventsClient Events { get; }

		ISessionClient Session { get; }

		IUsersClient User { get; }
	}
}
