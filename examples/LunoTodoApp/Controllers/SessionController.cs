using System.Threading.Tasks;
using Luno;
using Luno.Exceptions;
using LunoTodoApp.Extensions;
using LunoTodoApp.Helper;
using LunoTodoApp.Models;
using LunoTodoApp.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace LunoTodoApp.Controllers
{
	public class SessionController : BaseController
	{
		public SessionController(LunoClient lunoClient)
			 : base(lunoClient)
		{ }

		// GET: /Session/
		public IActionResult Index()
		{
			return RedirectToAction(nameof(Create));
		}

		// GET: /Session/Create
		public async Task<IActionResult> Create()
		{
			if (await SessionHelper.GetSessionAsync(this, LunoClient) != null)
				return RedirectToAction("Index", "Account");

			return View();
		}

		// POST: /Session/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(LoginViewModel model)
		{
			if (await SessionHelper.GetSessionAsync(this, LunoClient) != null)
				return RedirectToAction("Index", "Account");

			if (!ModelState.IsValid)
				return View(model);

			try
			{
				var response = await LunoClient.User.LoginAsync<Profile, SessionStorage>(model.Username, model.Password, expand: new[] { "user" });
				HttpContext.Session.SetObjectAsJson("session", response.Session);
			}
			catch (LunoApiException ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}

		// GET: /Session/Destroy
		public async Task<IActionResult> Destroy()
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Index", "Home");

			await LunoClient.Session.DeleteAsync(session);
			HttpContext.Session.Remove("session");
			return RedirectToAction("Index", "Home");
		}
	}
}
