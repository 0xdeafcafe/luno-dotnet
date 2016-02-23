namespace Luno.Connections
{
	public class ApiKeyConnection
	{
		/// <summary>
		/// Creates a new Api Key Connection
		/// </summary>
		/// <param name="apiKey">A Luno Api Key</param>
		/// <param name="secretKey">A Luno Secret Key</param>
		public ApiKeyConnection(string apiKey, string secretKey)
		{
			ApiKey = apiKey;
			SecretKey = secretKey;
		}

		public string ApiKey { get; private set; }

		public string SecretKey { get; private set; }
	}
}
