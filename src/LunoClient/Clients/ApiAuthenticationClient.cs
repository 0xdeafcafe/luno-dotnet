using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.ApiAuthentication;
using Luno.Models.User;

namespace Luno.Clients
{
	public class ApiAuthenticationClient
		: ApiClient, IApiAuthenticationClient
	{
		internal ApiAuthenticationClient(ApiKeyConnection connection)
			: base(connection)
		{ }
		
		public async Task<ApiAuthentication<TApiAuthentication, TUser>> CreateAsync<TApiAuthentication, TUser>(CreateApiAuthentication<TApiAuthentication> apiAuthentication = null, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<ApiAuthentication<TApiAuthentication, TUser>>(
				$"/api_authentication", apiAuthentication ?? new CreateApiAuthentication<TApiAuthentication>(), additionalParams);
		}
		
		#region [ GetAllAsync ]

		public async Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllAsync<TApiAuthentication, TUser>(string userId, string from = null, string to = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (userId != null) additionalParams.Add("user_id", userId);
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>>($"/api_authentication", additionalParams);
		}

		public async Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllAsync<TApiAuthentication, TUser>(User<TUser> user, string from = null, string to = null, uint limit = 100, string[] expand = null)
		{
			return await GetAllAsync<TApiAuthentication, TUser>(user?.Id, from: from, to: to, limit: limit, expand: expand);
		}

		#endregion

		public async Task<ApiAuthentication<TApiAuthentication, TUser>> GetAsync<TApiAuthentication, TUser>(string key, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<ApiAuthentication<TApiAuthentication, TUser>>($"/api_authentication/{key}", additionalParams);
		}

		public async Task<ApiAuthentication<TApiAuthentication, TUser>> GetAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication, string[] expand = null)
		{
			Ensure.ArgumentNotNull(apiAuthentication, nameof(apiAuthentication));

			return await GetAsync<TApiAuthentication, TUser>(apiAuthentication.Key, expand: expand);
		}

		public async Task<SuccessResponse> UpdateAsync<TApiAuthentication>(string key, TApiAuthentication details, bool destructive = false)
		{
			if (destructive)
				return await HttpConnection.PutAsync<SuccessResponse>($"/api_authentication/{key}", new { details });
			else
				return await HttpConnection.PatchAsync<SuccessResponse>($"/api_authentication/{key}", new { details });
		}

		public async Task<SuccessResponse> UpdateAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication, TApiAuthentication details, bool destructive = false)
		{
			Ensure.ArgumentNotNull(apiAuthentication, nameof(apiAuthentication));

			return await UpdateAsync(apiAuthentication.Key, details, destructive: destructive);
		}

		public async Task<SuccessResponse> DeleteAsync(string key)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/api_authentication/{key}");
		}

		public async Task<SuccessResponse> DeleteAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication)
		{
			Ensure.ArgumentNotNull(apiAuthentication, nameof(apiAuthentication));

			return await DeleteAsync(apiAuthentication.Key);
		}
	}
}
