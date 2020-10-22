using GC_Shops.Features.Implementations.ItemComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.Implementations.ShopComponent
{
    public class ShopItem
    {
        private ShopItem() { }

        public ShopItem(Shop shop, Item item, decimal sellCost, uint itemsInSell, decimal buyCost, uint itemsInBuy)
        {
            Shop = shop ?? throw new ArgumentNullException(nameof(shop));
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Update(sellCost, itemsInSell, buyCost, itemsInBuy);
        }

        public Shop Shop { get; private set; }

        public Item Item { get; private set; }

        public decimal SellCost { get; private set; }

        public uint ItemsInSell { get; private set; }

        public decimal BuyCost { get; private set; }

        public uint ItemsInBuy { get; private set; }

        public void Update(decimal sellCost, uint itemsInSell, decimal buyCost, uint itemsInBuy)
        {
            SellCost = sellCost;
            ItemsInSell = itemsInSell;
            BuyCost = buyCost;
            ItemsInBuy = itemsInBuy;
        }
    }
}
