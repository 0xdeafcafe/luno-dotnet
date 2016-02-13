using Luno.Interfaces;

namespace Luno.Connections
{
	public class ApiKeyConnection
		: IApiConnection
	{
		public ApiKeyConnection(string apiKey, string secretKey)
		{
			ApiKey = apiKey;
			SecretKey = secretKey;
		}

		public string ApiKey { get; private set; }

		public string SecretKey { get; private set; }
	}
}
