using Luno.Models.Session;
using Newtonsoft.Json;

namespace Luno.Models.User
{
	public class LoginResponse<T>
	{
		[JsonProperty("user")]
		public User<T> User { get; set; }

		[JsonProperty("session")]
		public Session<T> Session { get; set; }
	}
}
