using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;
using Luno.Models;
using Luno.Models.Session;

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
	}
}
