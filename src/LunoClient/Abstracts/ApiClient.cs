using Luno.Connections;
using Luno.Http;

namespace Luno.Abstracts
{
	public abstract class ApiClient
	{
		protected ApiClient(ApiKeyConnection connection)
		{
			Ensure.ArgumentNotNull(connection, nameof(connection));

			ApiKeyConnection = connection;
			HttpConnection = new HttpConnection(ApiKeyConnection);
		}

		internal ApiKeyConnection ApiKeyConnection { get; private set; }

		internal HttpConnection HttpConnection { get; private set; }
	}
}
