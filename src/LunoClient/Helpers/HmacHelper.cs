using System;
using System.Security.Cryptography;
using System.Text;

namespace Luno.Helpers
{
	public static class HmacHelper
	{
		/// <summary>
		/// Creates a Sha512 HMAC hash of an inputted string.
		/// </summary>
		/// <param name="input">The string to create the hash from.</param>
		public static string ComputeHmacSha512Hash(string input, string secret)
		{
			var utf8Encoding = new UTF8Encoding();
			var inputBytes = utf8Encoding.GetBytes(input);
			var secretBytes = utf8Encoding.GetBytes(secret);

			var hmacSha512 = new HMACSHA512(secretBytes);
			var hash = hmacSha512.ComputeHash(inputBytes);

			return BitConverter.ToString(hash).Replace("-", "");
		}
	}
}
