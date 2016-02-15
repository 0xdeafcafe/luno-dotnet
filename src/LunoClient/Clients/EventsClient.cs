using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;

namespace Luno.Clients
{
	public class EventsClient
		: ApiClient, IEventsClient
	{
		public EventsClient(ApiKeyConnection connection)
			: base(connection)
		{ }
	}
}
