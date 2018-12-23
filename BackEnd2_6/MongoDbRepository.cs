using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackEnd2_6
{
	public class MongoDbRepository : IRepository
	{
		private static IMongoCollection<Player> PlayerCollection;
		private static IMongoCollection<AuditLogMessage> LogMessages;

		public MongoDbRepository() {
			MongoClient client = new MongoClient("mongodb://localhost:27017");
			IMongoDatabase db = client.GetDatabase("game");
			PlayerCollection = db.GetCollection<Player>("players");
			LogMessages = db.GetCollection<AuditLogMessage>("auditlog");
		}

		public async Task<Item> CreateItem(Guid playerId, Item item) {
			var filter = Builders<Player>.Filter.Eq("Id", playerId);
			var update = Builders<Player>.Update.AddToSet("Items", item);
			await PlayerCollection.UpdateOneAsync(filter, update);
			return item;
		}

		public async Task<Player> CreatePlayer(Player player) {
			await PlayerCollection.InsertOneAsync(player);
			return player;
		}

		public async Task<Item> DeleteItem(Guid playerId, Guid id) {
			var filter = Builders<Player>.Filter.Eq("Id", playerId);
			var itemFilter = Builders<Item>.Filter.Eq("Id", id);
			var update = Builders<Player>.Update.PullFilter("Items", itemFilter);
			var player = await PlayerCollection.FindOneAndUpdateAsync(filter, update);
			return player.Items.Single(x => x.Id == id);
		}

		public async Task<Player> DeletePlayer(Guid id) {
			var filter = Builders<Player>.Filter.Eq("Id", id);
			var player = await PlayerCollection.FindOneAndDeleteAsync(filter);
			return player;
		}

		public async Task<Item[]> GetAllItems(Guid playerId) {
			var filter = Builders<Player>.Filter.Eq("Id", playerId);
			var player = await PlayerCollection.FindAsync(filter);
			return player.Single().Items.ToArray();
		}

		public async Task<Player[]> GetAllPlayers() {
			var filter = Builders<Player>.Filter.Empty;
			return (await PlayerCollection.FindAsync(filter)).ToList().ToArray();
		}

		public async Task<Player[]> GetTopPlayers(int amount) {
			return PlayerCollection.AsQueryable().OrderByDescending(x => x.Score).Take(amount).ToArray();
		}

		public async Task<int> GetCommonLevel() {

			var common = await PlayerCollection.Aggregate()
				.Project(x => new { x.Level })
				.Group(x => x.Level, g => new {
					Level = g.Key,
					Count = g.Count()
				}).SortByDescending(x => x.Count).Limit(1).FirstOrDefaultAsync();

			return common.Level;
		}

		public async Task<Item> GetItem(Guid playerId, Guid id) {
			var player = await GetPlayer(playerId);
			return player.Items.Single(x => x.Id == id);
		}

		public async Task<Player> GetPlayer(Guid id) {
			var filter = Builders<Player>.Filter.Eq("Id", id);
			var player = await PlayerCollection.FindAsync(filter);
			return player.Single();
		}

		public async Task<Player> GetPlayer(string name) {
			var filter = Builders<Player>.Filter.Eq("Name", name);
			var player = await PlayerCollection.FindAsync(filter);
			return player.Single();
		}

		public async Task<Item> ModifyItem(Guid playerId, Guid id, ModifiedItem item) {
			var filter = Builders<Player>.Filter.Where(x => x.Id == playerId && x.Items.Any(it => it.Id == id));
			var update = Builders<Player>.Update.Set(x => x.Items[-1].Level, item.Level);

			var player = await PlayerCollection.FindOneAndUpdateAsync(filter, update);

			var i = player.Items.Single(x => x.Id == id);
			i.Level = item.Level;

			return i;
		}

		public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player) {
			var filter = Builders<Player>.Filter.Eq("Id", id);
			UpdateDefinition<Player> LevelUpdate = Builders<Player>.Update.Set("Level", (int)player.Level);
			UpdateDefinition<Player> ScoreUpdate = Builders<Player>.Update.Set("Score", (int)player.Score);

			var update = Builders<Player>.Update.Combine(LevelUpdate, ScoreUpdate);

			await PlayerCollection.FindOneAndUpdateAsync(filter, update);
			return (await PlayerCollection.FindAsync(filter)).Single();
		}

		public async Task ModifyPlayerName(Guid id, string newName) {
			var filter = Builders<Player>.Filter.Eq("Id", id);
			UpdateDefinition<Player> update = Builders<Player>.Update.Set("Name", newName);

			await PlayerCollection.UpdateOneAsync(filter, update);
		}

		public async Task IncrementPlayerScore(Guid id, int score) {
			var filter = Builders<Player>.Filter.Eq("Id", id);
			UpdateDefinition<Player> update = Builders<Player>.Update.Inc("Score", score);

			await PlayerCollection.UpdateOneAsync(filter, update);
		}

		public Task RecordAuditMessage(AuditLogMessage message) {
			return LogMessages.InsertOneAsync(message);
		}
	}
}
