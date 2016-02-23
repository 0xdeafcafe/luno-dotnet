using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Event;

namespace Luno.Clients
{
	public class EventClient
		: ApiClient, IEventClient
	{
		public EventClient(ApiKeyConnection connection)
			: base(connection)
		{ }
		
		public async Task<Event<TEvent, TUser>> CreateAsync<TEvent, TUser>(CreateEvent<TEvent> @event, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<Event<TEvent, TUser>>($"/events", @event, additionalParams);
		}
		
		public async Task<PaginationResponse<Event<TEvent, TUser>>> GetAllAsync<TEvent, TUser>(string userId = null, string from = null, string to = null, string name = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (userId != null) additionalParams.Add("user_id", userId);
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (name != null) additionalParams.Add(nameof(name), name);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<Event<TEvent, TUser>>>("/events", additionalParams);
		}

		public async Task<Event<TEvent, TUser>> GetAsync<TEvent, TUser>(string id, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<Event<TEvent, TUser>>($"/events/{id}", additionalParams);
		}

		public async Task<Event<TEvent, TUser>> GetAsync<TEvent, TUser>(Event<TEvent, TUser> @event, string[] expand = null)
		{
			Ensure.ArgumentNotNull(@event, "event");

			return await GetAsync<TEvent, TUser>(@event.Id, expand: expand);
		}

		public async Task<SuccessResponse> UpdateAsync<TEvent>(string id, TEvent details, bool destructive = false)
		{
			if (destructive)
				return await HttpConnection.PutAsync<SuccessResponse>($"/events/{id}", new { details });
			else
				return await HttpConnection.PatchAsync<SuccessResponse>($"/events/{id}", new { details });
		}

		public async Task<SuccessResponse> UpdateAsync<TEvent, TUser>(Event<TEvent, TUser> @event, TEvent details, bool destructive = false)
		{
			Ensure.ArgumentNotNull(@event, "event");

			return await UpdateAsync(@event.Id, details, destructive: destructive);
		}

		public async Task<SuccessResponse> DeleteAsync(string id)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/events/{id}");
		}

		public async Task<SuccessResponse> DeleteAsync<TEvent, TUser>(Event<TEvent, TUser> @event)
		{
			Ensure.ArgumentNotNull(@event, "event");

			return await DeleteAsync(@event.Id);
		}
	}
}
