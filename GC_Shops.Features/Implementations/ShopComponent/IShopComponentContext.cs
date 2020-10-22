using GC_Shops.Features.Implementations.ItemComponent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.Implementations.ShopComponent
{
    public interface IShopComponentContext : IItemContext
    {
        public DbSet<Shop> Shops { get; set; }
    }
}
