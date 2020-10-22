using GC_Shops.Features.CoordinateComponent;
using GC_Shops.Features.MetroComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public class ControlBlockMapping : IEntityTypeConfiguration<ControlBlock>
    {
        public void Configure(EntityTypeBuilder<ControlBlock> builder)
        {
            builder.HasKey(cb => cb.ControlBlockId);
            builder.Property(cb => cb.Name).IsRequired();

            builder.OwnsOne(cb => cb.Coordinate, CoordinateMapping.Configure);

            builder.HasOne(cb => cb.EastCb)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(cb => cb.NorthCb)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(cb => cb.SouthCb)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(cb => cb.WestCb)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
