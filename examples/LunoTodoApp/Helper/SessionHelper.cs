using System.Threading.Tasks;
using Luno;
using Luno.Models.Session;
using LunoTodoApp.Controllers;
using LunoTodoApp.Extensions;
using LunoTodoApp.Models;

namespace LunoTodoApp.Helper
{
	public static class SessionHelper
	{
		public static async Task<Session<SessionStorage, Profile>> GetSessionAsync(BaseController controller, LunoClient client)
		{
			var session = controller.HttpContext.Session;
			var lunoSession = session.GetObjectFromJson<Session<SessionStorage, Profile>>("session");
			if (lunoSession == null) return null;
			lunoSession = await client.Session.ValidateAsync(lunoSession, expand: new[] { "user" });
			session.SetObjectAsJson("session", lunoSession);
			return controller.ViewBag.Session = lunoSession;
		}
	}
}
