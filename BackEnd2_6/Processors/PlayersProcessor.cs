using BackEnd2_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6.Processors
{
    public class PlayersProcessor
    {
		private readonly IRepository repository;

		public PlayersProcessor(IRepository repository) {
			this.repository = repository;
		}

		public Task<Player> Get(Guid id) {
			return repository.GetPlayer(id);
		}

		public Task<Player> Get(string name) {
			return repository.GetPlayer(name);
		}

		public Task<Player[]> GetAll() {
			return repository.GetAllPlayers();
		}

		public Task<Player[]> GetTopPlayers(int x) {
			return repository.GetTopPlayers(x);
		}

		public Task<int> GetCommonLevel() {
			return repository.GetCommonLevel();
		}

		public Task<Player> Create(NewPlayer player) {
			Player p = new Player
			{
				Name = player.Name,
				Id = Guid.NewGuid(),
				Score = 0,
				Level = 0,
				IsBanned = false,
				CreationTime = DateTime.Now,
				Items = new List<Item>()
			};
			return repository.CreatePlayer(p);
		}

		public Task<Player> Modify(Guid id, ModifiedPlayer player) {
			return repository.ModifyPlayer(id, player);
		}

		public Task ModifyPlayerName(Guid id, string newName) {
			return repository.ModifyPlayerName(id, newName);
		}

		public Task IncrementPlayerScore(Guid id, int score) {
			return repository.IncrementPlayerScore(id, score);
		}

		public Task<Player> Delete(Guid id) {
			return repository.DeletePlayer(id);
		}
	}
}
