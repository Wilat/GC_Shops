using GC_Shops.Features.Implementations.MetroComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public class MetroStationMapping : IEntityTypeConfiguration<MetroStation>
    {
        public void Configure(EntityTypeBuilder<MetroStation> builder)
        {
            builder.HasKey(m => m.MetroStationId);
            builder.Property(m => m.Name)
                .IsRequired();

            builder.HasMany(m => m.ControlBlocks)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(m => m.Owner)
                .WithMany();
        }
    }
}
