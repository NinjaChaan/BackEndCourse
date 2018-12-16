using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;
using BackEnd2_6.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd2_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
		private readonly PlayersProcessor playersProcessor;

		public PlayersController(PlayersProcessor playersProcessor) {
			this.playersProcessor = playersProcessor;
		}

		[HttpGet("{id}")]
		public Task<Player> Get(Guid id) {
			return playersProcessor.Get(id);
		}

		[HttpGet]
		public Task<Player[]> GetAll() {
			return playersProcessor.GetAll();
		}

		[HttpPost]
		public Task<Player> Create([FromBody] NewPlayer player) {
			return playersProcessor.Create(player);
		}

		[HttpPut("{id}")]
		public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player) {
			return playersProcessor.Modify(id, player);
		}

		[HttpDelete("{id}")]
		public Task<Player> Delete(Guid id) {
			return playersProcessor.Delete(id);
		}
	}
}