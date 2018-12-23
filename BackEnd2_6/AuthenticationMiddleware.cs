using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd2_6
{
	public class AuthenticationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string apiKey;
		private readonly string adminApiKey;

		public AuthenticationMiddleware(RequestDelegate next, IConfiguration config) {
			_next = next;
			apiKey = config["api-key"];
			adminApiKey = config["api-key-admin"];
		}

		public async Task Invoke(HttpContext context) {
			var headers = context.Request.Headers;
			if (headers.TryGetValue("x-api-key", out StringValues keyvalue)) {


				if(keyvalue.Any(x => x == adminApiKey)) {
					await _next(context);
				}else if (keyvalue.Any(x => x == apiKey)) {
					await _next(context);
				} else {
					context.Response.StatusCode = StatusCodes.Status403Forbidden;
				}

			} else {
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
			}
		}
	}
}
