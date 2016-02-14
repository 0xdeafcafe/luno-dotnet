using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.Session;

namespace Luno.Interfaces
{
	public interface ISessionClient
	{
		/// <summary>
		/// Get a list of recently created sessions
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<Session<TSession, TUser>>> GetAllAsync<TSession, TUser>(string to = null, string from = null, uint limit = 100, string[] expand = null);

		/// <summary>
		/// Get a session
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The session ID</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Session<TSession, TUser>> GetAsync<TSession, TUser>(string id, string[] expand = null);
	}
}
