using System;
using System.Collections.Generic;
using Luno.Models;

namespace Luno.Exceptions
{
	public class LunoApiException
		: Exception
	{
		public string Code { get; set; }

		public string Description { get; set; }

		public int Status { get; set; }

		public Dictionary<string, string> Details { get; set; }
		
		public LunoApiException(ErrorResponse error)
			: base(error.Message)
		{
			Code = error.Code;
			Description = error.Description;
			Status = error.Status;
			Details = error.Extra;
		}
	}
}
