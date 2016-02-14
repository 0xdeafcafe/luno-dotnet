using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.ApiAuthentication;
using Luno.Models.Event;
using Luno.Models.Session;
using Luno.Models.User;

namespace Luno.Clients
{
	public class UsersClient
		: ApiClient, IUsersClient
	{
		public UsersClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<User<T>> CreateAsync<T>(CreateUser<T> user, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<User<T>>("/users", user, additionalParams);
		}

		public async Task<User<T>> GetAsync<T>(string id, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<User<T>>($"/users/{id}", additionalParams);
		}

		public async Task<PaginationResponse<User<T>>> GetAllAsync<T>(string from = null, string to = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<User<T>>>("/users", additionalParams);
		}
		
		public async Task<SuccessResponse> UpdateAsync<T>(string id, User<T> user, bool distructive = false)
		{
			var updateUser = new UpdateUser<T>
			{
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Name = user.Name,
				Profile = user.Profile,
				Username = user.Username
			};

			if (distructive)
				return await HttpConnection.PutAsync<SuccessResponse>($"/users/{id}", updateUser);
			else
				return await HttpConnection.PatchAsync<SuccessResponse>($"/users/{id}", updateUser);
		}

		public async Task<SuccessResponse> DeleteAsync(string id)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/users/{id}");
		}

		public async Task<SuccessResponse> ValidatePassword(string id, string password)
		{
			return await HttpConnection.PostAsync<SuccessResponse>($"/users/{id}/validatePassword", new { password });
		}

		public async Task<SuccessResponse> ChangePassword(string id, string newPassword)
		{
			return await HttpConnection.PostAsync<SuccessResponse>($"/users/{id}/changePassword", new { password = newPassword });
		}

		public async Task<Event<TEvent, TUser>> CreateEventAsync<TEvent, TUser>(string id, CreateEvent<TEvent> @event, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<Event<TEvent, TUser>>(
				$"/users/{id}/events", @event, additionalParams);
		}

		public async Task<PaginationResponse<Event<TEvent, TUser>>> GetEventsAsync<TEvent, TUser>(string id, string from = null, string to = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<Event<TEvent, TUser>>>($"/users/{id}/events", additionalParams);
		}

		public async Task<LoginResponse<TUser, TSession>> LoginAsync<TUser, TSession>(string login, string password, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<LoginResponse<TUser, TSession>>("/users/login", new { login, password }, additionalParams);
		}

		public async Task<Session<TSession, TUser>> CreateSessionAsync<TSession, TUser>(string id, CreateSession<TSession> session = null, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));
			
			return await HttpConnection.PostAsync<Session<TSession, TUser>>($"/users/{id}/sessions", session ?? new CreateSession<TSession>(), additionalParams);
		}

		public async Task<PaginationResponse<Session<TSession, TUser>>> GetSessionsAsync<TSession, TUser>(string id, string from = null, uint limit = 100, string to = null, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<Session<TSession, TUser>>>($"/users/{id}/sessions", additionalParams);
		}

		public async Task<SuccessResponse> DeleteSessionsAsync(string id)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/users/{id}/sessions");
		}

		public async Task<ApiAuthentication<TApiAuthentication, TUser>> CreateApiAuthenticationAsync<TApiAuthentication, TUser>(string id, CreateApiAuthentication<TApiAuthentication> apiAuthentication = null, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<ApiAuthentication<TApiAuthentication, TUser>>(
				$"/users/{id}/api_authentication", apiAuthentication ?? new CreateApiAuthentication<TApiAuthentication>(), additionalParams);
		}

		public async Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllApiAuthenticationsAsync<TApiAuthentication, TUser>(string id, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>>($"/users/{id}/api_authentication", additionalParams);
		}
	}
}
