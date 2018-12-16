using BackEnd2_6.Models;
using System;
using System.Threading.Tasks;

namespace BackEnd2_6
{
	public class ItemsProcessor
	{
		private readonly IRepository repository;

		public ItemsProcessor(IRepository repository) {
			this.repository = repository;
		}

		public Task<Item> Get(Guid playerId, Guid id) {
			return repository.GetItem(playerId, id);
		}

		public Task<Item[]> GetAll(Guid playerId) {
			return repository.GetAllItems(playerId);
		}

		public async Task<Item> Create(Guid playerId, NewItem Item) {
			Player p = await repository.GetPlayer(playerId);

			if(p.Level < 3 && Item.Type == ItemType.Sword) {
				throw new LevelTooLowException();
			}

			Item i = new Item
			{
				Name = Item.Name,
				Id = Guid.NewGuid(),
				Level = Item.Level,
				Type = Item.Type,
				CreationTime = DateTime.Now
			};
			return await repository.CreateItem(playerId, i);
		}

		public Task<Item> Modify(Guid playerId, Guid id, ModifiedItem Item) {
			return repository.ModifyItem(playerId, id, Item);
		}

		public Task<Item> Delete(Guid playerId, Guid id) {
			return repository.DeleteItem(playerId, id);
		}
	}
}