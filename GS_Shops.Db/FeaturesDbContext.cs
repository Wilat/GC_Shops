using GC_Shops.Features.Implementations;
using GC_Shops.Features.Implementations.CoordinateComponent;
using GC_Shops.Features.Implementations.ItemComponent;
using GC_Shops.Features.Implementations.MetroComponent;
using GC_Shops.Features.Implementations.ShopComponent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace GS_Shops.Db
{
    public class FeaturesDbContext : DbContext, IFeaturesContext
    {
        public FeaturesDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public FeaturesDbContext()
        {
        }

        public DbSet<ControlBlock> ControlBlocks { get; set; }
        public DbSet<MetroStation> MetroStations { get; set; }
        public DbSet<MetroOwner> MetroOwners { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<MetroStation> Metros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetAssembly(typeof(FeaturesDbContext));
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
