using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Session;
using Luno.Models.User;

namespace Luno.Clients
{
	public class SessionClient
		: ApiClient, ISessionClient
	{
		internal SessionClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<Session<TSession, TUser>> CreateAsync<TSession, TUser>(CreateSession<TSession> session = null, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.PostAsync<Session<TSession, TUser>>($"/sessions", session ?? new CreateSession<TSession>(), additionalParams);
		}
		
		public async Task<PaginationResponse<Session<TSession, TUser>>> GetAllAsync<TSession, TUser>(string userId = null, string to = null, string from = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
			if (userId != null) additionalParams.Add("user_id", userId);
			if (from != null) additionalParams.Add(nameof(from), from);
			if (to != null) additionalParams.Add(nameof(to), to);
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<PaginationResponse<Session<TSession, TUser>>>("/sessions", additionalParams);
		}

		public async Task<Session<TSession, TUser>> GetAsync<TSession, TUser>(string id, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			return await HttpConnection.GetAsync<Session<TSession, TUser>>($"/sessions/{id}", additionalParams);
		}

		public async Task<Session<TSession, TUser>> GetAsync<TSession, TUser>(Session<TSession, TUser> session, string[] expand = null)
		{
			Ensure.ArgumentNotNull(session, nameof(session));

			return await GetAsync<TSession, TUser>(session.Id, expand: expand);
		}

		public async Task<SuccessResponse> UpdateAsync<TSession, TUser>(string id, Session<TSession, TUser> session, bool destructive = false)
		{
			Ensure.ArgumentNotNull(session, nameof(session));

			var updateSession = new UpdateSession<TSession>
			{
				UserId = session.User.Id,
				Expires = session.Expires,
				Ip = session.Ip,
				UserAgent = session.UserAgent,
				Details = session.Details
			};

			if (destructive)
				return await HttpConnection.PutAsync<SuccessResponse>($"/sessions/{id}", updateSession);
			else
				return await HttpConnection.PatchAsync<SuccessResponse>($"/sessions/{id}", updateSession);
		}

		public async Task<SuccessResponse> UpdateAsync<TSession, TUser>(Session<TSession, TUser> session, bool destructive = false)
		{
			Ensure.ArgumentNotNull(session, nameof(session));

			return await UpdateAsync(session.Id, session, destructive: destructive);
		}

		public async Task<SuccessResponse> DeleteAsync(string id)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/sessions/{id}");
		}

		public async Task<SuccessResponse> DeleteAsync<TSession, TUser>(Session<TSession, TUser> session)
		{
			Ensure.ArgumentNotNull(session, nameof(session));

			return await DeleteAsync(session.Id);
		}

		public async Task<Session<TSession, TUser>> ValidateAsync<TSession, TUser>(Session<TSession, TUser> session, string[] expand = null)
		{
			Ensure.ArgumentNotNull(session, nameof(session));

			var additionalParams = new Dictionary<string, string>();
			if (expand != null) additionalParams.Add(nameof(expand), string.Join(",", expand));

			var validateSession = new ValidateSession<TSession>
			{
				Key = session.Key,
				Ip = session.Ip,
				UserAgent = session.UserAgent,
				Details = session.Details
			};

			return await HttpConnection.PostAsync<Session<TSession, TUser>>("/sessions/access", validateSession, additionalParams);
		}
	}
}
