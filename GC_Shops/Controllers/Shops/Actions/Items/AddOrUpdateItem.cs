using GS_Shops.Db;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Shops.Actions.Items
{
    public class AddOrUpdateItem
    {
        public class Command : IRequest
        {

            [FromRoute]
            public Guid ShopId { get; set; }

            [FromRoute]
            public string ItemId { get; set; }

            [FromBody]
            public decimal SellCost { get; set; }
            [FromBody]
            public uint ItemsInSell { get; set; }
            [FromBody]
            public decimal BuyCost { get; set; }
            [FromBody]
            public uint ItemsInBuy { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly FeaturesDbContext _dbContext;
            public Handler(FeaturesDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var shop = await _dbContext.Shops
                    .Include(s => s.Items)
                    .SingleAsync(s => s.ShopId == request.ShopId, cancellationToken);
                var item = await _dbContext.Items.SingleAsync(i => i.ItemId == request.ItemId);
                shop.AddOrUpdateItem(item, request.SellCost, request.ItemsInSell, request.BuyCost, request.ItemsInBuy);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
