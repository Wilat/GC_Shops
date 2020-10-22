using GC_Shops.Features.Implementations.ItemComponent;
using GC_Shops.Features.Implementations.ShopComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public class ShopItemMapping : IEntityTypeConfiguration<ShopItem>
    {
        public void Configure(EntityTypeBuilder<ShopItem> builder)
        {
            builder.HasKey(nameof(Shop.ShopId), nameof(Item.ItemId));
            builder.HasOne(si => si.Shop)
                .WithMany(s => s.Items)
                .HasForeignKey(nameof(Shop.ShopId))
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(si => si.Item)
                .WithMany()
                .HasForeignKey(nameof(Item.ItemId))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
