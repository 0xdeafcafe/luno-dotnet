using System.Threading.Tasks;
using Luno.Enums;
using Luno.Models;
using Luno.Models.Session;
using Luno.Models.User;

namespace Luno.Interfaces
{
	public interface IUsersClient
	{
		/// <summary>
		/// Create a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The new user's details</param>
		/// <param name="autoName">Whether to automatically set Name, FirstName and LastName based on the provided data</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<User<T>> CreateAsync<T>(CreateUser<T> user, bool autoName = true, string[] expand = null);

		/// <summary>
		/// Get a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The user ID</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<User<T>> GetAsync<T>(string id, string[] expand = null);

		/// <summary>
		/// Get a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="searchField">The field to search for a user by</param>
		/// <param name="searchValue">The value of the field to find a user by</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<User<T>> GetByAsync<T>(UserSearchField searchField, string searchValue, string[] expand = null);

		/// <summary>
		/// Get a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The user model</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<User<T>> GetAsync<T>(User<T> user, string[] expand = null);

		/// <summary>
		/// Get a list of recently created users
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="from">The item ID to retrieve the list from</param>
		/// <param name="to">The item ID to stop retrieving the list at</param>
		/// <param name="limit">The maximum number of items to return. min: 0, max: 200</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<PaginationResponse<User<T>>> GetAllAsync<T>(string from = null, string to = null, uint limit = 100, string[] expand = null);

		/// <summary>
		/// Update a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="id">The user ID</param>
		/// <param name="updatedUser">The updated user model</param>
		/// <param name="autoName">Whether to automatically set Name, FirstName and LastName based on the provided data</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<T>(string id, User<T> updatedUser, bool autoName = true, bool destructive = false);

		/// <summary>
		/// Update a user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="updatedUser">The updated user model</param>
		/// <param name="autoName">Whether to automatically set Name, FirstName and LastName based on the provided data</param>
		/// <param name="destructive">Whether to update existing attributes, or override the model and replace it in it's entirety</param>
		Task<SuccessResponse> UpdateAsync<T>(User<T> updatedUser, bool autoName = true, bool destructive = false);

		/// <summary>
		/// Deactivate a user, setting the `closed` attribute.
		/// </summary>
		/// <param name="id">The user ID</param>
		Task<SuccessResponse> DeactivateAsync(string id);

		/// <summary>
		/// Deactivate a user, setting the `closed` attribute.
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The user model</param>
		Task<SuccessResponse> DeactivateAsync<T>(User<T> user);

		/// <summary>
		/// Check that a password is correct, without logging the user in
		/// </summary>
		/// <param name="id">The user ID</param>
		/// <param name="password">The user's password </param>
		Task<SuccessResponse> ValidatePasswordAsync(string id, string password);

		/// <summary>
		/// Check that a password is correct, without logging the user in
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The user model</param>
		/// <param name="password">The user's password </param>
		Task<SuccessResponse> ValidatePasswordAsync<T>(User<T> user, string password);

		/// <summary>
		/// Change or set a users password
		/// </summary>
		/// <param name="id">The user ID</param>
		/// <param name="newPassword">The new password</param>
		/// <param name="currentPassword">The user's current password to validate before changing the password.</param>
		Task<SuccessResponse> ChangePasswordAsync(string id, string newPassword, string currentPassword = null);

		/// <summary>
		/// Change or set a users password
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The user model</param>
		/// <param name="newPassword">The new password</param>
		/// <param name="currentPassword">The user's current password to validate before changing the password.</param>
		Task<SuccessResponse> ChangePasswordAsync<T>(User<T> user, string newPassword, string currentPassword = null);

		/// <summary>
		/// Log in a user by id, email or username
		/// </summary>
		/// <typeparam name="TUser">Any arbitrary data associated with the user model</typeparam>
		/// <typeparam name="TSession">Any arbitrary data associated with the session model</typeparam>
		/// <param name="login">The user ID, email or username</param>
		/// <param name="password">The user's password </param>
		/// <param name="session">Attributes associated with the session which is created if the credentials are valid</param>
		/// <param name="expand">The models to expand (fetch details)</param>
		Task<LoginResponse<TUser, TSession>> LoginAsync<TUser, TSession>(string login, string password, CreateSession<TSession> session = null, string[] expand = null);

		/// <summary>
		/// Permanently delete all sessions associated with this user
		/// </summary>
		/// <param name="id">The user ID</param>
		Task<SuccessResponse> DeleteSessionsAsync(string id);

		/// <summary>
		/// Permanently delete all sessions associated with this user
		/// </summary>
		/// <typeparam name="T">Any arbitrary data associated with the user model</typeparam>
		/// <param name="user">The user model</param>
		Task<SuccessResponse> DeleteSessionsAsync<T>(User<T> user);
	}
}
