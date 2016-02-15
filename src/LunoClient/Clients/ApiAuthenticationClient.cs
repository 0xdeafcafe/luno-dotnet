using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models.ApiAuthentication;

namespace Luno.Clients
{
    public class ApiAuthenticationClient
		: ApiClient, IApiAuthenticationClient
    {
		public ApiAuthenticationClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<ApiAuthentication<TApiAuthentication, TUser>> GetAsync<TApiAuthentication, TUser>(string key, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<ApiAuthentication<TApiAuthentication, TUser>>($"/api_authentication/{key}", additionalParams);
		}
	}
}
