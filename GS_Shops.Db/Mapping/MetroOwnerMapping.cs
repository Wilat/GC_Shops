using GC_Shops.Features.Implementations.MetroComponent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS_Shops.Db.Mapping
{
    public class MetroOwnerMapping : IEntityTypeConfiguration<MetroOwner>
    {
        public void Configure(EntityTypeBuilder<MetroOwner> builder)
        {
            builder.HasKey(mo => mo.MetroOwnerId);
            builder.Property(mo => mo.Name).IsRequired();
        }
    }
}
