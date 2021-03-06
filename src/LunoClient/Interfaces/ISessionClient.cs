﻿using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.Session;

namespace Luno.Interfaces
{
	public interface ISessionClient
	{
		/// <summary>
		/// Create a new session for this user
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="session">The new sessions's details</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Session<TSession, TUser>> CreateAsync<TSession, TUser>(CreateSession<TSession> session = null, string[] expand = null);
		
		/// <summary>
		/// Get a list of recently created sessions
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="userId">User ID to filter sessions by, use null to list anonymous sessions</param>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<Session<TSession, TUser>>> GetAllAsync<TSession, TUser>(string userId = null, string to = null, string from = null, uint limit = 100, string[] expand = null);
		
		/// <summary>
		/// Get a session
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The session ID</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Session<TSession, TUser>> GetAsync<TSession, TUser>(string id, string[] expand = null);

		/// <summary>
		/// Get a session
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="session">The session model</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Session<TSession, TUser>> GetAsync<TSession, TUser>(Session<TSession, TUser> session, string[] expand = null);

		/// <summary>
		/// Set attributes for a session, extending the `details` properties
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The session ID</param>
		/// <param name="session">The updated session model</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<TSession, TUser>(string id, Session<TSession, TUser> session, bool destructive = false);

		/// <summary>
		/// Set attributes for a session, extending the `details` properties
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="session">The updated session model</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<TSession, TUser>(Session<TSession, TUser> session, bool destructive = false);

		/// <summary>
		/// Permanently delete a session
		/// </summary>
		/// <param name="id">The session ID</param>
		Task<SuccessResponse> DeleteAsync(string id);

		/// <summary>
		/// Permanently delete a session
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="session">The session model</param>
		Task<SuccessResponse> DeleteAsync<TSession, TUser>(Session<TSession, TUser> session);

		/// <summary>
		/// Validate a session key, incrementing the access count and setting the last access time.
		/// </summary>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <param name="session">The session model to validate</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<Session<TSession, TUser>> ValidateAsync<TSession, TUser>(Session<TSession, TUser> session, string[] expand = null);
	}
}
