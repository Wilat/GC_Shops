using GC_Shops.Features.ShopComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public class ShopMapping : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(s => s.ShopId);
            builder.Property(s => s.Name).IsRequired();

            builder.OwnsOne(s => s.Coordinate);

            builder.HasMany(s => s.MetroStations)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
