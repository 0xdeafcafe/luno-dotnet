using System;
using Newtonsoft.Json;

namespace Luno.Models.User
{
	public class CreateUser<T>
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("created")]
		public DateTime? Created { get; set; }

		[JsonProperty("profile")]
		public T Profile { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
