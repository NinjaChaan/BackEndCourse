using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6
{
    public static class MiddlewareExtensions
    {
		public static void UseAuthentication(this IApplicationBuilder builder) {
			builder.UseMiddleware<AuthenticationMiddleware>();
		}
    }
}
