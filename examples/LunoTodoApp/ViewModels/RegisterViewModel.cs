using System.ComponentModel.DataAnnotations;

namespace LunoTodoApp.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[StringLength(25, MinimumLength = 1)]
		[RegularExpression(@"^(?=[a-zA-Z0-9])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$")]
		public string Username { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$")]
		public string Password { get; set; }

		[Required]
		[StringLength(25, MinimumLength = 1)]
		public string Name { get; set; }
	}
}
