using System;

namespace LunoClient.Test.Extensions
{
	public static class ArrayExtensions
	{
		public static string GetRandom(this string[] array, Random random)
		{
			return array[random.Next(array.Length)];
		}
	}
}
