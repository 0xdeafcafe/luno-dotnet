using System.Threading.Tasks;
using Luno;
using LunoTodoApp.Helper;
using Microsoft.AspNet.Mvc;

namespace LunoTodoApp.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(LunoClient lunoClient)
			 : base(lunoClient)
		{ }
		
		public async Task<IActionResult> Index()
		{
			await SessionHelper.GetSessionAsync(this, LunoClient);
			return View();
		}
		
		public async Task<IActionResult> Error()
		{
			await SessionHelper.GetSessionAsync(this, LunoClient);
			return View();
		}
	}
}
