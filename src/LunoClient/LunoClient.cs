namespace Luno
{
	public class LunoClient
	{
		public const string BasePath = "https://api.luno.io";

		public const ushort Version = 1;

		public ushort Timeout { get; set; } = 10000;

		internal readonly string ApiKey;

		internal readonly string SecretKey;

		public LunoClient(string apiKey, string secretKey, ushort timeout = 10000)
		{
			ApiKey = apiKey;
			SecretKey = secretKey;
			Timeout = timeout;
		}


	}
}
