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

		public Task<Player[]> GetAll() {
			return repository.GetAllPlayers();
		}

		public Task<Player> Create(NewPlayer player) {
			Player p = new Player
			{
				Name = player.Name,
				Id = Guid.NewGuid(),
				Score = 0,
				Level = 0,
				IsBanned = false,
				CreationTime = DateTime.Now
			};
			return repository.CreatePlayer(p);
		}

		public Task<Player> Modify(Guid id, ModifiedPlayer player) {
			return repository.ModifyPlayer(id, player);
		}

		public Task<Player> Delete(Guid id) {
			return repository.DeletePlayer(id);
		}
	}
}
