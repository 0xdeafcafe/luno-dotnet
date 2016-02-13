using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Extension;
using Luno.Helpers;
using Luno.Models;
using Newtonsoft.Json;

namespace Luno.Http
{
	public class HttpConnection
	{
		private const string _baseUrl = "https://api.luno.io";
		private const ushort _version = 1;
		private const string _jsonContentType = "application/json";

		private ushort _timestamp = 10000;
		private string _apiKey = null;
		private string _secretKey = null;

		private enum HttpMethod
		{
			GET,
			POST,
			PUT,
			PATCH,
			DELETE
		}


		public HttpConnection(ApiKeyConnection connection)
		{
			Ensure.ArgumentNotNull(connection, nameof(connection));
			Ensure.ArgumentNotNullOrEmptyString(connection.ApiKey, nameof(connection.ApiKey));
			Ensure.ArgumentNotNullOrEmptyString(connection.SecretKey, nameof(connection.SecretKey));

			_apiKey = connection.ApiKey;
			_secretKey = connection.SecretKey;
		}

		public async Task<T> GetAsync<T>(string route, SortedDictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.GET, route, parameters);
		}

		private async Task<T> MakeRequestAsync<T>(HttpMethod method, string route, SortedDictionary<string, string> parameters = null)
		{
			if (parameters == null)
				parameters = new SortedDictionary<string, string>();

			using (var httpClient = new HttpClient())
			{
				// Set required headers
				httpClient.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue(_jsonContentType));
				httpClient.DefaultRequestHeaders.UserAgent.Add(
					new ProductInfoHeaderValue("luno_dotnet", "0.0.1")); // TODO: make this reaaal

				httpClient.BaseAddress = new Uri(_baseUrl);
				var path = $"/v{_version}{route}";

				// Add additional required parameters
				parameters.Add("key", _apiKey);
				parameters.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "Z");

				// Create signature
				var signatureDatum = $"{method.ToString()}:{path}?{parameters.ToQueryString()}"; // TODO: apply code for a body
				var signature = HmacHelper.ComputeHmacSha512Hash(signatureDatum, _secretKey).ToLower();
				parameters.Add("sign", signature);

				HttpResponseMessage response = null;
				switch (method)
				{
					case HttpMethod.GET:
						response = await httpClient.GetAsync($"{path}?{parameters.ToQueryString()}");
						break;

					default:
						throw new NotImplementedException($"The http method '{method.ToString()}' is not supported.");
				}

				// read the response content into a string
				var responseContent = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
					return JsonConvert.DeserializeObject<T>(responseContent);

				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
				Debugger.Break();
				throw new InvalidOperationException();
			}
		}
	}
}
