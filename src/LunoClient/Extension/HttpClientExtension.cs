﻿using System.Net.Http;
using System.Threading.Tasks;

namespace Luno.Extension
{
	public static class HttpClientExtension
	{
		public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUrl, StringContent content)
		{
			var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUrl) { Content = content };
			return client.SendAsync(request);
		}
	}
}
