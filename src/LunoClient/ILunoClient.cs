using Luno.Interfaces;

namespace Luno
{
	public interface ILunoClient
	{
		IAnalyticsClient Analytics { get; }

		IEventClient Event { get; }

		ISessionClient Session { get; }

		IUsersClient User { get; }
	}
}
