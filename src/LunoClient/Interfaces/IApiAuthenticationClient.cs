using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.ApiAuthentication;
using Luno.Models.User;

namespace Luno.Interfaces
{
	public interface IApiAuthenticationClient
	{
		/// <summary>
		/// Create a new API key
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="apiAuthentication">The new api authentication's details</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<ApiAuthentication<TApiAuthentication, TUser>> CreateAsync<TApiAuthentication, TUser>(CreateApiAuthentication<TApiAuthentication> apiAuthentication = null, string[] expand = null);
		
		/// <summary>
		/// Get a list of recently created API Authentications
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the API Authentications model</typeparam>
		/// <param name="userId">User ID to filter by</param>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllAsync<TApiAuthentication, TUser>(string userId = null, string from = null, string to = null, uint limit = 100, string[] expand = null);

		/// <summary>
		/// Get a list of recently created API Authentications
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the API Authentications model</typeparam>
		/// <param name="user">User to filter by</param>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllAsync<TApiAuthentication, TUser>(User<TUser> user = null, string from = null, string to = null, uint limit = 100, string[] expand = null);

		/// <summary>
		/// Get API Authentication details by the API key
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="key">The API key</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<ApiAuthentication<TApiAuthentication, TUser>> GetAsync<TApiAuthentication, TUser>(string key, string[] expand = null);

		/// <summary>
		/// Get API Authentication details by the API Authentication Model
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="apiAuthentication">The API Authentication</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<ApiAuthentication<TApiAuthentication, TUser>> GetAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication, string[] expand = null);

		/// <summary>
		/// Set attributes for an API Authentication, extending the `details` properties
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <param name="key">The API key</param>
		/// <param name="details">The updated API Authentication details model</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<TApiAuthentication>(string key, TApiAuthentication details, bool destructive = false);

		/// <summary>
		/// Set attributes for an API Authentication, extending the `details` properties
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="apiAuthentication">The API Authentication</param>
		/// <param name="details">The updated API Authentication details model</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication, TApiAuthentication details, bool destructive = false);

		/// <summary>
		/// Permanently delete an API Authentication
		/// </summary>
		/// <param name="key">The API key</param>
		Task<SuccessResponse> DeleteAsync(string key);

		/// <summary>
		/// Permanently delete an API Authentication
		/// </summary>
		/// <typeparam name="TApiAuthentication">Any arbitrary data associated with the api authentication model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="apiAuthentication">The API Authentication</param>
		Task<SuccessResponse> DeleteAsync<TApiAuthentication, TUser>(ApiAuthentication<TApiAuthentication, TUser> apiAuthentication);
	}
}
