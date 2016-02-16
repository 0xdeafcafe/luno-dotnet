using Luno;
using Microsoft.AspNet.Mvc;

namespace LunoTodoApp.Controllers
{
	public class BaseController : Controller
	{
		public LunoClient LunoClient { get; set; }

		public BaseController(LunoClient lunoClient)
		{
			LunoClient = lunoClient;
		}
	}
}
