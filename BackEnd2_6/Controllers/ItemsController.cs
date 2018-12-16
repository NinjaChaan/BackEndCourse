using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd2_6.Controllers
{
    [Route("api/players/{playerId}/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
		private readonly ItemsProcessor itemsProcessor;

		public ItemsController(ItemsProcessor itemsProcessor) {
			this.itemsProcessor = itemsProcessor;
		}

		[HttpGet("{id}")]
		public Task<Item> Get(Guid playerId, Guid id) {
			return itemsProcessor.Get(playerId, id);
		}

		[HttpGet]
		public Task<Item[]> GetAll(Guid playerId) {
			return itemsProcessor.GetAll(playerId);
		}

		[HttpPost]
		[LevelTooLowExceptionFilter]
		public Task<Item> Create(Guid playerId, [FromBody] NewItem Item) {
			return itemsProcessor.Create(playerId, Item);
		}

		[HttpPut("{id}")]
		public Task<Item> Modify(Guid playerId, Guid id, [FromBody] ModifiedItem Item) {
			return itemsProcessor.Modify(playerId, id, Item);
		}

		[HttpDelete("{id}")]
		public Task<Item> Delete(Guid playerId, Guid id) {
			return itemsProcessor.Delete(playerId, id);
		}
	}
}