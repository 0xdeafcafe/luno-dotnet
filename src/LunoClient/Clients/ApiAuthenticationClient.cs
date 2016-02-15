using System.Threading.Tasks;
using Luno.Abstracts;
using Luno.Connections;
using Luno.Interfaces;

namespace Luno.Clients
{
    public class ApiAuthenticationClient
		: ApiClient, IApiAuthenticationClient
    {
		public ApiAuthenticationClient(ApiKeyConnection connection)
			: base(connection)
		{ }
    }
}
