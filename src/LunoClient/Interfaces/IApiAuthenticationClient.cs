using System.Threading.Tasks;
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
	}
}
