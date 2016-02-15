﻿using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.ApiAuthentication;

namespace Luno.Interfaces
{
	public interface IApiAuthenticationClient
	{
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