using System;
using Luno.Connections;
using Luno.Models.User;
using Luno.Test.LunoClient.Extensions;
using Luno.Test.LunoClient.Models.Test;

namespace Luno.Test.LunoClient.Helpers
{
	public static class Factory
	{
		public static readonly string[] FirstNameCollection = { "Alex", "George", "Ryan", "Hannah", "Shad", "Jade", "James", "Kaelan", "Laura", "Simion", "Robin", "Simon" };
		public static readonly string[] LastNameCollection = { "Forbes-Reed", "Miller", "Licchelli", "Mayes", "Mugal", "Stanger", "Billingham", "Fouwels", "Corlett", "Putina", "Johnson", "Tabor" };
		public static readonly string[] SquadKeyWordCollection = { "tragic", "burn", "bae", "taylor", "swift", "baelor", "tinder", "boob", "snapchat", "mdma", "flair", "snake", "pokemon", "shemma", "xoxo", "himym", "alison", "brie" };

		public static CreateUser<Profile> GenerateCreateUser(Random random, Profile profile = null)
		{
			var keyWord = $"{SquadKeyWordCollection.GetRandom(random)}-{SquadKeyWordCollection.GetRandom(random)}";

			return new CreateUser<Profile>
			{
				FirstName = FirstNameCollection.GetRandom(random),
				LastName = $"{LastNameCollection.GetRandom(random)}",
				Email = $"test.{keyWord}.{random.Next(100000, 999999)}@outlook.com",
				Username = $"test.{keyWord}.{random.Next(100000, 999999)}",
				Password = "12345qwerty,./",
				Profile = profile
			};
		}

		public static ApiKeyConnection GenerateApiKeyConnection()
		{
			var key = Environment.GetEnvironmentVariable("LUNO_API_KEY");
			var secret = Environment.GetEnvironmentVariable("LUNO_SECRET_KEY");

			return new ApiKeyConnection(key, secret);
		}
	}
}
