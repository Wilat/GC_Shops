using GC_Shops.Features.Implementations.CoordinateComponent;
using GC_Shops.Features.Implementations.ItemComponent;
using GC_Shops.Features.Implementations.MetroComponent;
using GC_Shops.Features.Implementations.ShopComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features.Implementations
{
    public interface IFeaturesContext : 
        IItemContext, 
        IMetroComponentContext, 
        IShopComponentContext
    {
    }
}
