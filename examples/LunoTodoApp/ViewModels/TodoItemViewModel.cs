using System.ComponentModel.DataAnnotations;

namespace LunoTodoApp.ViewModels
{
	public class TodoItemViewModel
	{
		[Required]
		[StringLength(25, MinimumLength = 1)]
		public string Name { get; set; }

		[Required]
		[StringLength(250, MinimumLength = 1)]
		public string Description { get; set; }

		public bool Completed { get; set; } = false;
	}
}
