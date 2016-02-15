using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Event;

namespace Luno.Clients
{
	public class EventsClient
		: ApiClient, IEventsClient
	{
		public EventsClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<PaginationResponse<Event<TEvent, TUser>>> GetAllAsync<TEvent, TUser>(string from = null, string to = null, string name = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (name != null) additionalParams.Add(nameof(name), name);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<Event<TEvent, TUser>>>("/events", additionalParams);
		}
	}
}
