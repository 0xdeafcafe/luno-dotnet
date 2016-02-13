using Luno.Interfaces;

namespace Luno
{
	public interface ILunoClient
	{
		IUsersClient User { get; }
	}
}
