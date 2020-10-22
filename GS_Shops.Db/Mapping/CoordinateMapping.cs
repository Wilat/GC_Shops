using GC_Shops.Features;
using GC_Shops.Features.CoordinateComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public static class CoordinateMapping
    {
        public static void Configure<T>(OwnedNavigationBuilder<T, Coordinate> builder) where T : class
        {
            builder.Property(c => c.X)
                .HasColumnName("Coordinate_X")
                .IsRequired();
            builder.Property(c => c.Y)
                .HasColumnName("Coordinate_Y")
                .IsRequired();
            builder.Property(c => c.Z)
                .HasColumnName("Coordinate_Z")
                .IsRequired();
            builder.HasIndex(c => new
            {
                c.X,
                c.Y,
                c.Z
            });
        }
    }
}
