using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;

namespace BackEnd2_6
{
	public interface IRepository
	{
		Task<Player> GetPlayer(Guid id);
		Task<Player[]> GetAllPlayers();
		Task<Player> CreatePlayer(Player player);
		Task<Player> ModifyPlayer(Guid id, ModifiedPlayer player);
		Task<Player> DeletePlayer(Guid id);


		Task<Item> GetItem(Guid playerId, Guid id);
		Task<Item[]> GetAllItems(Guid playerId);
		Task<Item> CreateItem(Guid playerId, Item item);
		Task<Item> ModifyItem(Guid playerId, Guid id, ModifiedItem item);
		Task<Item> DeleteItem(Guid playerId, Guid id);
	}
}
