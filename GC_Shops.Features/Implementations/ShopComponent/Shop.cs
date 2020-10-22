using GC_Shops.Features.Implementations.CoordinateComponent;
using GC_Shops.Features.Implementations.ItemComponent;
using GC_Shops.Features.Implementations.MetroComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GC_Shops.Features.Implementations.ShopComponent
{
    public class Shop
    {
        private Shop()
        {

        }

        public Shop(string name, Coordinate coordinate = null)
        {
            ShopId = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            Coordinate = coordinate;
        }

        public Guid ShopId { get; private set; }

        /// <summary>
        /// Название магазина
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Координата входа
        /// </summary>
        public Coordinate Coordinate { get; private set; }

        /// <summary>
        /// Ближайшее метро
        /// </summary>
        public ISet<MetroStation> MetroStations { get; private set; }

        public ISet<ShopItem> Items { get; private set; }

        public void AddOrUpdateItem(Item item, decimal sellCost, uint itemsInSell, decimal buyCost, uint itemsInBuy)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));
            var existedItem = Items.SingleOrDefault(i => item.Equals(i.Item));
            if (existedItem is null)
            {
                Items.Add(new ShopItem(this, item, sellCost, itemsInSell, buyCost, itemsInBuy));
            } else
            {
                existedItem.Update(sellCost, itemsInSell, buyCost, itemsInBuy);
            }
        }

        public ShopItem RemoveItem(string itemId)
        {
            var existedItem = Items.SingleOrDefault(i => i.Item.ItemId == itemId);
            if (existedItem is null)
            {
                return null;
            }
            Items.Remove(existedItem);
            return existedItem;
        }

        public ShopItem RemoveItem(Item item) => RemoveItem(item.ItemId);

        public void AddMetroStation(MetroStation station)
        {
            _ = station ?? throw new ArgumentNullException(nameof(station));
            var existedStation = MetroStations.SingleOrDefault(ms => station.Equals(ms));
            if (existedStation is null)
            {
                MetroStations.Add(station);
            }
        }

        public MetroStation RemoveMetroStation(MetroStation station)
        {
            _ = station ?? throw new ArgumentNullException(nameof(station));
            return RemoveMetroStation(station.MetroStationId);
        }

        public MetroStation RemoveMetroStation(Guid metroStationId)
        {
            var existedStation = MetroStations.SingleOrDefault(ms => ms.MetroStationId == metroStationId);
            if (existedStation != null)
            {
                MetroStations.Remove(existedStation);
            }
            return existedStation;
        }
    }
}
