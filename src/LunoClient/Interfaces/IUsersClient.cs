using System.Threading.Tasks;
using Luno.Models;
using Luno.Models.User;

namespace Luno.Interfaces
{
	public interface IUsersClient
	{
		Task<User<T>> CreateAsync<T>(CreateUser<T> user, string[] expand = null);

		Task<User<T>> GetAsync<T>(string id, string[] expand = null);

		Task<PaginationResponse<User<T>>> GetAllAsync<T>(string from = null, string to = null, int limit = 100, string[] expand = null);
		
		Task<SuccessResponse> UpdateAsync<T>(string id, User<T> updatedUser, bool distructive = false);

		Task<SuccessResponse> DeleteAsync(string id);

		Task<LoginResponse<T>> LoginAsync<T>(string login, string password, string[] expand = null);
	}
}
