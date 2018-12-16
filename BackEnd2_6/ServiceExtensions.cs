using BackEnd2_6.Processors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6
{
    public static class ServiceExtensions
    {
		public static IServiceCollection UseRepository(this IServiceCollection service, IRepository rep) {
			return service.AddSingleton(rep);
		}

		public static IServiceCollection UsePlayersProcessor(this IServiceCollection service) {
			return service.AddSingleton<PlayersProcessor>();
		}

		public static IServiceCollection UseItemsProcessor(this IServiceCollection service) {
			return service.AddSingleton<ItemsProcessor>();
		}
	}
}
