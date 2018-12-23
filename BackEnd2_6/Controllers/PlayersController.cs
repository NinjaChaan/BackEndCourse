using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;
using BackEnd2_6.Processors;
using Microsoft.AspNetCore.Authorization;
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

		[HttpGet("{id:Guid}")]
		public Task<Player> Get(Guid id) {
			return playersProcessor.Get(id);
		}

		[HttpGet("{name}")]
		public Task<Player> Get(string name) {
			return playersProcessor.Get(name);
		}

		[HttpGet]
		public Task<Player[]> GetAll() {
			return playersProcessor.GetAll();
		}

		[HttpGet("top/{x}")]
		public Task<Player[]> GetTopPlayers(int x) {
			return playersProcessor.GetTopPlayers(x);
		}

		[HttpGet("commonLevel")]
		public Task<int> GetCommonLevel() {
			return playersProcessor.GetCommonLevel();
		}

		[HttpPost]
		public Task<Player> Create([FromBody] NewPlayer player) {
			return playersProcessor.Create(player);
		}

		[HttpPut("{id}")]
		public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player) {
			return playersProcessor.Modify(id, player);
		}

		[HttpPut("{id}/updateName")]
		public Task ModifyPlayerName(Guid id, [FromBody] string newName) {
			return playersProcessor.ModifyPlayerName(id, newName);
		}

		[HttpPut("{id}/updateScore")]
		public Task IncrementPlayerScore(Guid id, [FromBody] int score) {
			return playersProcessor.IncrementPlayerScore(id, score);
		}

		[HttpDelete("{id}")]
		[AuditLogFilter("delete player")]
		public Task<Player> Delete(Guid id) {
			return playersProcessor.Delete(id);
		}
	}
}