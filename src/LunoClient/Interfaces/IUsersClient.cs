using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.ApiAuthentication;
using Luno.Models.Event;
using Luno.Models.Session;
using Luno.Models.User;

namespace Luno.Interfaces
{
	public interface IUsersClient
	{
		Task<User<T>> CreateAsync<T>(CreateUser<T> user, string[] expand = null);

		Task<User<T>> GetAsync<T>(string id, string[] expand = null);

		Task<PaginationResponse<User<T>>> GetAllAsync<T>(string from = null, string to = null, uint limit = 100, string[] expand = null);
		
		Task<SuccessResponse> UpdateAsync<T>(string id, User<T> updatedUser, bool distructive = false);

		Task<SuccessResponse> DeleteAsync(string id);

		Task<SuccessResponse> ValidatePassword(string id, string password);

		Task<SuccessResponse> ChangePassword(string id, string newPassword);

		Task<Event<TEvent, TUser>> CreateEventAsync<TEvent, TUser>(string id, CreateEvent<TEvent> @event, string[] expand = null);

		Task<LoginResponse<TUser, TSession>> LoginAsync<TUser, TSession>(string login, string password, string[] expand = null);

		Task<Session<TSession, TUser>> CreateSessionAsync<TSession, TUser>(string id, CreateSession<TSession> session = null, string[] expand = null);

		Task<PaginationResponse<Session<TSession, TUser>>> GetSessionsAsync<TSession, TUser>(string id, string from = null, uint limit = 100, string to = null, string[] expand = null);

		Task<SuccessResponse> DeleteSessionsAsync(string id);

		Task<ApiAuthentication<TApiAuthentication, TUser>> CreateApiAuthenticationAsync<TApiAuthentication, TUser>(string id, CreateApiAuthentication<TApiAuthentication> apiAuthentication = null, string[] expand = null);

		Task<PaginationResponse<ApiAuthentication<TApiAuthentication, TUser>>> GetAllApiAuthenticationsAsync<TApiAuthentication, TUser>(string id, string[] expand = null);
	}
}
