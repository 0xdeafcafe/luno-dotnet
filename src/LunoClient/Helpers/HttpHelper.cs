using System;
using System.Net.Http;

namespace Luno.Helpers
{
	public static class HttpHelper
	{
		private enum HttpMethod
		{
			GET,
			POST,
			PUT,
			PATCH,
			DELETE
		}
		
		public static void Get()
		{
			throw new NotImplementedException();
		}

		private static void MakeRequest(HttpMethod method, string baseUrl, ushort version, string route)
		{
			using (var httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri($"{baseUrl}/v{version}{route}");
			}
		}
	}
}