using GC_Shops.Features.CoordinateComponent;
using GC_Shops.Features.ItemComponent;
using GC_Shops.Features.MetroComponent;
using GC_Shops.Features.ShopComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Shops.Features
{
    public interface IFeaturesContext : 
        IItemContext, 
        IMetroComponentContext, 
        IShopComponentContext
    {
    }
}
