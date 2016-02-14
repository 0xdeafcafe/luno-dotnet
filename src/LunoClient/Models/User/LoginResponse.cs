using Luno.Models.Session;
using Newtonsoft.Json;

namespace Luno.Models.User
{
	public class LoginResponse<TUser, TSession>
	{
		[JsonProperty("user")]
		public User<TUser> User { get; set; }

		[JsonProperty("session")]
		public Session<TSession, TUser> Session { get; set; }
	}
}
