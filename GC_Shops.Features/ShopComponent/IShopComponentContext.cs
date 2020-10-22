using GC_Shops.Features.ItemComponent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.ShopComponent
{
    public interface IShopComponentContext : IItemContext
    {
        public DbSet<Shop> Shops { get; set; }
    }
}
