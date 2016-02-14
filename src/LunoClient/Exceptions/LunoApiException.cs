using System;
using Luno.Models;

namespace Luno.Exceptions
{
	public class LunoApiException
		: Exception
	{
		public string Code { get; set; }

		public int Status { get; set; }

		public dynamic Extra { get; set; }
		
		public LunoApiException(ErrorResponse error)
			: base(error.Message)
		{
			Code = error.Code;
			Status = error.Status;
			Extra = error.Extra;
		}
	}
}
