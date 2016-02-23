using System.Linq;
using System.Threading.Tasks;
using Luno;
using Luno.Exceptions;
using Luno.Models.Event;
using LunoTodoApp.Helper;
using LunoTodoApp.Models;
using LunoTodoApp.ViewModels;
using Microsoft.AspNet.Mvc;

namespace LunoTodoApp.Controllers
{
	public class TodoController : BaseController
	{
		public TodoController(LunoClient lunoClient)
			: base(lunoClient)
		{ }

		// GET: /Todo/
		public async Task<IActionResult> Index()
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			var items = await LunoClient.Event.GetAllAsync<TodoItem, Profile>(session.User.Id, limit: 200, expand: new[] { "user" });
			var todoItems = items.List.Where(i => i.Name == "Todo Item");
			return View(todoItems);
		}

		// GET: /Todo/Create
		public async Task<IActionResult> Create()
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			return View();
		}

		// POST: /Todo/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TodoItemViewModel model)
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			if (!ModelState.IsValid)
				return View(model);

			await LunoClient.Event.CreateAsync<TodoItem, Profile>(new CreateEvent<TodoItem>
			{
				UserId = session.User.Id,
				Details = new TodoItem { Name = model.Name, Description = model.Description },
				Name = "Todo Item"
			});

			return RedirectToAction("Index");
		}

		// GET: /Todo/Update/{id}
		public async Task<IActionResult> Update(string id)
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			Event<TodoItem, Profile> item = null;
			try
			{
				item = await LunoClient.Event.GetAsync<TodoItem, Profile>(id);
				if (item.Name != "Todo Item" || item.User.Id != session.User.Id)
					return RedirectToAction("Index", "Todo");
			}
			catch (LunoApiException ex)
			{
				if (ex.Code == "event_not_found")
					return RedirectToAction("Index", "Todo");
				else
					throw;
			}

			return View(new TodoItemViewModel { Name = item.Details.Name, Description = item.Details.Description, Completed = item.Details.Completed });
		}

		// POST: /Todo/Update/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(string id, TodoItemViewModel model)
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			if (!ModelState.IsValid)
				return View(model);

			Event<TodoItem, Profile> item = null;
			try
			{
				item = await LunoClient.Event.GetAsync<TodoItem, Profile>(id);
				if (item.Name != "Todo Item" || item.User.Id != session.User.Id)
					return RedirectToAction("Index", "Todo");
			}
			catch (LunoApiException ex)
			{
				if (ex.Code == "event_not_found")
					return RedirectToAction("Index", "Todo");
				else
					throw;
			}

			await LunoClient.Event.UpdateAsync(item, new TodoItem { Name = model.Name, Description = model.Description, Completed = model.Completed }, destructive: true);
			return RedirectToAction("Index");
		}
		
		// GET: /Todo/Delete/{id}
		public async Task<IActionResult> Destroy(string id)
		{
			var session = await SessionHelper.GetSessionAsync(this, LunoClient);
			if (session == null)
				return RedirectToAction("Create", "Session");

			try
			{
				var item = await LunoClient.Event.GetAsync<TodoItem, Profile>(id);
				if (item.Name != "Todo Item" || item.User.Id != session.User.Id)
					return RedirectToAction("Index", "Todo");
			}
			catch (LunoApiException ex)
			{
				if (ex.Code == "event_not_found")
					return RedirectToAction("Index", "Todo");
				else
					throw;
			}

			await LunoClient.Event.DeleteAsync(id);
			return RedirectToAction("Index", "Todo");
		}
	}
}
