using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6.Models
{
	public enum ItemType
	{
		Sword,
		Dagger,
		Halberd,
		HealthPotion,
		Helmet,
		Shield
	}

	public class Item
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public ItemType Type { get; set; }
		public DateTime CreationTime { get; set; }
	}

    public class NewItem
    {
		[StringLength(128)]
		public string Name { get; set; }

		[Range(0, 99)]
		public int Level { get; set; }

		[AllowedItemTypes(ItemType.Sword, ItemType.Dagger, ItemType.Halberd, ItemType.HealthPotion, ItemType.Helmet, ItemType.Shield)]
		public ItemType Type { get; set; }

		[PastDate]
		[DataType(DataType.Date)]
		public DateTime CreationTime { get; set; }
	}

	public class ModifiedItem
	{
		[Range(0, 99)]
		public int Level { get; set; }
	}
}
