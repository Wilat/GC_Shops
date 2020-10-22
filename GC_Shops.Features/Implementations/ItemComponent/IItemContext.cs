using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.Implementations.ItemComponent
{
    public interface IItemContext
    {
        public DbSet<Item> Items { get; set; }
    }
}
