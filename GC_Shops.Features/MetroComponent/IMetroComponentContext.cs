using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.MetroComponent
{
    public interface IMetroComponentContext
    {
        DbSet<ControlBlock> ControlBlocks { get; set; }
        DbSet<MetroStation> Metros { get; set; }
        DbSet<MetroOwner> MetroOwners { get; set; }
    }
}
