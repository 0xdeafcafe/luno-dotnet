using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Luno.Connections;
using Luno.Exceptions;
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

		private TimeSpan _timeout = TimeSpan.FromMilliseconds(10000);
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

		public async Task<T> GetAsync<T>(string route, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.GET, route, parameters: parameters);
		}

		public async Task<T> PostAsync<T>(string route, object body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.POST, route, body, parameters);
		}

		public async Task<T> PutAsync<T>(string route, object body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.PUT, route, body, parameters);
		}

		public async Task<T> PatchAsync<T>(string route, object body, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.PATCH, route, body, parameters);
		}

		public async Task<T> DeleteAsync<T>(string route, Dictionary<string, string> parameters = null)
		{
			return await MakeRequestAsync<T>(HttpMethod.DELETE, route, parameters);
		}

		private async Task<T> MakeRequestAsync<T>(HttpMethod method, string route, object body = null, Dictionary<string, string> parameters = null)
		{
			SortedDictionary<string, string> sortedParameters = parameters == null
				? sortedParameters = new SortedDictionary<string, string>()
				: sortedParameters = new SortedDictionary<string, string>(parameters);

			using (var httpClient = new HttpClient(new HttpClientHandler { Proxy = new WebProxy("localhost:8888", true), UseDefaultCredentials = true }))
			{
				httpClient.Timeout = _timeout;

				// Set required headers
				httpClient.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue(_jsonContentType));
				httpClient.DefaultRequestHeaders.UserAgent.Add(
					new ProductInfoHeaderValue("luno_dotnet", "0.0.1")); // TODO: make this reaaal
				
				httpClient.BaseAddress = new Uri(_baseUrl);
				var path = $"/v{_version}{route}";

				// Add additional required parameters
				sortedParameters.Add("key", _apiKey);
				sortedParameters.Add("timestamp", $"{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff")}Z");

				// Create signature
				var signatureDatum = $"{method.ToString()}:{path}?{sortedParameters.ToQueryString()}";
				if (body != null) signatureDatum += $":{JsonConvert.SerializeObject(body)}";
				var signature = HmacHelper.ComputeHmacSha512Hash(signatureDatum, _secretKey).ToLower();
				sortedParameters.Add("sign", signature);

				path = $"{path}?{sortedParameters.ToQueryString()}";

				HttpResponseMessage response = null;
				switch (method)
				{
					case HttpMethod.GET:
						response = await httpClient.GetAsync(path);
						break;

					case HttpMethod.POST:
						response = await httpClient.PostAsync(path, 
							new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, _jsonContentType));
						break;

					case HttpMethod.PUT:
						response = await httpClient.PutAsync(path, 
							new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, _jsonContentType));
						break;

					case HttpMethod.PATCH:
						response = await httpClient.PatchAsync(path, 
							new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, _jsonContentType));
						break;

					case HttpMethod.DELETE:
						response = await httpClient.DeleteAsync(path);
						break;

					default:
						throw new NotImplementedException($"The http method '{method.ToString()}' is not supported.");
				}

				// read the response content into a string
				var responseContent = await response.Content.ReadAsStringAsync();

				// if the response was a success: deserialize the response and then return it.
				if (response.IsSuccessStatusCode)
					return JsonConvert.DeserializeObject<T>(responseContent);

				// otherwise, deserialize it into an error response and throw it
				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
				throw new LunoApiException(errorResponse);
			}
		}
	}
}
