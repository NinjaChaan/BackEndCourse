using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;

namespace BackEnd2_6
{
	public class InMemoryRepository : IRepository
	{
		private readonly List<Player> players = new List<Player>();

		public async Task<Player> CreatePlayer(Player player) {
			players.Add(player);
			return player;
		}

		public async Task<Player> DeletePlayer(Guid id) {
			Player p = await GetPlayer(id);
			players.Remove(p);
			return p;
		}

		public async Task<Player> GetPlayer(Guid id) {
			return players.SingleOrDefault(x => x.Id == id);
		}

		public async Task<Player> GetPlayer(string name) {
			return players.SingleOrDefault(x => x.Name == name);
		}

		public async Task<Player[]> GetAllPlayers() {
			return players.ToArray();
		}

		public async Task<Player[]> GetTopPlayers(int amount) {
			return players.AsQueryable().OrderByDescending(x => x.Score).Take(amount).ToArray();
		}

		public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player) {
			Player p = await GetPlayer(id);
			p.Score = player.Score;
			return p;
		}

		public async Task ModifyPlayerName(Guid id, string newName) {
			Player p = await GetPlayer(id);
			p.Name = newName;
		}

		public async Task IncrementPlayerScore(Guid id, int score) {
			Player p = await GetPlayer(id);
			p.Score += score;
		}

		public async Task<Item> GetItem(Guid playerId, Guid id) {
			Player p = await GetPlayer(playerId);
			return p.Items.SingleOrDefault(x => x.Id == id);
		}

		public async Task<Item[]> GetAllItems(Guid playerId) {
			Player p = await GetPlayer(playerId);
			return p.Items.ToArray();
		}

		public async Task<Item> CreateItem(Guid playerId, Item item) {
			Player p = await GetPlayer(playerId);
			p.Items.Add(item);
			return item;
		}

		public async Task<Item> ModifyItem(Guid playerId, Guid id, ModifiedItem item) {
			Item i = await GetItem(playerId, id);
			i.Level = item.Level;
			return i;
		}

		public async Task<Item> DeleteItem(Guid playerId, Guid id) {
			Item i = await GetItem(playerId, id);
			Player p = await GetPlayer(playerId);
			p.Items.Remove(i);
			return i;
		}

		public async Task<int> GetCommonLevel() {
			var commonLevel = players.AsQueryable().Select(x => x.Level).GroupBy(x => x).OrderByDescending(x => x.Count()).FirstOrDefault();
			return commonLevel.Key;
		}
	}
}
