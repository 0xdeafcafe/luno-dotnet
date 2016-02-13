using System;
using Newtonsoft.Json;

namespace Luno.Models.User
{
	public class CreateUser<T>
	{
		[JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
		public string Email { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
		public string FirstName { get; set; }

		[JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
		public string LastName { get; set; }

		[JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
		public string Username { get; set; }

		[JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? Created { get; set; }

		[JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
		public T Profile { get; set; }

		[JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
		public string Password { get; set; }
	}
}
