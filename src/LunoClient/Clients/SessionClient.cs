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
		public SessionClient(ApiKeyConnection connection)
			: base(connection)
		{ }

		public async Task<PaginationResponse<Session<TSession, TUser>>> GetAllAsync<TSession, TUser>(string to = null, string from = null, uint limit = 100, string[] expand = null)
		{
			var additionalParams = new Dictionary<string, string>();
			additionalParams.Add(nameof(limit), limit.ToString());
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

		public async Task<SuccessResponse> UpdateAsync<TSession, TUser>(string id, Session<TSession, TUser> session, bool distructive = false)
		{
			var updateSession = new UpdateSession<TSession>
			{
				Ip = session.Ip,
				UserAgent = session.UserAgent,
				Details = session.Details
			};

			if (distructive)
				return await HttpConnection.PutAsync<SuccessResponse>($"/sessions/{id}", updateSession);
			else
				return await HttpConnection.PatchAsync<SuccessResponse>($"/sessions/{id}", updateSession);
		}

		public async Task<SuccessResponse> DeleteAsync(string id)
		{
			return await HttpConnection.DeleteAsync<SuccessResponse>($"/sessions/{id}");
		}

		public async Task<Session<TSession, TUser>> ValidateAsync<TSession, TUser>(Session<TSession, TUser> session, string[] expand = null)
		{
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
