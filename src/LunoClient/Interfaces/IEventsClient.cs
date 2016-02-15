using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.Event;

namespace Luno.Interfaces
{
	public interface IEventsClient
	{
		/// <summary>
		/// Get a list of recently created users
		/// </summary>
		/// <typeparam name="TEvent">Any arbitrary data associated with the event model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="name">Name of an event to filter the list by</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<Event<TEvent, TUser>>> GetAllAsync<TEvent, TUser>(string from = null, string to = null, string name = null, uint limit = 100, string[] expand = null);
		
		/// <summary>
		/// Get an event
		/// </summary>
		/// <typeparam name="TEvent">Any arbitrary data associated with the event model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The event ID</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Event<TEvent, TUser>> GetAsync<TEvent, TUser>(string id, string[] expand = null);

		/// <summary>
		/// Set attributes for an event, extending the `details` properties
		/// </summary>
		/// <typeparam name="TEvent">Any arbitrary data associated with the event model</typeparam>
		/// <param name="id">The event ID</param>
		/// <param name="details">The updated event details model</param>
		/// <param name="distructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<TEvent>(string id, TEvent details, bool distructive = false);

		/// <summary>
		/// Permanently delete an event
		/// </summary>
		/// <param name="id">The event ID</param>
		Task<SuccessResponse> DeleteAsync(string id);
	}
}
