using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace GC_Shops.Features.Implementations.ItemComponent
{
    public class Item : IEquatable<Item>
    {
        private Item()
        {

        }

        public Item(string id, string name, int? stackSize = null)
        {
            ItemId = id;
            Update(name, stackSize ?? 64);
        }

        /// <summary>
        /// Идентификатор предмета
        /// </summary>
        public string ItemId { get; private set; }

        /// <summary>
        /// Название предмета
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Размер стака предмета
        /// </summary>
        public int StackSize { get; private set; }

        public void Update(string name, int stackSize)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            StackSize = stackSize;
        }

        public bool Equals([AllowNull] Item other) =>
            !(other is null)
            && (ReferenceEquals(this, other) || ItemId == other.ItemId);

        public override bool Equals(object obj) =>
            obj is Item item && Equals(item);

        public static long IdToString(string id)
        {
            var values = id.Split(",");
            var mainValue = int.Parse(values.First());
            var partValue = values.Length == 1 ? 0 : int.Parse(values[1]);
            const int multipler = 100;
            if (partValue >= multipler)
            {
                throw new ArgumentException(nameof(id));
            }
            return mainValue * multipler + partValue;
        }
    }
}
