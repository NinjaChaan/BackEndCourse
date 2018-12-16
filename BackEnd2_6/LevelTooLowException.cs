using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6
{
    public class LevelTooLowException : Exception
    {
	}


    public class LevelTooLowExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context) {

			if (context.Exception is LevelTooLowException) {
				JsonResult error = new JsonResult("Level is too low");
				error.StatusCode = StatusCodes.Status422UnprocessableEntity;

				context.ExceptionHandled = true;
				context.Result = error;
			}
		}
	}
}
