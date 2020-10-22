using GC_Shops.Features.Implementations.ShopComponent;
using GS_Shops.Db;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GC_Shops.Controllers.Shops.Actions
{
    public class CreateShop
    {
        public class Request : IRequest<Guid>
        {
            [FromBody]
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Request, Guid>
        {
            private readonly FeaturesDbContext _dbContext;
            public Handler(FeaturesDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
            {
                var shop = _dbContext.Shops.Add(new Shop(request.Name)).Entity;
                await _dbContext.SaveChangesAsync();
                return shop.ShopId;
            }
        }
    }
}
